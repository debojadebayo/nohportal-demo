// filepath: Modules/Billing/Endpoints/BillingEndpoints.cs
using ComposedHealthBase.Server.Endpoints;
using Server.Modules.Billing.Entities;
using Server.Modules.Billing.Infrastructure.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using ComposedHealthBase.Server.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Shared.DTOs.Billing;
using Server.Modules.Billing.Infrastructure.Commands;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Server.Modules.Billing.Endpoints
{
    public class InvoiceEndpoints : BaseEndpoints<Invoice, InvoiceDto, BillingDbContext>, IEndpoints
    {
        public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints = base.MapEndpoints(endpoints);
            var group = endpoints.MapGroup("/api/billing/invoice");

            // Custom invoice generation endpoint
            group.MapPost("/generate", (
                [FromServices] IGenerateInvoiceCommand generateInvoiceCommand,
                ClaimsPrincipal user,
                InvoiceFilterDto filterDto
            ) => GenerateInvoice(generateInvoiceCommand, user, filterDto));

            // CSV export endpoint
            group.MapGet("/export-csv/{invoiceId:guid}", (
                [FromServices] BillingDbContext dbContext,
                [FromServices] IMapper<Invoice, InvoiceDto> mapper,
                Guid invoiceId
            ) => ExportInvoiceToCSV(dbContext, mapper, invoiceId));

            // Post to Xero endpoint
            group.MapPost("/post-to-xero", (
                [FromServices] IPostToXeroCommand postToXeroCommand,
                ClaimsPrincipal user,
                PostToXeroRequestDto requestDto
            ) => PostToXero(postToXeroCommand, user, requestDto));

            return endpoints;
        }

        private async Task<IResult> GenerateInvoice(IGenerateInvoiceCommand generateInvoiceCommand, ClaimsPrincipal user, InvoiceFilterDto filterDto)
        {
            try
            {
                var invoiceId = await generateInvoiceCommand.Handle(filterDto, user);
                return Results.Ok(new { InvoiceId = invoiceId });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while generating invoice: {ex.Message}");
                return Results.Problem($"An error occurred while generating the invoice: {ex.Message}");
            }
        }

        private async Task<IResult> ExportInvoiceToCSV(BillingDbContext dbContext, IMapper<Invoice, InvoiceDto> mapper, Guid invoiceId)
        {
            try
            {
                var invoice = await dbContext.Invoices
                    .Include(i => i.LineItems)
                    .FirstOrDefaultAsync(i => i.Id == invoiceId);

                if (invoice == null)
                {
                    return Results.NotFound($"Invoice with ID {invoiceId} not found.");
                }

                var csv = new StringBuilder();
                csv.AppendLine("Invoice Number,Invoice Date,Due Date,Customer ID,Product ID,Line Item,Product Name,Charge Code,Unit Price,Quantity,Line Total,Service Date,Description");
                
                foreach (var lineItem in invoice.LineItems)
                {
                    csv.AppendLine($"\"{invoice.InvoiceNumber}\",\"{invoice.InvoiceDate:yyyy-MM-dd}\",\"{invoice.DueDate?.ToString("yyyy-MM-dd") ?? ""}\",\"{invoice.CustomerId}\",\"{invoice.ProductId ?? Guid.Empty}\",\"{lineItem.Id}\",\"{lineItem.ProductName}\",\"{lineItem.ProductTypeChargeCode}\",\"{lineItem.UnitPrice}\",\"{lineItem.Quantity}\",\"{lineItem.LineTotal}\",\"{lineItem.ServiceDate:yyyy-MM-dd}\",\"{lineItem.Description ?? ""}\"");
                }

                var bytes = Encoding.UTF8.GetBytes(csv.ToString());
                return Results.File(bytes, "text/csv", $"Invoice_{invoice.InvoiceNumber}_{DateTime.Now:yyyyMMdd}.csv");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while exporting invoice to CSV: {ex.Message}");
                return Results.Problem($"An error occurred while exporting the invoice: {ex.Message}");
            }
        }

        private async Task<IResult> PostToXero(IPostToXeroCommand postToXeroCommand, ClaimsPrincipal user, PostToXeroRequestDto requestDto)
        {
            try
            {
                var result = await postToXeroCommand.Handle(requestDto, user);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while posting to Xero: {ex.Message}");
                return Results.Problem($"An error occurred while posting to Xero: {ex.Message}");
            }
        }
    }

    public class LineItemEndpoints : BaseEndpoints<LineItem, LineItemDto, BillingDbContext>, IEndpoints { }
}
