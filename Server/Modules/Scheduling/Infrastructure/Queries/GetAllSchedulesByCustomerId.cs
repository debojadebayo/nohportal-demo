
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Queries;
using Server.Modules.Scheduling.Entities;
using Shared.DTOs.Scheduling;
using Server.Modules.Scheduling.Infrastructure.Database;

namespace Server.Modules.Scheduling.Infrastructure.Queries
{
    public class GetAllSchedulesByCustomerId : IGetAllQuery<Schedule, ScheduleDto, SchedulingDbContext>
    {
        private readonly IDbContext<SchedulingDbContext> _dbContext;
        private readonly IMapper<Schedule, ScheduleDto> _mapper;
        private readonly long _customerId;

        public GetAllSchedulesByCustomerId(IDbContext<SchedulingDbContext> dbContext, IMapper<Schedule, ScheduleDto> mapper, long customerId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _customerId = customerId;
        }

        public async Task<IEnumerable<ScheduleDto>> Handle()
        {
            var schedules = await _dbContext.Set<Schedule>()
                .Where(s => s.CustomerId == _customerId)
                .ToListAsync();

            return _mapper.Map(schedules);
        }
    }
}