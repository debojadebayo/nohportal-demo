using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Schedule.Infrastructure.Database;
using Shared.DTOs.Schedule;

namespace Server.Modules.Schedule.Infrastructure.Queries
{
	public class GetAllReferralsQuery : IRequest<List<ReferralDto>>
	{
	}

	public class GetAllReferralsHandler : IRequestHandler<GetAllReferralsQuery, List<ReferralDto>>
	{
		private readonly ScheduleDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetAllReferralsHandler(ScheduleDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<List<ReferralDto>> Handle(GetAllReferralsQuery request, CancellationToken cancellationToken)
		{
			var referrals = await _dbContext.Referrals.ToListAsync(cancellationToken);
			return _mapper.Map<List<ReferralDto>>(referrals);
		}
	}

}
