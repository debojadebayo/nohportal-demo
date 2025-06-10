using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;
using ComposedHealthBase.Server.Interfaces;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetByPredicateQuery<T, TDto, TContext>
    {
        Task<List<TDto>> Handle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[]? includes);
    }

    public class GetByPredicateQuery<T, TDto, TContext> : IGetByPredicateQuery<T, TDto, TContext>, IQuery
        where T : BaseEntity<T>
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;

        public GetByPredicateQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<List<TDto>> Handle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking().Where(predicate);
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entities = await query.ToListAsync();
            var authorizedEntities = new List<TDto>();
            foreach (var entity in entities)
            {
                var authResult = await _authorizationService.AuthorizeAsync(user: null, resource: entity, policyName: "resource-access"); // You may want to pass a ClaimsPrincipal here
                if (authResult.Succeeded)
                {
                    authorizedEntities.Add(_mapper.Map(entity));
                }
            }
            return authorizedEntities;
        }
    }
}
