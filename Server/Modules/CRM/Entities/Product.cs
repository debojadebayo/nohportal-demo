using ComposedHealthBase.Server.Entities;


namespace Server.Modules.CRM.Entities
{
    public class Product : BaseEntity<Product>, IEntity, IAuditEntity
    {
        public required ProductType ProductType { get; set; }
        public decimal Price { get; set; }
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