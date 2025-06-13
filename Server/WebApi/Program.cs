using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Server.Extensions;
using Server.Modules.CRM.Infrastructure;
using Server.Modules.Scheduling.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var moduleTypes = new List<Type>
{
	typeof(BaseModule),
	typeof(CRMModule),
	typeof(SchedulingModule)
};

builder.Services.RegisterServices(builder.Configuration, ref moduleTypes, out var registeredModules);

var app = builder.Build();

app.ConfigureServicesAndMapEndpoints(builder.Environment.IsDevelopment(), ref moduleTypes, registeredModules);
app.MapGet("/api-health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));
app.Run();