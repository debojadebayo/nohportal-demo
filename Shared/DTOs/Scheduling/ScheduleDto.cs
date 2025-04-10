using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.Scheduling
{
	public class ScheduleDto : BaseDto<ScheduleDto>, IDto
	{
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}