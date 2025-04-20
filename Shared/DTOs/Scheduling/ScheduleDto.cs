using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
	public class ScheduleDto : BaseDto<ScheduleDto>, ICalendarItem
	{
		public long CustomerId { get; set; }
		public long ReferralId { get; set; }
		public long PatientId { get; set; }
		public long ClinicianId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}