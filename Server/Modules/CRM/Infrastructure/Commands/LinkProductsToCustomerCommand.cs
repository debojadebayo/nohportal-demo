using AutoMapper;
using MediatR;
using Server.Modules.CRM.Entities;
using Server.Modules.CRM.Infrastructure.Database;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Commands
{
	public class LinkProductsToCustomerCommand : IRequest<bool>
	{
		public int CustomerId { get; set; }
		public List<ProductDto>? Products { get; set; }
	}

	public class LinkProductsToCustomerHandler : IRequestHandler<LinkProductsToCustomerCommand, bool>
	{
		private readonly CRMDbContext _dbContext;
		private readonly IMapper _mapper;

		public LinkProductsToCustomerHandler(CRMDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<bool> Handle(LinkProductsToCustomerCommand request, CancellationToken cancellationToken)
		{
			var customer = await _dbContext.NOHCustomers.FindAsync(request.CustomerId);

			if (customer == null)
				return false;

			var products = _mapper.Map<HashSet<Product>>(request.Products);
			customer.Products.UnionWith(products);

			await _dbContext.SaveChangesAsync(cancellationToken);
			return true;
		}
	}

}
