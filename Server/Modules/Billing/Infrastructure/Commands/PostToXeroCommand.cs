using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using ComposedHealthBase.Server.Interfaces;
using Server.Modules.Billing.Infrastructure.Database;
using Server.Modules.Billing.Entities;
using Shared.DTOs.Billing;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Security.Claims;

namespace Server.Modules.Billing.Infrastructure.Commands;

public interface IPostToXeroCommand
{
    Task<PostToXeroResponseDto> Handle(PostToXeroRequestDto requestDto, ClaimsPrincipal user);
}

public class PostToXeroCommand : IPostToXeroCommand, ICommand
{
    private readonly IAuthorizationService _authorizationService;
    private readonly BillingDbContext _billingDbContext;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PostToXeroCommand(
        IAuthorizationService authorizationService,
        BillingDbContext billingDbContext,
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _authorizationService = authorizationService;
        _billingDbContext = billingDbContext;
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<PostToXeroResponseDto> Handle(PostToXeroRequestDto requestDto, ClaimsPrincipal user)
    {
        // Step 1: Fetch invoice with line items using the shared database context
        var invoice = await _billingDbContext.Set<Invoice>()
            .Include(i => i.LineItems)
            .FirstOrDefaultAsync(i => i.Id == requestDto.InvoiceId);

        if (invoice == null)
        {
            throw new InvalidOperationException($"Invoice with ID {requestDto.InvoiceId} not found.");
        }

        // Step 2: Check if invoice is already posted to Xero
        if (!string.IsNullOrEmpty(invoice.XeroInvoiceId))
        {
            return new PostToXeroResponseDto
            {
                Success = false,
                Message = "Invoice has already been posted to Xero.",
                XeroInvoiceId = invoice.XeroInvoiceId
            };
        }

        // Step 3: Validate invoice status
        if (invoice.Status != "Finalised")
        {
            throw new InvalidOperationException("Only finalised invoices can be posted to Xero.");
        }

        try
        {
            // Step 4: Map invoice to Xero format
            var xeroInvoice = MapToXeroInvoice(invoice);

            // Step 5: Post to Xero API
            var xeroResponse = await PostToXeroApi(xeroInvoice);

            if (xeroResponse.Success)
            {
                // Step 6: Update invoice with Xero details
                invoice.XeroInvoiceId = xeroResponse.XeroInvoiceId;
                invoice.PostedToXero = true;
                invoice.PostedToXeroAt = DateTime.UtcNow;

                await _billingDbContext.SaveChangesWithAuditAsync(user);
            }

            return xeroResponse;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error posting invoice to Xero: {ex.Message}", ex);
        }
    }

    private object MapToXeroInvoice(Invoice invoice)
    {
        return new
        {
            Type = "ACCREC",
            Contact = new
            {
                ContactID = invoice.CustomerId.ToString(),
                Name = $"Customer {invoice.CustomerId}" // You might want to add customer name to Invoice entity
            },
            Date = invoice.InvoiceDate.ToString("yyyy-MM-dd"),
            DueDate = invoice.DueDate?.ToString("yyyy-MM-dd"),
            InvoiceNumber = invoice.InvoiceNumber,
            Reference = invoice.Notes,
            Status = "AUTHORISED",
            LineItems = invoice.LineItems.Select(item => new
            {
                Description = item.Description ?? item.ProductName,
                Quantity = item.Quantity,
                UnitAmount = item.UnitPrice,
                AccountCode = item.ProductTypeChargeCode,
                TaxType = "NONE" // You might want to add tax type mapping
            }).ToArray()
        };
    }

    private async Task<PostToXeroResponseDto> PostToXeroApi(object xeroInvoice)
    {
        var json = JsonSerializer.Serialize(new { Invoices = new[] { xeroInvoice } });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Get Xero configuration from appsettings
        var xeroAccessToken = _configuration["Xero:AccessToken"];
        var xeroTenantId = _configuration["Xero:TenantId"];
        var xeroApiUrl = _configuration["Xero:ApiUrl"];

        if (string.IsNullOrEmpty(xeroAccessToken) || string.IsNullOrEmpty(xeroTenantId) || string.IsNullOrEmpty(xeroApiUrl))
        {
            throw new InvalidOperationException("Xero configuration is missing. Please check AccessToken, TenantId, and ApiUrl settings.");
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", xeroAccessToken);
        _httpClient.DefaultRequestHeaders.Add("Xero-tenant-id", xeroTenantId);

        var response = await _httpClient.PostAsync($"{xeroApiUrl}/Invoices", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var responseObj = JsonSerializer.Deserialize<JsonElement>(responseContent);
            var invoiceId = responseObj.GetProperty("Invoices")[0].GetProperty("InvoiceID").GetString();

            return new PostToXeroResponseDto
            {
                Success = true,
                Message = "Successfully posted to Xero",
                XeroInvoiceId = invoiceId
            };
        }

        return new PostToXeroResponseDto
        {
            Success = false,
            Message = $"Xero API error: {responseContent}",
            XeroInvoiceId = null
        };
    }
}
