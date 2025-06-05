namespace ComposedHealthBase.Server.Entities
{
	public interface IEntity
	{
		long Id { get; set; }
		bool IsActive { get; set; }
		string CreatedBy { get; set; }
		string LastModifiedBy { get; set; }
		DateTime CreatedDate { get; set; }
		DateTime ModifiedDate { get; set; }
		long SubjectId { get; set; }
		long TenantId { get; set; }
	}
}
