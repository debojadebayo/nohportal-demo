using ComposedHealthBase.Server.BaseModule.Endpoints;
using Shared.DTOs.CRM;
using Server.Modules.CRM.Entities;

namespace Server.Modules.CRM.Endpoints
{
		public class EmployeeEndpoints : BaseEndpoints<Employee, EmployeeDto>, IEndpoints {}
		public class CustomerEndpoints : BaseEndpoints<NOHCustomer, CustomerDto>, IEndpoints {}
}