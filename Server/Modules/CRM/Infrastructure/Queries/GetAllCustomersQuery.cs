using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Modules.CRM.Infrastructure.Database;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Queries
{
	public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
	{
	}

	public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
	{
		private readonly CRMDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetAllCustomersHandler(CRMDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
		{
			var customers = await _dbContext.NOHCustomers.ToListAsync(cancellationToken);
			return _mapper.Map<List<CustomerDto>>(customers);
		}
	}

}
