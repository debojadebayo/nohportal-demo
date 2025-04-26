using ComposedHealthBase.Server.Entities;

namespace Server.Modules.CRM.Entities
{
    public class Employee : BaseEntity<Employee>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public long CustomerId { get; set; }
        public string JobRole { get; set; }
        public string Department { get; set; }
        public string LineManager { get; set; }
    }
}