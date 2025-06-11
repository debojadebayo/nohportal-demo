using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Server.Extensions;
using Server.Modules.CRM.Infrastructure;
using Server.Modules.Scheduling.Infrastructure;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase;

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

app.Run();

if (app.Environment.IsDevelopment())
{
	await RoleSeeder.SeedRolesAndPermissions(registeredModules, app.Services.GetRequiredService<AuthDbContext>());
}