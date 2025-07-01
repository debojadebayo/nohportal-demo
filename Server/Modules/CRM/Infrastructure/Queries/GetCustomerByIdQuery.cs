using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Server.Modules.CRM.Infrastructure.Database;
using Server.Modules.Shared.Contracts;
using Shared.DTOs.CRM;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.CRM.Infrastructure.Queries
{
    public class GetCustomerByIdQuery : IGetCustomerByIdQuery, IQuery
    {
        private readonly CRMDbContext _dbContext;
        private readonly IMapper<Customer, CustomerDto> _mapper;

        public GetCustomerByIdQuery(CRMDbContext dbContext, IMapper<Customer, CustomerDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CustomerDto?> Handle(Guid customerId)
        {
            var customer = await _dbContext.Set<Customer>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == customerId);

            return customer != null ? _mapper.Map(customer) : null;
        }
    }
}
