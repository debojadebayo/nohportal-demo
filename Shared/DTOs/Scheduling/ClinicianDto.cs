using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;
using Shared.Enums;

namespace Shared.DTOs.Scheduling
{
	public class ClinicianDto : ICalendarResource<ScheduleDto>, IDto
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public int CreatedBy { get; set; }
		public int LastModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Telephone { get; set; }
		public string Email { get; set; }
		public ClinicianTypeEnum ClinicianType { get; set; }
		public RegulatorTypeEnum RegulatorType { get; set; }
		public string LicenceNumber { get; set; }
		public IEnumerable<ScheduleDto> CalendarItems { get; set; } = new List<ScheduleDto>();
		public string AvatarImage { get; set; }
		public string AvatarTitle { get; set; }
		public string AvatarDescription { get; set; }
	}
}