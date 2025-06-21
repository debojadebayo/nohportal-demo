using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;
using ComposedHealthBase.Server.Interfaces;
using System.Security.Claims;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetSingleQuery<T, TDto, TContext>
    {
        Task<TDto?> Handle(Expression<Func<T, bool>> predicate, ClaimsPrincipal user, long tenantId = 0, long subjectId = 0, params Expression<Func<T, object>>[]? includes);
    }

    public class GetSingleQuery<T, TDto, TContext> : IGetSingleQuery<T, TDto, TContext>, IQuery
        where T : class, IAuditEntity
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;

        public GetSingleQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }
        public async Task<TDto?> Handle(Expression<Func<T, bool>> predicate, ClaimsPrincipal user, long tenantId = 0, long subjectId = 0, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (tenantId != 0)
            {
                query = query.Where(e => e.TenantId == tenantId);
            }
            if (subjectId != 0)
            {
                query = query.Where(e => e.SubjectId == subjectId);
            }
            query = query.AsNoTracking();
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.FirstOrDefaultAsync(predicate);
            if (entity == null)
            {
                return default;
            }
            var authResult = await _authorizationService.AuthorizeAsync(user, entity, "resource-access");
            if (!authResult.Succeeded)
            {
                return default;
            }
            return _mapper.Map(entity);
        }
    }
}