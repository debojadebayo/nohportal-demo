using System.Collections.Generic;
using ComposedHealthBase.Server.Entities;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.CRM.Entities
{
    public class Manager : BaseEntity<Manager>, IEntity, IFilterByCustomer, IFilterByEmployee
    {
        public long CustomerId { get; set; }
        public long EmployeeId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Department { get; set; }
    }
}