using ComposedHealthBase.Server.Infrastructure.Database;

namespace ComposedHealthBase.Server.Commands
{
    public interface IDeleteCommand
    {
        Task<bool> Handle(long id);
    }
    public class DeleteCommand<T> : IDeleteCommand
    where T : class
    {
        private readonly IDbContext _dbContext;

        public DeleteCommand(IDbContext dbContext)
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
