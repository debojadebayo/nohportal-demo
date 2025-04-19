using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;

namespace Shared.DTOs.Scheduling
{
	public class ScheduleDto : BaseDto<ScheduleDto>
	{
		public long CustomerId { get; set; }
		public long ReferralId { get; set; }
		public long PatientId { get; set; }
		public long ClinicianId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		
	}
}