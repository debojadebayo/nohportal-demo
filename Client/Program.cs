using Client;
using Client.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Client.RoleManagement;
using MudBlazor.Services;
using System.Net;
using MudBlazor;
using ComposedHealthBase.BaseClient.Services;
using Shared.DTOs.CRM;
using Shared.DTOs.Scheduling;
using Shared.DTOs.Clinical;
using Blazored.LocalStorage;
using Blazor.SubtleCrypto;
using FluentValidation;
using Shared.Validators;
using Shared.DTOs.Billing;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure options from appsettings.json
builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection(ApiOptions.SectionName));
builder.Services.Configure<OidcOptions>(builder.Configuration.GetSection(OidcOptions.SectionName));
builder.Services.Configure<CultureOptions>(builder.Configuration.GetSection(CultureOptions.SectionName));

// Get configuration values
var apiOptions = builder.Configuration.GetSection(ApiOptions.SectionName).Get<ApiOptions>() ?? new ApiOptions();
var oidcOptions = builder.Configuration.GetSection(OidcOptions.SectionName).Get<OidcOptions>() ?? new OidcOptions();
var cultureOptions = builder.Configuration.GetSection(CultureOptions.SectionName).Get<CultureOptions>() ?? new CultureOptions();

builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri(apiOptions.BaseUrl))
   .AddHttpMessageHandler(sp =>
   {
       var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
           .ConfigureHandler(authorizedUrls: apiOptions.AuthorizedUrls);
       return handler;
   });

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = oidcOptions.Authority;
    options.ProviderOptions.ClientId = oidcOptions.ClientId;
    options.ProviderOptions.ResponseType = oidcOptions.ResponseType;
    foreach (var scope in oidcOptions.DefaultScopes)
    {
        options.ProviderOptions.DefaultScopes.Add(scope);
    }
    options.UserOptions.RoleClaim = oidcOptions.RoleClaim;
}).AddAccountClaimsPrincipalFactory<ParseRoleClaimsPrincipalFactory>();

builder.Services.AddMudServices();
MudGlobal.InputDefaults.Variant = Variant.Outlined;
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSubtleCrypto();

// Register FluentValidation validators so they can be injected in Blazor components
builder.Services.AddValidatorsFromAssemblyContaining<ReferralDetailsValidator>();

// Register application services
builder.Services.AddScoped<ILazyLookupService<CustomerDto>, LazyLookupService<CustomerDto>>();
builder.Services.AddScoped<ILazyLookupService<EmployeeDto>, LazyLookupService<EmployeeDto>>();
builder.Services.AddScoped<ILazyLookupService<ManagerDto>, LazyLookupService<ManagerDto>>();
builder.Services.AddScoped<ILazyLookupService<ClinicianDto>, LazyLookupService<ClinicianDto>>();
builder.Services.AddScoped<ILazyLookupService<ReferralDto>, LazyLookupService<ReferralDto>>();
builder.Services.AddScoped<ILazyLookupService<ProductDto>, LazyLookupService<ProductDto>>();
builder.Services.AddScoped<ILazyLookupService<ProductTypeDto>, LazyLookupService<ProductTypeDto>>();
builder.Services.AddScoped<ILazyLookupService<ScheduleDto>, LazyLookupService<ScheduleDto>>();
builder.Services.AddScoped<ILazyLookupService<CustomerDocumentDto>, LazyLookupService<CustomerDocumentDto>>();
builder.Services.AddScoped<ILazyLookupService<EmployeeDocumentDto>, LazyLookupService<EmployeeDocumentDto>>();
builder.Services.AddScoped<ILazyLookupService<ContractDto>, LazyLookupService<ContractDto>>();
builder.Services.AddScoped<ILazyLookupService<CaseNoteDto>, LazyLookupService<CaseNoteDto>>();
builder.Services.AddScoped<ILazyLookupService<ClinicalReportDto>, LazyLookupService<ClinicalReportDto>>();
builder.Services.AddScoped<ILazyLookupService<InvoiceDto>, LazyLookupService<InvoiceDto>>();

builder.Services.AddScoped<IDocumentUploadService, DocumentUploadService>();
builder.Services.AddScoped<IAuthHelperService, AuthHelperService>();

// Set the culture from configuration for proper currency formatting
var culture = new System.Globalization.CultureInfo(cultureOptions.DefaultCulture);
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();