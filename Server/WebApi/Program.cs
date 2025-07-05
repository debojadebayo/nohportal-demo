using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Server.Extensions;
using Server.Modules.Clinical.Infrastructure;
using Server.Modules.CRM.Infrastructure;
using Server.Modules.Scheduling.Infrastructure;
using Server.Modules.Auth.Infrastructure;
using Server.Modules.Billing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add startup logging
var startupLogger = LoggerFactory.Create(config => 
    config.AddConsole().SetMinimumLevel(LogLevel.Information))
    .CreateLogger("Application.Startup");

startupLogger.LogInformation("Starting NationOH Server application...");
startupLogger.LogInformation("Environment: {Environment}", builder.Environment.EnvironmentName);

var moduleTypes = new List<Type>
{
    typeof(AuthModule),
    typeof(BillingModule),
    typeof(ClinicalModule),
    typeof(CRMModule),
    typeof(SchedulingModule)
};

startupLogger.LogInformation("Registering {ModuleCount} modules: {Modules}", 
    moduleTypes.Count, 
    string.Join(", ", moduleTypes.Select(t => t.Name)));

builder.Services.RegisterServices(builder.Configuration, ref moduleTypes, out var registeredModules);

var app = builder.Build();

<<<<<<< HEAD
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application built successfully. Configuring services and endpoints...");
=======
Console.WriteLine("=== PROGRAM.CS DIAGNOSTICS ===");
Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
Console.WriteLine($"IsDevelopment: {builder.Environment.IsDevelopment()}");
Console.WriteLine($"Registered Modules Count: {registeredModules.Count}");
Console.WriteLine("=== STARTING MODULE CONFIGURATION ===");
>>>>>>> 4582737 (explicit logging in program.cs)

app.ConfigureServicesAndMapEndpoints(builder.Environment.IsDevelopment(), registeredModules);
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

logger.LogInformation("NationOH Server configured and ready to start");

app.Run();