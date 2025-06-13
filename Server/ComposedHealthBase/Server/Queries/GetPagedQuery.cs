using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;
using ComposedHealthBase.Server.Interfaces;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetPagedQuery<T, TDto, TContext>
    {
        Task<IEnumerable<TDto>> Handle(int page, int pageSize, params Expression<Func<T, object>>[]? includes);
    }

    public class GetPagedQuery<T, TDto, TContext> : IGetPagedQuery<T, TDto, TContext>, IQuery
        where T : class, IAuditEntity
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;

        public GetPagedQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }
        public async Task<IEnumerable<TDto>> Handle(int page, int pageSize, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking();
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entities = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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