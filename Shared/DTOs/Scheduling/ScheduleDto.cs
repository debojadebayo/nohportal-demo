namespace Shared.DTOs.Scheduling
{
	public class ScheduleDto : IDto
	{
		public long Id { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}