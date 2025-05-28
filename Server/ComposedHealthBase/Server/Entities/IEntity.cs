namespace ComposedHealthBase.Server.Entities
{
	public interface IEntity
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public long CreatedBy { get; set; }
		public long LastModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public long SubjectId { get; set; }
		public long TenantId { get; set; }
	}
}
