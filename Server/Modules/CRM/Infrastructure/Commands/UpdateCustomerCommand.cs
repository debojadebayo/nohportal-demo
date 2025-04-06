using AutoMapper;
using MediatR;
using Server.Modules.CRM.Infrastructure.Database;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Commands
{
	public class UpdateCustomerCommand : IRequest<bool>
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? ContactName { get; set; }
		public List<ProductDto>? Products { get; set; }
		public List<ContractDto>? Contracts { get; set; }
		public string? Notes { get; set; }
	}

	public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
	{
		private readonly CRMDbContext _dbContext;
		private readonly IMapper _mapper;

		public UpdateCustomerHandler(CRMDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
		{
			var existingCustomer = await _dbContext.NOHCustomers.FindAsync(request.Id);

			if (existingCustomer == null)
				return false;

			_mapper.Map(request, existingCustomer);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
