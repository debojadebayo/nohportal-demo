using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;

namespace ComposedHealthBase.Server.Queries
{
    public interface ISearchQuery<T, TDto, TContext>
    {
        Task<IEnumerable<TDto>> Handle(ClaimsPrincipal user, string searchTerm, Guid? tenantId = null, Guid? subjectId = null, params Expression<Func<T, object>>[]? includes);
    }

    public class SearchQuery<T, TDto, TContext> : ISearchQuery<T, TDto, TContext>, IQuery
        where T : class, IEntity, IAuditEntity
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;

        public SearchQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<IEnumerable<TDto>> Handle(ClaimsPrincipal user, string searchTerm, Guid? tenantId = null, Guid? subjectId = null, params Expression<Func<T, object>>[]? includes)
        {

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<TDto>();
            }
            var query = _dbContext.Set<T>().AsQueryable();

            if (tenantId != null)
            {
                query = query.Where(e => e.TenantId == tenantId);
            }
            if (subjectId != null)
            {
                query = query.Where(e => e.SubjectId == subjectId);
            }

            query = query.AsNoTracking();
            if (typeof(ISearchTags).IsAssignableFrom(typeof(T)))
            {
                var loweredTerm = searchTerm.ToLower();
                query = query.Where(c =>
                    EF.Property<string>(c, "SearchTags") != null &&
                    EF.Property<string>(c, "SearchTags").ToLower().Contains(loweredTerm)
                );
            }
            else
            {
                throw new InvalidOperationException($"The type {typeof(T).Name} is not searchable.");
            }
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
                var authResult = await _authorizationService.AuthorizeAsync(user, entity, "resource-access");
                if (authResult.Succeeded)
                {
                    authorizedEntities.Add(_mapper.Map(entity));
                }
            }
            return authorizedEntities;
        }
    }
}
