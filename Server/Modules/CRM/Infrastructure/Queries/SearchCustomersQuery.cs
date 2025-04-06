using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Modules.CRM.Infrastructure.Database;
using Shared.DTOs.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modules.CRM.Infrastructure.Queries
{
	public class SearchCustomersQuery : IRequest<List<CustomerDto>>
	{
		public string? Name { get; set; }
		public string? Email { get; set; }
	}

	public class SearchCustomersHandler : IRequestHandler<SearchCustomersQuery, List<CustomerDto>>
	{
		private readonly CRMDbContext _dbContext;
		private readonly IMapper _mapper;

		public SearchCustomersHandler(CRMDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<List<CustomerDto>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
		{
			var query = _dbContext.NOHCustomers.AsQueryable();

			if (!string.IsNullOrEmpty(request.Name))
			{
				query = query.Where(c => c.Name.Contains(request.Name));
			}

			if (!string.IsNullOrEmpty(request.Email))
			{
				query = query.Where(c => c.Email.Contains(request.Email));
			}

			var customers = await query.ToListAsync(cancellationToken);
			return _mapper.Map<List<CustomerDto>>(customers);
		}
	}

}
