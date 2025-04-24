
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
    public class GetAllCliniciansWithSchedulesQuery : IGetAllQuery<Clinician, ClinicianDto, SchedulingDbContext>
    {
        private readonly IDbContext<SchedulingDbContext> _dbContext;
        private readonly IMapper<Clinician, ClinicianDto> _mapper;

        public GetAllCliniciansWithSchedulesQuery(IDbContext<SchedulingDbContext> dbContext, IMapper<Clinician, ClinicianDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClinicianDto>> Handle()
        {
            var clinicians = await _dbContext.Set<Clinician>()
                .Include(c => c.CalendarItems)
                .ToListAsync();

            return _mapper.Map(clinicians);
        }
    }
}