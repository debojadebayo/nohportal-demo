using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Interfaces;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetByIdQuery<T, TDto, TContext>
    {
        Task<TDto?> Handle(long id, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes);
    }

    public class GetByIdQuery<T, TDto, TContext> : IGetByIdQuery<T, TDto, TContext>, IQuery
        where T : BaseEntity<T>
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;

        public GetByIdQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<TDto?> Handle(long id, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking().Where(x => x.Id == id);
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.SingleOrDefaultAsync();
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found");
            }
            var authResult = await _authorizationService.AuthorizeAsync(user, entity, "resource-access");
            if (!authResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Authorization failed for resource-access policy.");
            }
            return _mapper.Map(entity);
        }
    }
}