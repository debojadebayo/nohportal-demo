using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;
using Shared.Enums;
using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class ScheduleDto : BaseCalendarItem, IDto, IAuditDto, ILazyLookup
    {
        public new Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid TenantId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ReferralId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ClinicianId { get; set; }
        public Guid ProductId { get; set; }
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
        public AppointmentStatusEnum AppointmentStatus { get; set; }
        public string DisplayName => $"{Title} - {Description}";
    }
}