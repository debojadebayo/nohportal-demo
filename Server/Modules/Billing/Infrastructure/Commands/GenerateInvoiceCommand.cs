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
using ComposedHealthBase.Server.Queries;
using Server.Modules.Shared.Contracts;

namespace Server.Modules.Billing.Infrastructure.Commands
{
    public interface IGenerateInvoiceCommand
    {
        Task<Guid> Handle(InvoiceFilterDto filterDto, ClaimsPrincipal user);
    }

    public class GenerateInvoiceCommand : IGenerateInvoiceCommand, ICommand
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IGetSchedulesForInvoiceQuery _getSchedulesForInvoiceQuery;
        private readonly IGetProductsByIdsQuery _getProductsByIdsQuery;
        private readonly BillingDbContext _billingDbContext;

        public GenerateInvoiceCommand(
            IGetSchedulesForInvoiceQuery getSchedulesForInvoiceQuery,
            IGetProductsByIdsQuery getProductsByIdsQuery,
            IAuthorizationService authorizationService,
            BillingDbContext billingDbContext)
        {
            _authorizationService = authorizationService;
            _getProductsByIdsQuery = getProductsByIdsQuery;
            _billingDbContext = billingDbContext;
            _getSchedulesForInvoiceQuery = getSchedulesForInvoiceQuery;
        }

        public async Task<Guid> Handle(InvoiceFilterDto filterDto, ClaimsPrincipal user)
        {
            // Step 1: Fetch schedules using the shared interface
            var schedules = await _getSchedulesForInvoiceQuery.Handle(
                filterDto.CustomerId,
                filterDto.FromDate,
                filterDto.ToDate,
                filterDto.ProductId);

            if (!schedules.Any())
            {
                throw new InvalidOperationException("No schedules found for the specified criteria.");
            }

            // Step 2: Fetch products using the shared interface
            var productIds = schedules.Select(s => s.ProductId).Distinct().ToList();
            var products = await _getProductsByIdsQuery.Handle(productIds);

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
                Status = "Draft",
                TaxRate = 0.20m, // 20% VAT
                IsActive = true,
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
