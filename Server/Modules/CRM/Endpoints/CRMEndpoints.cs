using ComposedHealthBase.Server.BaseModule.Endpoints;
using Shared.DTOs.CRM;
using Server.Modules.CRM.Entities;

namespace Server.Modules.CRM.Endpoints
{
		public class EmployeeEndpoints : BaseEndpoints<Employee, EmployeeDto>, IEndpoints {}
		public class CustomerEndpoints : BaseEndpoints<Customer, CustomerDto>, IEndpoints {}
		public class ContractEndpoints : BaseEndpoints<Contract, ContractDto>, IEndpoints {}
		public class ProductEndpoints : BaseEndpoints<Product, ProductDto>, IEndpoints {}
}