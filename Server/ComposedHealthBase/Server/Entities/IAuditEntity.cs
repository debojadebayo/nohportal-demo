namespace ComposedHealthBase.Server.Entities
{
	public interface IAuditEntity
	{
		bool IsActive { get; set; }
		string CreatedBy { get; set; }
		string LastModifiedBy { get; set; }
		DateTime CreatedDate { get; set; }
		DateTime ModifiedDate { get; set; }
		Guid SubjectId { get; set; }
		Guid TenantId { get; set; }
		Guid TenantKeycloakId { get; set; }
		Guid SubjectKeycloakId { get; set; }
		Guid CreatedByKeycloakId { get; set; }
		Guid ModifiedByKeycloakId { get; set; }
	}
}
