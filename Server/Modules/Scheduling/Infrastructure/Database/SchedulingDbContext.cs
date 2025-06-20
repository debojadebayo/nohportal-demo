using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Scheduling.Entities;

namespace Server.Modules.Scheduling.Infrastructure.Database
{
	public sealed class SchedulingDbContext(DbContextOptions<SchedulingDbContext> options) : BaseDbContext<SchedulingDbContext>(options), IDbContext<SchedulingDbContext>
	{
		public DbSet<Clinician> Clinicians { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Referral> Referrals { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema.Scheduling);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingDbContext).Assembly);
			modelBuilder.Entity<Clinician>()

				.HasData(
					new Clinician { Id = 1, KeycloakId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), FirstName = "Alice", LastName = "Smith", Telephone = "555-1001", Email = "alice.smith@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.SeniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1001", AvatarImage = "https://randomuser.me/api/portraits/women/1.jpg", AvatarTitle = "Dr. Alice Smith", AvatarDescription = "Senior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 2, KeycloakId = new Guid("9c8b7a6d-5e4f-3c2b-1a09-876543210fed"), FirstName = "Bob", LastName = "Johnson", Telephone = "555-1002", Email = "bob.johnson@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.JuniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1002", AvatarImage = "https://randomuser.me/api/portraits/men/2.jpg", AvatarTitle = "Dr. Bob Johnson", AvatarDescription = "Junior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 3, KeycloakId = new Guid("123e4567-e89b-12d3-a456-426614174000"), FirstName = "Carol", LastName = "Williams", Telephone = "555-1003", Email = "carol.williams@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.Nurse, RegulatorType = Shared.Enums.RegulatorTypeEnum.NMC, LicenceNumber = "NMC1003", AvatarImage = "https://randomuser.me/api/portraits/women/3.jpg", AvatarTitle = "Nurse Carol Williams", AvatarDescription = "Nurse, NMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 4, KeycloakId = new Guid("ba012345-6789-abcd-0123-456789abcdef"), FirstName = "David", LastName = "Brown", Telephone = "555-1004", Email = "david.brown@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.SeniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1004", AvatarImage = "https://randomuser.me/api/portraits/men/4.jpg", AvatarTitle = "Dr. David Brown", AvatarDescription = "Senior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 5, KeycloakId = new Guid("00112233-4455-6677-8899-aabbccddeeff"), FirstName = "Eva", LastName = "Jones", Telephone = "555-1005", Email = "eva.jones@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.Nurse, RegulatorType = Shared.Enums.RegulatorTypeEnum.NMC, LicenceNumber = "NMC1005", AvatarImage = "https://randomuser.me/api/portraits/women/5.jpg", AvatarTitle = "Nurse Eva Jones", AvatarDescription = "Nurse, NMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 6, KeycloakId = new Guid("ffeeddcc-bbaa-9988-7766-554433221100"), FirstName = "Frank", LastName = "Garcia", Telephone = "555-1006", Email = "frank.garcia@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.JuniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1006", AvatarImage = "https://randomuser.me/api/portraits/men/6.jpg", AvatarTitle = "Dr. Frank Garcia", AvatarDescription = "Junior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 7, KeycloakId = new Guid("abcdef01-2345-6789-abcd-ef0123456789"), FirstName = "Grace", LastName = "Martinez", Telephone = "555-1007", Email = "grace.martinez@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.Nurse, RegulatorType = Shared.Enums.RegulatorTypeEnum.NMC, LicenceNumber = "NMC1007", AvatarImage = "https://randomuser.me/api/portraits/women/7.jpg", AvatarTitle = "Nurse Grace Martinez", AvatarDescription = "Nurse, NMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 8, KeycloakId = new Guid("fedcba98-7654-3210-fedc-ba9876543210"), FirstName = "Henry", LastName = "Lee", Telephone = "555-1008", Email = "henry.lee@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.SeniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1008", AvatarImage = "https://randomuser.me/api/portraits/men/8.jpg", AvatarTitle = "Dr. Henry Lee", AvatarDescription = "Senior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 9, KeycloakId = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), FirstName = "Ivy", LastName = "Walker", Telephone = "555-1009", Email = "ivy.walker@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.Nurse, RegulatorType = Shared.Enums.RegulatorTypeEnum.NMC, LicenceNumber = "NMC1009", AvatarImage = "https://randomuser.me/api/portraits/women/9.jpg", AvatarTitle = "Nurse Ivy Walker", AvatarDescription = "Nurse, NMC", CreatedBy = "System", LastModifiedBy = "System" },
					new Clinician { Id = 10, KeycloakId = new Guid("a1b2c3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"), FirstName = "Jack", LastName = "Hall", Telephone = "555-1010", Email = "jack.hall@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.JuniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1010", AvatarImage = "https://randomuser.me/api/portraits/men/10.jpg", AvatarTitle = "Dr. Jack Hall", AvatarDescription = "Junior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System" }
					);
			modelBuilder.Entity<Referral>()
				.HasData(
					new Referral { Id = 1, CustomerId = 1, EmployeeId = 1, EmployeeDocumentId=0, ReferralDetails = "Routine checkup for hypertension.", Title = "Hypertension Checkup", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 2, CustomerId = 1, EmployeeId = 2, EmployeeDocumentId=0, ReferralDetails = "Follow-up for diabetes management.", Title = "Diabetes Follow-up", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 3, CustomerId = 1, EmployeeId = 3, EmployeeDocumentId=0, ReferralDetails = "Initial consultation for back pain.", Title = "Back Pain Consultation", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 4, CustomerId = 2, EmployeeId = 4, EmployeeDocumentId=0, ReferralDetails = "Annual physical examination.", Title = "Annual Physical Exam", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 5, CustomerId = 2, EmployeeId = 5, EmployeeDocumentId=0, ReferralDetails = "Referral for allergy testing.", Title = "Allergy Testing", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 6, CustomerId = 2, EmployeeId = 6, EmployeeDocumentId=0, ReferralDetails = "Consultation for asthma symptoms.", Title = "Asthma Consultation", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 7, CustomerId = 3, EmployeeId = 7, EmployeeDocumentId=0, ReferralDetails = "Pre-surgery evaluation.", Title = "Pre-Surgery Evaluation", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 8, CustomerId = 3, EmployeeId = 8, EmployeeDocumentId=0, ReferralDetails = "Post-operative follow-up.", Title = "Post-Op Follow-up", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 9, CustomerId = 3, EmployeeId = 9, EmployeeDocumentId=0, ReferralDetails = "Referral for physical therapy.", Title = "Physical Therapy Referral", CreatedBy = "System", LastModifiedBy = "System" },
					new Referral { Id = 10, CustomerId = 3, EmployeeId = 10, EmployeeDocumentId=0, ReferralDetails = "Consultation for migraine headaches.", Title = "Migraine Consultation", CreatedBy = "System", LastModifiedBy = "System" }
						);
			modelBuilder.Entity<Schedule>()
				.HasData(
					new Schedule { Id = 1, ClinicianId = 1, CustomerId = 1, ReferralId = 1, EmployeeId = 1, Start = new DateTime(2025, 4, 22, 9, 0, 0, DateTimeKind.Utc), End = new DateTime(2025, 4, 22, 10, 0, 0, DateTimeKind.Utc), Title = "Blood Pressure Check-up", Description = "Regular blood pressure monitoring and medication review", CreatedBy = "System", LastModifiedBy = "System" },
					new Schedule { Id = 2, ClinicianId = 2, CustomerId = 1, ReferralId = 2, EmployeeId = 2, Start = new DateTime(2025, 4, 22, 10, 0, 0, DateTimeKind.Utc), End = new DateTime(2025, 4, 22, 11, 0, 0, DateTimeKind.Utc), Title = "Diabetes Follow-up", Description = "Review of blood sugar levels and medication adjustment", CreatedBy = "System", LastModifiedBy = "System" },
					new Schedule { Id = 3, ClinicianId = 3, CustomerId = 1, ReferralId = 3, EmployeeId = 3, Start = new DateTime(2025, 4, 22, 11, 0, 0, DateTimeKind.Utc), End = new DateTime(2025, 4, 22, 12, 0, 0, DateTimeKind.Utc), Title = "Back Pain Assessment", Description = "Initial evaluation of chronic lower back pain", CreatedBy = "System", LastModifiedBy = "System" }
				);
		}
	}
}