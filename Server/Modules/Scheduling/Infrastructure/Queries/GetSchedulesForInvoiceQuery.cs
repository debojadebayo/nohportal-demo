using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Server.Interfaces;
using Shared.DTOs.Scheduling;
using Server.Modules.Scheduling.Infrastructure.Database;
using Server.Modules.Scheduling.Entities;
using Server.Modules.Shared.Contracts;

namespace Server.Modules.Scheduling.Infrastructure.Queries
{
    public class GetSchedulesForInvoiceQuery : IGetSchedulesForInvoiceQuery
    {
        private readonly SchedulingDbContext _dbContext;
        private readonly IMapper<Schedule, ScheduleDto> _mapper;

        public GetSchedulesForInvoiceQuery(SchedulingDbContext dbContext, IMapper<Schedule, ScheduleDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDto>> Handle(Guid customerId, DateTime fromDate, DateTime toDate, Guid? productId = null)
        {
            // Convert to UTC to ensure PostgreSQL compatibility
            var fromDateUtc = fromDate.Kind == DateTimeKind.Utc ? fromDate : fromDate.ToUniversalTime();
            var toDateUtc = toDate.Kind == DateTimeKind.Utc ? toDate : toDate.ToUniversalTime();

            var schedules = await _dbContext.Set<Schedule>()
                .Where(s => s.CustomerId == customerId &&
                            s.Start >= fromDateUtc &&
                            s.Start <= toDateUtc &&
                            (productId == null || productId == Guid.Empty || s.ProductId == productId.Value)).ToListAsync();

            return _mapper.Map(schedules);
        }
    }
}
