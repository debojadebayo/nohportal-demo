using ComposedHealthBase.Server.Endpoints;
using Server.Modules.Clinical.Entities;
using Server.Modules.Clinical.Infrastructure.Database;
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
using Shared.DTOs.Clinical;

namespace Server.Modules.Clinical.Endpoints
{
	public class ClinicalReportEndpoints : BaseEndpoints<ClinicalReport, ClinicalReportDto, ClinicalDbContext>, IEndpoints { }
	public class CaseNoteEndpoints : BaseEndpoints<CaseNote, CaseNoteDto, ClinicalDbContext>, IEndpoints { }
}
