using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;

namespace Shared.DTOs.CRM
{
    public class ProductTypeDto : IDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long TenantId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal DefaultPrice { get; set; }
        public required string ChargeCode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
