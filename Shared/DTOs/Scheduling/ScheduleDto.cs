using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;
using Shared.Enums;
using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class ScheduleDto : BaseCalendarItem, IDto
    {
        public new long Id { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long TenantId { get; set; }
        public long CustomerId { get; set; }
        public long ReferralId { get; set; }
        public long EmployeeId { get; set; }
        public long ClinicianId { get; set; }
        public long ProductId { get; set; }
        public required string Title
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
        public required string Description { get; set; }
        public ScheduleStatusEnum Status { get; set; }
    }
}