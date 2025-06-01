using System.Collections.Generic;
using ComposedHealthBase.Server.Entities;


namespace Server.Modules.CRM.Entities
{
    public class Manager : BaseEntity<Manager>, IEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Department { get; set; }
        public HashSet<Employee> Employees { get; set; } = new();
        public long CustomerId
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