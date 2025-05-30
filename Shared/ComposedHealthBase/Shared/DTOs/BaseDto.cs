namespace ComposedHealthBase.Shared.DTOs
{
	public class BaseDto<T> : IDto
	where T : class
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public long CreatedBy { get; set; }
		public long LastModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}