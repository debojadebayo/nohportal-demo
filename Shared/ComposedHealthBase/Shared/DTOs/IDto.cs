namespace ComposedHealthBase.Shared.DTOs
{
    public interface IDto
    {
        long Id { get; set; }
        bool IsActive { get; set; }
        long CreatedBy { get; set; }
        long LastModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}