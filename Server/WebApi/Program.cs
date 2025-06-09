using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Server.Extensions;
using Server.Modules.CRM.Infrastructure;
using Server.Modules.Scheduling.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Auth.AuthorizationHandlers; // Corrected namespace
using ComposedHealthBase.Server.Auth.Requirements;

var builder = WebApplication.CreateBuilder(args);

var moduleTypes = new List<Type>
{
	typeof(BaseModule),
	typeof(CRMModule),
	typeof(SchedulingModule)
};

builder.Services.AddHttpContextAccessor(); // Needed for accessing HttpContext in handlers

// Register Authorization Handlers
builder.Services.AddScoped<IAuthorizationHandler, TenantLimitedAdministratorAuthorizationHandler>(); // Corrected class name


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TenantLimitedAdminPolicy", policy =>
        policy.Requirements.Add(new TenantLimitedAdminRequirement()));
    // Add other policies here
});

builder.Services.RegisterServices(builder.Configuration, ref moduleTypes, out var registeredModules);

var app = builder.Build();

app.ConfigureServicesAndMapEndpoints(builder.Environment.IsDevelopment(), ref moduleTypes, registeredModules);

app.Run();