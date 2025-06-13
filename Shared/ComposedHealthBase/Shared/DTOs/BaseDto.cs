namespace ComposedHealthBase.Shared.DTOs
{
	public class BaseDto<T> : IAuditDto
	where T : class
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public string CreatedBy { get; set; } = string.Empty;
		public string LastModifiedBy { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}
}