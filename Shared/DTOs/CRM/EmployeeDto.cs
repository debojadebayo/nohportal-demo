using Server.Base.DTOs;

namespace Shared.DTOs.CRM
{
    public class EmployeeDto : IDto
    {
        public int EmployeeId { get; set; }
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
