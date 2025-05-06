using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;
using Shared.Enums;
using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
	public class ScheduleDto : BaseCalendarItem, IDto
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public int CreatedBy { get; set; }
		public int LastModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public long CustomerId { get; set; }
		public long ReferralId { get; set; }
		public long EmployeeId { get; set; }
		public long ClinicianId { get; set; }
		public long ProductId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public ScheduleStatusEnum Status { get; set; }
	}
}