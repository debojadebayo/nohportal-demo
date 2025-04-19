using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Client.RoleManagement;
using MudBlazor.Services;
using System.Net;

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

await builder.Build().RunAsync();