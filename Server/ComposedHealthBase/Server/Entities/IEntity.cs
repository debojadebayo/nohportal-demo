namespace ComposedHealthBase.Server.Entities
{
	public interface IEntity
	{
		long Id { get; set; }
		bool IsActive { get; set; }
		long CreatedBy { get; set; }
		long LastModifiedBy { get; set; }
		DateTime CreatedDate { get; set; }
		DateTime ModifiedDate { get; set; }
		long SubjectId { get; set; }
		long TenantId { get; set; }
	}
}
