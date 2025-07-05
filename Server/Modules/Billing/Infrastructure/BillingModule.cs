// filepath: Modules/Billing/Infrastructure/BillingModule.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Billing.Infrastructure.Database;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Server.Mappers;
using Server.Modules.Billing.Entities;
using Shared.DTOs.Billing;
using Server.Modules.Billing.Infrastructure.Mappers;
using Server.Modules.Billing.Infrastructure.Commands;
using ComposedHealthBase.Server.Commands;
using ComposedHealthBase.Server.Queries;

namespace Server.Modules.Billing.Infrastructure
{
	public class BillingModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<IDbContext<BillingDbContext>, BillingDbContext>(options =>
							options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

			// Register mappers
			services.AddScoped<IMapper<Invoice, InvoiceDto>, InvoiceMapper>();
			services.AddScoped<IMapper<LineItem, LineItemDto>, LineItemMapper>();

			// Register commands
			services.AddScoped<IGenerateInvoiceCommand, GenerateInvoiceCommand>();
			services.AddScoped<IPostToXeroCommand, PostToXeroCommand>();
			services.AddScoped<CreateCommand<Invoice, InvoiceDto, BillingDbContext>>();
			services.AddScoped<UpdateCommand<Invoice, InvoiceDto, BillingDbContext>>();
			services.AddScoped<DeleteCommand<Invoice, BillingDbContext>>();
			services.AddScoped<CreateCommand<LineItem, LineItemDto, BillingDbContext>>();
			services.AddScoped<UpdateCommand<LineItem, LineItemDto, BillingDbContext>>();
			services.AddScoped<DeleteCommand<LineItem, BillingDbContext>>();

			// Register queries - use base query classes
			services.AddScoped<GetByIdQuery<Invoice, InvoiceDto, BillingDbContext>>();
			services.AddScoped<GetAllQuery<Invoice, InvoiceDto, BillingDbContext>>();
			services.AddScoped<GetByIdsQuery<Invoice, InvoiceDto, BillingDbContext>>();
			services.AddScoped<SearchQuery<Invoice, InvoiceDto, BillingDbContext>>();
			services.AddScoped<GetByIdQuery<LineItem, LineItemDto, BillingDbContext>>();
			services.AddScoped<GetAllQuery<LineItem, LineItemDto, BillingDbContext>>();
			services.AddScoped<GetByIdsQuery<LineItem, LineItemDto, BillingDbContext>>();
			services.AddScoped<SearchQuery<LineItem, LineItemDto, BillingDbContext>>();

			// Register HTTP client for inter-module communication
			services.AddHttpClient();

			return services;
		}
		
		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			if (isDevelopment)
			{
				using (var scope = app.Services.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<BillingDbContext>();
					dbContext.Database.Migrate();
				}
			}
			return app;
		}
	}
}
