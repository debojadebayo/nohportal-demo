using ComposedHealthBase.Server.Entities;


namespace Server.Modules.CRM.Entities
{
    public class Contract : BaseEntity<Contract>, IEntity, IAuditEntity
    {
        public required string Reference { get; set; }
        public string? Notes { get; set; }
        public Guid RepresentativeId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid CustomerId
        {
            get
            {
                return TenantId;
            }
            set
            {
                TenantId = value;
            }
        }
    }
}