using MediatR;
using Server.Modules.CRM.Infrastructure.Database;

namespace Server.Modules.CRM.Infrastructure.Commands
{
	public class DeleteCustomerCommand : IRequest<bool>
	{
		public int Id { get; set; }
	}

	public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
	{
		private readonly CRMDbContext _dbContext;

		public DeleteCustomerHandler(CRMDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
		{
			var customer = await _dbContext.NOHCustomers.FindAsync(request.Id);

			if (customer == null)
				return false;

			_dbContext.NOHCustomers.Remove(customer);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return true;
		}
	}

}
