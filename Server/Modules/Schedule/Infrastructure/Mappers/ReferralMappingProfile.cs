using AutoMapper;
using Server.Modules.Schedule.Entities;
using Shared.DTOs.Schedule;

namespace Server.Modules.Schedule.Infrastructure.Mappers
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

