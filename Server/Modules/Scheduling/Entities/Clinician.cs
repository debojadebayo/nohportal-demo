using ComposedHealthBase.Server.Entities;

using Shared.Enums;
using ComposedHealthBase.Shared.Interfaces;

namespace Server.Modules.Scheduling.Entities
{
    public class Clinician : BaseEntity<Clinician>, IEntity, IAuditEntity, ISearchTags, ISubject
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? UserName { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
        public string? AvatarImage { get; set; }
        public string? AvatarTitle { get; set; }
        public string? AvatarDescription { get; set; }
        public required Guid KeycloakId { get; set; }
        public ClinicianTypeEnum ClinicianType { get; set; }
        public RegulatorTypeEnum RegulatorType { get; set; }
        public required string LicenceNumber { get; set; }
        public HashSet<Schedule> CalendarItems { get; set; } = new HashSet<Schedule>();
        public string SearchTags { get; set; } = string.Empty;
    }
}