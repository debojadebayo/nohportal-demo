using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Schedule.Entities;
using Server.Modules.Schedule.Infrastructure.Database;

namespace Server.Modules.Schedule.Infrastructure.Commands
{
	public class DeleteReferralCommand : IRequest<bool>
	{
		public long ReferralId { get; set; }
	}

	public class DeleteReferralHandler : IRequestHandler<DeleteReferralCommand, bool>
	{
		private readonly ScheduleDbContext _dbContext;
		private readonly IMapper _mapper;

		public DeleteReferralHandler(ScheduleDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<bool> Handle(DeleteReferralCommand request, CancellationToken cancellationToken)
		{
			var entity = await _dbContext.Referrals.FirstOrDefaultAsync(x => x.Id == request.ReferralId, cancellationToken);
			if (entity == null)
			{
				throw new Exception("Referral not found");
			}
			_dbContext.Referrals.Remove(entity);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}