using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using System.Security.Claims;
using System.Text;
using ComposedHealthBase.Server.Interfaces;
using Server.Modules.Billing.Infrastructure.Database;
using Server.Modules.Billing.Entities;
using Shared.DTOs.Billing;
using Shared.DTOs.Scheduling;
using Shared.DTOs.CRM;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.Billing.Infrastructure.Commands
{
    public interface IGenerateInvoiceCommand
    {
        Task<Guid> Handle(InvoiceFilterDto filterDto, ClaimsPrincipal user);
    }

    public class GenerateInvoiceCommand : IGenerateInvoiceCommand, ICommand
    {
        private readonly IDbContext<BillingDbContext> _billingDbContext;
        private readonly IMapper<Invoice, InvoiceDto> _invoiceMapper;
        private readonly IMapper<LineItem, LineItemDto> _lineItemMapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthorizationService _authorizationService;

        public GenerateInvoiceCommand(
            IDbContext<BillingDbContext> billingDbContext,
            IMapper<Invoice, InvoiceDto> invoiceMapper,
            IMapper<LineItem, LineItemDto> lineItemMapper,
            IHttpClientFactory httpClientFactory,
            IAuthorizationService authorizationService)
        {
            _billingDbContext = billingDbContext;
            _invoiceMapper = invoiceMapper;
            _lineItemMapper = lineItemMapper;
            _httpClientFactory = httpClientFactory;
            _authorizationService = authorizationService;
        }

        public async Task<Guid> Handle(InvoiceFilterDto filterDto, ClaimsPrincipal user)
        {
            // Step 1: Fetch schedules from scheduling module
            var schedules = await FetchSchedulesFromSchedulingModule(filterDto);
            
            if (!schedules.Any())
            {
                throw new InvalidOperationException("No schedules found for the specified criteria.");
            }

            // Step 2: Fetch products from CRM module
            var productIds = schedules.Select(s => s.ProductId).Distinct().ToList();
            var products = await FetchProductsFromCRMModule(productIds);

            // Step 3: Create invoice
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = await GenerateInvoiceNumber(),
                InvoiceDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30), // 30 days payment terms
                FromDate = filterDto.FromDate,
                ToDate = filterDto.ToDate,
                CustomerId = filterDto.CustomerId,
                ProductId = filterDto.ProductId,
                Status = "Draft",
                TaxRate = 0.20m, // 20% VAT
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedBy = user.Identity?.Name ?? "System",
                LastModifiedBy = user.Identity?.Name ?? "System",
                TenantId = filterDto.CustomerId
            };

            // Step 4: Create line items
            decimal netTotal = 0;
            foreach (var schedule in schedules)
            {
                var product = products.FirstOrDefault(p => p.Id == schedule.ProductId);
                if (product == null) continue;

                var lineItem = new LineItem
                {
                    Id = Guid.NewGuid(),
                    InvoiceId = invoice.Id,
                    ScheduleId = schedule.Id,
                    ProductId = schedule.ProductId,
                    ProductName = product.ProductType.Name,
                    ProductTypeChargeCode = product.ProductType.ChargeCode,
                    UnitPrice = product.Price,
                    Quantity = 1,
                    ServiceDate = schedule.Start,
                    Description = schedule.Description,
                    EmployeeId = schedule.EmployeeId,
                    ClinicianId = schedule.ClinicianId,
                    IsActive = true,
                    TenantId = filterDto.CustomerId
                };

                lineItem.LineTotal = lineItem.UnitPrice * lineItem.Quantity;
                netTotal += lineItem.LineTotal;

                invoice.LineItems.Add(lineItem);
            }

            // Step 5: Calculate totals
            invoice.NetAmount = netTotal;
            invoice.TaxAmount = invoice.NetAmount * invoice.TaxRate;
            invoice.TotalAmount = invoice.NetAmount + invoice.TaxAmount;

            // Step 6: Save to database
            _billingDbContext.Set<Invoice>().Add(invoice);
            await _billingDbContext.SaveChangesWithAuditAsync(user);

            return invoice.Id;
        }

        private async Task<List<ScheduleDto>> FetchSchedulesFromSchedulingModule(InvoiceFilterDto filterDto)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            
            // Build query parameters
            var queryParams = new List<string>
            {
                $"customerId={filterDto.CustomerId}",
                $"fromDate={filterDto.FromDate:yyyy-MM-dd}",
                $"toDate={filterDto.ToDate:yyyy-MM-dd}"
            };

            if (filterDto.ProductId.HasValue)
            {
                queryParams.Add($"productId={filterDto.ProductId.Value}");
            }

            var queryString = string.Join("&", queryParams);
            var response = await httpClient.GetAsync($"/api/schedule/filter?{queryString}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<List<ScheduleDto>>(content) ?? new List<ScheduleDto>();
            }

            throw new HttpRequestException($"Failed to fetch schedules: {response.StatusCode}");
        }

        private async Task<List<ProductDto>> FetchProductsFromCRMModule(List<Guid> productIds)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            
            var json = System.Text.Json.JsonSerializer.Serialize(productIds);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync("/api/product/getbyids/", content);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<List<ProductDto>>(result) ?? new List<ProductDto>();
            }

            throw new HttpRequestException($"Failed to fetch products: {response.StatusCode}");
        }

        private async Task<string> GenerateInvoiceNumber()
        {
            var year = DateTime.UtcNow.Year;
            var prefix = $"INV-{year}-";
            
            // Find the last invoice number for this year
            var lastInvoice = await _billingDbContext.Set<Invoice>()
                .Where(i => i.InvoiceNumber.StartsWith(prefix))
                .OrderByDescending(i => i.InvoiceNumber)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastInvoice != null)
            {
                var lastNumberPart = lastInvoice.InvoiceNumber.Substring(prefix.Length);
                if (int.TryParse(lastNumberPart, out var lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}{nextNumber:D6}"; // e.g., INV-2025-000001
        }
    }
}
