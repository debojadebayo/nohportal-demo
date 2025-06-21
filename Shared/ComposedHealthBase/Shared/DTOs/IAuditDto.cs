namespace ComposedHealthBase.Shared.DTOs
{
    public interface IAuditDto
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        string CreatedBy { get; set; }
        string LastModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}