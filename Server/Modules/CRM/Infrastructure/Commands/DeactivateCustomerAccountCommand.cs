using MediatR;
using Server.Modules.CRM.Infrastructure.Database;

namespace Server.Modules.CRM.Infrastructure.Commands
{
	public class DeactivateCustomerAccountCommand : IRequest<bool>
	{
		public int CustomerId { get; set; }
	}

	public class DeactivateCustomerAccountHandler : IRequestHandler<DeactivateCustomerAccountCommand, bool>
	{
		private readonly CRMDbContext _dbContext;

		public DeactivateCustomerAccountHandler(CRMDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<bool> Handle(DeactivateCustomerAccountCommand request, CancellationToken cancellationToken)
		{
			var customer = await _dbContext.NOHCustomers.FindAsync(request.CustomerId);

			if (customer == null)
				return false;

			customer.IsActive = false;
			await _dbContext.SaveChangesAsync(cancellationToken);

			return true;
		}
	}

}
