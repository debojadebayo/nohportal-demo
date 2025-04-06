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
	public class GetCustomerByIdQuery : IRequest<CustomerDto>
	{
		public int Id { get; set; }
	}

	public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
	{
		private readonly CRMDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetCustomerByIdHandler(CRMDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
		{
			var customer = await _dbContext.NOHCustomers.FindAsync(request.Id);

			if (customer == null)
				throw new KeyNotFoundException("Customer not found.");

			return _mapper.Map<CustomerDto>(customer);
		}
	}

}
