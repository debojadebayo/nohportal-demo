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

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        await RoleSeeder.SeedRolesAndPermissions(dbContext);
    }
}

app.Run();