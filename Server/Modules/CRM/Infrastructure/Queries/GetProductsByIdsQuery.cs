using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Server.Modules.CRM.Infrastructure.Database;
using Server.Modules.Shared.Contracts;
using Shared.DTOs.CRM;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.CRM.Infrastructure.Queries
{
    public class GetProductsByIdsQuery : IGetProductsByIdsQuery, IQuery
    {
        private readonly CRMDbContext _dbContext;
        private readonly IMapper<Product, ProductDto> _mapper;

        public GetProductsByIdsQuery(CRMDbContext dbContext, IMapper<Product, ProductDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(List<Guid> productIds)
        {
            var products = await _dbContext.Set<Product>()
                .AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            return products.Select(_mapper.Map).ToList();
        }
    }
}
