using System;
using System.Threading.Tasks;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace ComposedHealthBase.Server.Queries
{
    public interface IGetUserByKeycloakIdQuery<T, TContext>
    {
        Task<T?> Handle(Guid keycloakId);
    }

    public class GetUserByKeycloakIdQuery<T, TContext> : IGetUserByKeycloakIdQuery<IApplicationUser, TContext>, IQuery
        where T : IApplicationUser
        where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }

        public GetUserByKeycloakIdQuery(IDbContext<TContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IApplicationUser?> Handle(Guid keycloakId)
        {
            if (keycloakId == Guid.Empty)
            {
                return null;
            }
            var entity = await _dbContext.Set<IApplicationUser>().AsNoTracking().FirstOrDefaultAsync(e => e.KeycloakId == keycloakId);

            return entity;
        }
    }
}