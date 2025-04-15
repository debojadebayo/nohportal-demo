using ComposedHealthBase.Server.Database;

namespace ComposedHealthBase.Server.Commands
{
    public interface IDeleteCommand
    {
        Task<bool> Handle(long id);
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

        public async Task<bool> Handle(long id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
