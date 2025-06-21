using Client;
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

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("http://localhost:5003/"))
   .AddHttpMessageHandler(sp =>
   {
	   var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
		   .ConfigureHandler(authorizedUrls: new[] { "http://localhost:5003" });
	   return handler;
   });

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

builder.Services.AddOidcAuthentication(options =>
{
	options.ProviderOptions.Authority = "http://localhost:8180/realms/NationOH";
	options.ProviderOptions.ClientId = "nationoh_client";
	options.ProviderOptions.ResponseType = "code";
	options.ProviderOptions.DefaultScopes.Add("nationoh_webapi-scope");
	options.UserOptions.RoleClaim = "role";
}).AddAccountClaimsPrincipalFactory<ParseRoleClaimsPrincipalFactory>();

builder.Services.AddMudServices();
MudGlobal.InputDefaults.Variant = Variant.Outlined;
builder.Services.AddBlazorPdfViewer();

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
builder.Services.AddScoped<IDocumentUploadService, DocumentUploadService>();
builder.Services.AddScoped<IAuthHelperService, AuthHelperService>();

await builder.Build().RunAsync();