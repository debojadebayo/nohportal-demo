using AutoMapper;
using MediatR;
using Server.Modules.Schedule.Entities;
using Server.Modules.Schedule.Infrastructure.Database;
using Shared.DTOs.Schedule;

namespace Server.Modules.Schedule.Infrastructure.Commands
{
	public class CreateReferralCommand : IRequest<long>
	{
		public ReferralDto Referral { get; set; }
	}

	public class CreateReferralHandler : IRequestHandler<CreateReferralCommand, long>
	{
		private readonly ScheduleDbContext _dbContext;
		private readonly IMapper _mapper;

		public CreateReferralHandler(ScheduleDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<long> Handle(CreateReferralCommand request, CancellationToken cancellationToken)
		{
			var newReferral = _mapper.Map<Referral>(request.Referral);

			_dbContext.Referrals.Add(newReferral);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return newReferral.Id;
		}
	}
}