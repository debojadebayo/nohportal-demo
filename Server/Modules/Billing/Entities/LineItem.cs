using ComposedHealthBase.Server.Entities;

namespace Server.Modules.Billing.Entities
{
    public class LineItem : BaseEntity<LineItem>, IEntity, IAuditEntity
    {
        public Guid InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        
        public Guid ScheduleId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductTypeChargeCode { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal LineTotal { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? Description { get; set; }
        
        // Employee and clinician details
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public Guid ClinicianId { get; set; }
        public string ClinicianName { get; set; } = string.Empty;
        
        // Customer context for line item
        public Guid LineItemCustomerId
        {
            get => TenantId;
            set => TenantId = value;
        }
    }
}
