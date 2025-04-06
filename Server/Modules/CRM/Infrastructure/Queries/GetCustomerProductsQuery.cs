using AutoMapper;
using MediatR;
using Server.Modules.CRM.Infrastructure.Database;
using Shared.DTOs.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modules.CRM.Infrastructure.Queries
{
	public class GetCustomerProductsQuery : IRequest<List<ProductDto>>
	{
		public int CustomerId { get; set; }
	}

	public class GetCustomerProductsHandler : IRequestHandler<GetCustomerProductsQuery, List<ProductDto>>
	{
		private readonly CRMDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetCustomerProductsHandler(CRMDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<List<ProductDto>> Handle(GetCustomerProductsQuery request, CancellationToken cancellationToken)
		{
			var customer = await _dbContext.NOHCustomers.FindAsync(request.CustomerId);

			if (customer == null)
				throw new KeyNotFoundException("Customer not found.");

			return _mapper.Map<List<ProductDto>>(customer.Products);
		}
	}

}
