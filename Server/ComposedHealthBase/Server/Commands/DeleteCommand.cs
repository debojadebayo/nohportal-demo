using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ComposedHealthBase.Server.Database;

namespace ComposedHealthBase.Server.Commands
{
    public interface IDeleteCommand
    {
        Task<bool> Handle(long id, ClaimsPrincipal user);
    }
    public class DeleteCommand<T, TContext> : IDeleteCommand
    where T : class
    where TContext : IDbContext<TContext>
    {
        private readonly IDbContext<TContext> _dbContext;

        public DeleteCommand(IDbContext<TContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(long id, ClaimsPrincipal user)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesWithAuditAsync(user);
            return true;
        }
    }
}
