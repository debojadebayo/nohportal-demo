using AutoMapper;
using Server.Modules.Scheduling.Entities;
using Shared.DTOs.Scheduling;

namespace Server.Modules.Scheduling.Infrastructure.Mappers
{
	public class ReferralMappingProfile : Profile
	{
		public ReferralMappingProfile()
		{
			CreateMap<ReferralDto, Referral>()
				.ReverseMap();
		}
	}
}

