using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.CRM
{
    public class DocumentDto : IDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long CustomerId { get; set; }
        public long EmployeeId { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
