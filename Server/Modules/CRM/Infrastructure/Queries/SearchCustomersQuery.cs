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
    public interface ISearchCustomersQuery
    {
        Task<List<CustomerDto>> Handle(ClaimsPrincipal user, string term);
    }

    public class SearchCustomersQuery : ISearchCustomersQuery, IQuery
    {
        private readonly CRMDbContext _dbContext;
        private readonly IMapper<Customer, CustomerDto> _mapper;
        private readonly IAuthorizationService _authorizationService;

        public SearchCustomersQuery(
            CRMDbContext dbContext,
            IMapper<Customer, CustomerDto> mapper,
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<List<CustomerDto>> Handle(ClaimsPrincipal user, string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return new List<CustomerDto>();

            var searchTerm = term.Trim().ToLower();
            bool isId = long.TryParse(searchTerm, out var idValue);

            var query = _dbContext.Customers.Where(c =>
                (!string.IsNullOrEmpty(c.Name) && c.Name.ToLower().Contains(searchTerm)) ||
                (isId && c.Id == idValue)
            );

            var results = await query.ToListAsync();
            var authorizedResults = new List<CustomerDto>();
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
