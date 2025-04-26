
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Queries;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;
using Server.Modules.CRM.Infrastructure.Database;

namespace Server.Modules.CRM.Infrastructure.Queries
{
    public class GetEmployeesByCustomerQuery : IGetAllQuery<Employee, EmployeeDto, CRMDbContext>
    {
        private readonly IDbContext<CRMDbContext> _dbContext;
        private readonly IMapper<Employee, EmployeeDto> _mapper;
        private readonly long _customerId;

        public GetEmployeesByCustomerQuery(IDbContext<CRMDbContext> dbContext, IMapper<Employee, EmployeeDto> mapper, long customerId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _customerId = customerId;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle()
        {
            var employees = await _dbContext.Set<Employee>()
                .Where(e => e.CustomerId == _customerId)
                .ToListAsync();

            return _mapper.Map(employees);
        }
    }
}