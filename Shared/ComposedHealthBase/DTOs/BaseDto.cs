namespace ComposedHealthBase.Shared.DTOs
{
    public class BaseDto<TDto>
    where TDto : class
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}