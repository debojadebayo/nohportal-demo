using System.Collections.Generic;
using ComposedHealthBase.Server.Entities;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.CRM.Entities
{
    public class Manager : BaseEntity<Manager>, IEntity, INOHEntity, IFilterByCustomer, IFilterByEmployee
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Department { get; set; }
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
        public long EmployeeId
        {
            get
            {
                return SubjectId;
            }
            set
            {
                SubjectId = value;
            }
        }
    }
}