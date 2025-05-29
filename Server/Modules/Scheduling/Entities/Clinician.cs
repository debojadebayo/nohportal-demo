using ComposedHealthBase.Server.Entities;
using Server.Modules.CommonModule.Interfaces;
using Shared.Enums;

namespace Server.Modules.Scheduling.Entities
{
	public class Clinician : BaseEntity<Clinician>, IEntity, INOHEntity
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Telephone { get; set; }
		public required string Email { get; set; }
		public ClinicianTypeEnum ClinicianType { get; set; }
		public RegulatorTypeEnum RegulatorType { get; set; }
		public required string LicenceNumber { get; set; }
		public required string AvatarImage { get; set; }
		public required string AvatarTitle { get; set; }
		public required string AvatarDescription { get; set; }
		public HashSet<Schedule> CalendarItems { get; set; } = new HashSet<Schedule>();
		public long CustomerId
		{
			get
			{
				return TenantId;
			}
			set
			{
				TenantId = value;
			}
		}
		public long EmployeeId
		{
			get
			{
				return SubjectId;
			}
			set
			{
				SubjectId = value;
			}
		}
	}
}