using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;
using ComposedHealthBase.Server.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Modules.CRM.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;

namespace Server.Modules.CRM.Infrastructure.Queries
{
    public interface ISearchEmployeesQuery
    {
        Task<List<EmployeeDto>> Handle(ClaimsPrincipal user, string term);
    }

    public class SearchEmployeesQuery : ISearchEmployeesQuery, IQuery
    {
        private readonly CRMDbContext _dbContext;
        private readonly IMapper<Employee, EmployeeDto> _mapper;
        private readonly IAuthorizationService _authorizationService;

        public SearchEmployeesQuery(
            CRMDbContext dbContext,
            IMapper<Employee, EmployeeDto> mapper,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<List<EmployeeDto>> Handle(ClaimsPrincipal user, string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return new List<EmployeeDto>();

            var searchTerm = term.Trim().ToLower();
            var employees = _dbContext.Employees.AsQueryable();

            bool isId = long.TryParse(searchTerm, out var idValue);
            bool isDate = DateTime.TryParse(searchTerm, out var dobValue);

            var query = employees.Where(e =>
                (!string.IsNullOrEmpty(e.FirstName) && e.FirstName.ToLower().Contains(searchTerm)) ||
                (!string.IsNullOrEmpty(e.LastName) && e.LastName.ToLower().Contains(searchTerm)) ||
                (isId && e.Id == idValue) ||
                (isDate && e.DOB != null && e.DOB.Value.Date == dobValue.Date)
            );

            var results = await query.ToListAsync();
            var authorizedResults = new List<EmployeeDto>();
            foreach (var entity in results)
            {
                var authResult = await _authorizationService.AuthorizeAsync(user, entity, "resource-access");
                if (authResult.Succeeded)
                {
                    authorizedResults.Add(_mapper.Map(entity));
                }
            }
            return authorizedResults;
        }
    }
}
