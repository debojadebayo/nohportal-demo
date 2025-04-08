using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.CRM.Entities
{
    public class Employee : BaseEntity<Employee>
    {
        public string CaseNumber { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string JobRole { get; set; }
        public string Department { get; set; }
        public string LineManager { get; set; }
    }
}