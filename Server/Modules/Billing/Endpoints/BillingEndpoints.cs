// filepath: Modules/Billing/Endpoints/BillingEndpoints.cs
using ComposedHealthBase.Server.Endpoints;
using Server.Modules.Billing.Entities;
using Server.Modules.Billing.Infrastructure.Database;
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

namespace Server.Modules.Billing.Endpoints
{
	//public class ManagerEndpoints : BaseEndpoints<Entity, EntityDto, BillingDbContext>, IEndpoints { }
}
