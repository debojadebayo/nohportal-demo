using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;
using ComposedHealthBase.Server.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Modules.CRM.Infrastructure.Database;

namespace Server.Modules.CRM.Infrastructure.Queries
{
    public class SearchCustomersQuery
    {
        private readonly CRMDbContext _dbContext;
        private readonly IMapper<Customer, CustomerDto> _mapper;
        private readonly string _term;

        public SearchCustomersQuery(CRMDbContext dbContext, IMapper<Customer, CustomerDto> mapper, string term)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _term = term;
        }

        public async Task<List<CustomerDto>> Handle()
        {
            if (string.IsNullOrWhiteSpace(_term))
                return new List<CustomerDto>();

            var term = _term.Trim().ToLower();
            bool isId = long.TryParse(term, out var idValue);

            var query = _dbContext.Customers.Where(c =>
                (!string.IsNullOrEmpty(c.Name) && c.Name.ToLower().Contains(term)) ||
                (isId && c.Id == idValue)
            );

            var results = await query.ToListAsync();
            return results.Select(_mapper.Map).ToList();
        }
    }
}
