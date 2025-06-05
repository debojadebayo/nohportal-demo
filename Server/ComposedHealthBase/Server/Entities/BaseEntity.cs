namespace ComposedHealthBase.Server.Entities
{
	public class BaseEntity<T> : IEntity
	where T : class
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public string CreatedBy { get; set; } = string.Empty;
		public string LastModifiedBy { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public long TenantId { get; set; }
		public long SubjectId { get; set; }
	}
}