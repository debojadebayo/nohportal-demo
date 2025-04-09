using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Queries
{
    interface IGetAllQuery<T>
    {
        Task<IEnumerable<T>> Handle();
    }

    class GetAllQuery<T> : IGetAllQuery<T>
    where T : class
    {
        public IDbContext _dbContext { get; }

        public GetAllQuery(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> Handle()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}