using AutoMapper;
using MediatR;
using Server.Modules.CRM.Entities;
using Server.Modules.CRM.Infrastructure.Database;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Commands
{
	public class CreateCustomerCommand : IRequest<long>
	{
		public int Id { get; set; } // Unique identifier for the customer
		public string? Name { get; set; } // Name of the customer organization
		public string? Address { get; set; } // Physical address of the customer
		public string? Email { get; set; } // Contact email
		public string? PhoneNumber { get; set; } // Contact phone number
		public string? ContactName { get; set; } // Main point of contact
		public List<ProductDto>? Products { get; set; } // List of products purchased by the customer
		public List<ContractDto>? Contracts { get; set; } // List of contracts associated with the customer
		public string? Notes { get; set; } // Additional notes or information about the customer
	}

	public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, long>
	{
		private readonly CRMDbContext _dbContext;
		private readonly IMapper _mapper;

		public CreateCustomerHandler(CRMDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			// Map the command properties to the NOHCustomer entity
			var newCustomer = _mapper.Map<NOHCustomer>(request);

			// Add the new customer to the database
			_dbContext.NOHCustomers.Add(newCustomer);
			await _dbContext.SaveChangesAsync(cancellationToken);

			// Return the ID of the newly created customer
			return newCustomer.Id;
		}
	}
}
