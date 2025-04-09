using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.Scheduling
{
	public class ScheduleDto : BaseDto<ScheduleDto>, IDto
	{
		public long Id { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}