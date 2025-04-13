using ComposedHealthBase.Server.BaseModule.Endpoints;
using ComposedHealthBase.Server.BaseModule.Infrastructure;

using Server.Modules.CRM.Endpoints;
using Server.Modules.CRM.Infrastructure;

using Server.Modules.Scheduling.Endpoints;
using Server.Modules.Scheduling.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var moduleTypes = new List<Type>
{
	typeof(BaseModule),
	typeof(CRMModule),
	typeof(ScheduleModule)
};

var endpointTypes = new List<Type>
{
	typeof(CustomerEndpoints),
	typeof(EmployeeEndpoints),
	
};

builder.Services.RegisterServices(builder.Configuration, ref moduleTypes, out var registeredModules);

var app = builder.Build();

app.ConfigureServices(builder.Environment.IsDevelopment(), ref moduleTypes, registeredModules);

app.MapEndpoints(ref endpointTypes);

app.Run();