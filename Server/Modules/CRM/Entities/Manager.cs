using System.Collections.Generic;
using ComposedHealthBase.Server.Entities;

namespace Server.Modules.CRM.Entities
{
    public class Manager : BaseEntity<Manager>
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public Customer Customer { get; set; }
    }
}