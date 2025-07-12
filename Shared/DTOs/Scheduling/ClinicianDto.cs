using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;
using Shared.Enums;

namespace Shared.DTOs.Scheduling
{
    public class ClinicianDto : BaseDto<ClinicianDto>, ICalendarResource<ScheduleDto>, IDto, IAuditDto, ILazyLookup, ISubject
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Username { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
        public ClinicianTypeEnum ClinicianType { get; set; }
        public RegulatorTypeEnum RegulatorType { get; set; }
        public required string LicenceNumber { get; set; }
        public IEnumerable<ScheduleDto> CalendarItems { get; set; } = new List<ScheduleDto>();
        public required string AvatarImage { get; set; }
        public required string AvatarTitle { get; set; }
        public required string AvatarDescription { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public string? RoleName { get; set; }
    }
}