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
    public class SearchEmployeesQuery
    {
        private readonly CRMDbContext _dbContext;
        private readonly IMapper<Employee, EmployeeDto> _mapper;
        private readonly string _term;

        public SearchEmployeesQuery(CRMDbContext dbContext, IMapper<Employee, EmployeeDto> mapper, string term)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _term = term;
        }

        public async Task<List<EmployeeDto>> Handle()
        {
            if (string.IsNullOrWhiteSpace(_term))
                return new List<EmployeeDto>();

            var term = _term.Trim().ToLower();
            var employees = _dbContext.Employees.AsQueryable();

            bool isId = long.TryParse(term, out var idValue);
            bool isDate = DateTime.TryParse(term, out var dobValue);

            var query = employees.Where(e =>
                (!string.IsNullOrEmpty(e.FirstName) && e.FirstName.ToLower().Contains(term)) ||
                (!string.IsNullOrEmpty(e.LastName) && e.LastName.ToLower().Contains(term)) ||
                (isId && e.Id == idValue) ||
                (isDate && e.DOB != null && e.DOB.Date == dobValue.Date)
            );

            var results = await query.ToListAsync();
            return results.Select(_mapper.Map).ToList();
        }
    }
}
