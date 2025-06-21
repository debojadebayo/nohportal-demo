using ComposedHealthBase.Server.Endpoints;
using Shared.DTOs.CRM;
using Server.Modules.CRM.Entities;
using Server.Modules.CRM.Infrastructure.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using ComposedHealthBase.Server.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;

using ComposedHealthBase.Shared.DTOs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Server.Modules.CRM.Endpoints
{
	public class EmployeeEndpoints : BaseEndpoints<Employee, EmployeeDto, CRMDbContext>, IEndpoints { }
	public class CustomerEndpoints : BaseEndpoints<Customer, CustomerDto, CRMDbContext>, IEndpoints { }
	public class ContractEndpoints : BaseEndpoints<Contract, ContractDto, CRMDbContext>, IEndpoints { }
	public class ProductEndpoints : BaseEndpoints<Product, ProductDto, CRMDbContext>, IEndpoints { }
	public class ProductTypeEndpoints : BaseEndpoints<ProductType, ProductTypeDto, CRMDbContext>, IEndpoints { }
	public class CustomerDocumentEndpoints : DocumentEndpoints<CustomerDocument, CustomerDocumentDto, CRMDbContext>, IEndpoints { }
	public class EmployeeDocumentEndpoints : DocumentEndpoints<EmployeeDocument, EmployeeDocumentDto, CRMDbContext>, IEndpoints { }
	public class ManagerEndpoints : BaseEndpoints<Manager, ManagerDto, CRMDbContext>, IEndpoints { }
}