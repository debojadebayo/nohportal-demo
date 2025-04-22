namespace ComposedHealthBase.Shared.DTOs
{
    public interface IDto
    {
        long Id { get; set; }
        bool IsActive { get; set; }
        int CreatedBy { get; set; }
        int LastModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}