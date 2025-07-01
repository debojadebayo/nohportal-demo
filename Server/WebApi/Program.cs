using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Server.Extensions;
using Server.Modules.Clinical.Infrastructure;
using Server.Modules.CRM.Infrastructure;
using Server.Modules.Scheduling.Infrastructure;
using Server.Modules.Auth.Infrastructure;
using Server.Modules.Billing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var moduleTypes = new List<Type>
{
    typeof(AuthModule),
    typeof(BillingModule),
	typeof(ClinicalModule),
	typeof(CRMModule),
	typeof(SchedulingModule)
};

builder.Services.RegisterServices(builder.Configuration, ref moduleTypes, out var registeredModules);

var app = builder.Build();


app.ConfigureServicesAndMapEndpoints(builder.Environment.IsDevelopment(), registeredModules);
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.Run();