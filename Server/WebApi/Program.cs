using ComposedHealthBase.Server.BaseModule.Endpoints;
using ComposedHealthBase.Server.BaseModule.Infrastructure;

using Server.Modules.Billing.Endpoints;
using Server.Modules.Billing.Infrastructure;

using Server.Modules.Clinical.Endpoints;
using Server.Modules.Clinical.Infrastructure;

using Server.Modules.CRM.Endpoints;
using Server.Modules.CRM.Infrastructure;

using Server.Modules.Schedule.Endpoints;
using Server.Modules.Schedule.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var moduleTypes = new List<Type>
{
	typeof(BaseModule),
	typeof(BillingModule),
	typeof(ClinicalModule),
	typeof(CRMModule),
	typeof(ScheduleModule)	
};

var entpointTypes = new List<Type>
{
	typeof(BaseEndpoints),
	typeof(BillingEndpoints),
	typeof(ClinicalEndpoints),
	typeof(CRMEndpoints),
	typeof(ScheduleEndpoints)
};

builder.Services.RegisterServices(builder.Configuration, ref moduleTypes, out var registeredModules);

var app = builder.Build();

app.ConfigureServices(builder.Environment.IsDevelopment(), ref moduleTypes, registeredModules);

app.MapEndpoints(ref entpointTypes);

app.Run();