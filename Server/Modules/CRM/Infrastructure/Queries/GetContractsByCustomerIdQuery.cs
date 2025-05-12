using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;
using ComposedHealthBase.Server.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Modules.CRM.Infrastructure.Database;

namespace Server.Modules.CRM.Infrastructure.Queries
{
    public class GetContractsByCustomerIdQuery
    {
        private readonly CRMDbContext _dbContext;
        private readonly IMapper<Contract, ContractDto> _mapper;
        private readonly long _customerId;

        public GetContractsByCustomerIdQuery(CRMDbContext dbContext, IMapper<Contract, ContractDto> mapper, long customerId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _customerId = customerId;
        }

        public async Task<List<ContractDto>> Handle()
        {
            var customer = await _dbContext.Customers
                .Include(c => c.Contracts)
                    .ThenInclude(co => co.Products) // Include Products within Contracts
                .FirstOrDefaultAsync(c => c.Id == _customerId);

            if (customer == null || customer.Contracts == null)
            {
                return new List<ContractDto>();
            }

            return customer.Contracts.Select(_mapper.Map).ToList();
        }
    }
}