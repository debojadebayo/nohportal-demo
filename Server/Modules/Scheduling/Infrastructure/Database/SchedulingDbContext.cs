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
					new Clinician { Id = 1, FirstName = "Alice", LastName = "Smith", Telephone = "555-1001", Email = "alice.smith@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.SeniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1001", AvatarImage = "https://randomuser.me/api/portraits/women/1.jpg", AvatarTitle = "Dr. Alice Smith", AvatarDescription = "Senior Doctor, GMC" },
					new Clinician { Id = 2, FirstName = "Bob", LastName = "Johnson", Telephone = "555-1002", Email = "bob.johnson@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.JuniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1002", AvatarImage = "https://randomuser.me/api/portraits/men/2.jpg", AvatarTitle = "Dr. Bob Johnson", AvatarDescription = "Junior Doctor, GMC" },
					new Clinician { Id = 3, FirstName = "Carol", LastName = "Williams", Telephone = "555-1003", Email = "carol.williams@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.Nurse, RegulatorType = Shared.Enums.RegulatorTypeEnum.NMC, LicenceNumber = "NMC1003", AvatarImage = "https://randomuser.me/api/portraits/women/3.jpg", AvatarTitle = "Nurse Carol Williams", AvatarDescription = "Nurse, NMC" },
					new Clinician { Id = 4, FirstName = "David", LastName = "Brown", Telephone = "555-1004", Email = "david.brown@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.SeniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1004", AvatarImage = "https://randomuser.me/api/portraits/men/4.jpg", AvatarTitle = "Dr. David Brown", AvatarDescription = "Senior Doctor, GMC" },
					new Clinician { Id = 5, FirstName = "Eva", LastName = "Jones", Telephone = "555-1005", Email = "eva.jones@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.Nurse, RegulatorType = Shared.Enums.RegulatorTypeEnum.NMC, LicenceNumber = "NMC1005", AvatarImage = "https://randomuser.me/api/portraits/women/5.jpg", AvatarTitle = "Nurse Eva Jones", AvatarDescription = "Nurse, NMC" },
					new Clinician { Id = 6, FirstName = "Frank", LastName = "Garcia", Telephone = "555-1006", Email = "frank.garcia@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.JuniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1006", AvatarImage = "https://randomuser.me/api/portraits/men/6.jpg", AvatarTitle = "Dr. Frank Garcia", AvatarDescription = "Junior Doctor, GMC" },
					new Clinician { Id = 7, FirstName = "Grace", LastName = "Martinez", Telephone = "555-1007", Email = "grace.martinez@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.Nurse, RegulatorType = Shared.Enums.RegulatorTypeEnum.NMC, LicenceNumber = "NMC1007", AvatarImage = "https://randomuser.me/api/portraits/women/7.jpg", AvatarTitle = "Nurse Grace Martinez", AvatarDescription = "Nurse, NMC" },
					new Clinician { Id = 8, FirstName = "Henry", LastName = "Lee", Telephone = "555-1008", Email = "henry.lee@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.SeniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1008", AvatarImage = "https://randomuser.me/api/portraits/men/8.jpg", AvatarTitle = "Dr. Henry Lee", AvatarDescription = "Senior Doctor, GMC" },
					new Clinician { Id = 9, FirstName = "Ivy", LastName = "Walker", Telephone = "555-1009", Email = "ivy.walker@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.Nurse, RegulatorType = Shared.Enums.RegulatorTypeEnum.NMC, LicenceNumber = "NMC1009", AvatarImage = "https://randomuser.me/api/portraits/women/9.jpg", AvatarTitle = "Nurse Ivy Walker", AvatarDescription = "Nurse, NMC" },
					new Clinician { Id = 10, FirstName = "Jack", LastName = "Hall", Telephone = "555-1010", Email = "jack.hall@example.com", ClinicianType = Shared.Enums.ClinicianTypeEnum.JuniorDoctor, RegulatorType = Shared.Enums.RegulatorTypeEnum.GMC, LicenceNumber = "GMC1010", AvatarImage = "https://randomuser.me/api/portraits/men/10.jpg", AvatarTitle = "Dr. Jack Hall", AvatarDescription = "Junior Doctor, GMC" }
					);
			modelBuilder.Entity<Referral>()
				.HasData(
					new Referral { Id = 1, CustomerId = 1, PatientId = 1, ReferralDetails = "Routine checkup for hypertension.", DocumentId = "DOC-1001" },
					new Referral { Id = 2, CustomerId = 1, PatientId = 2, ReferralDetails = "Follow-up for diabetes management.", DocumentId = "DOC-1002" },
					new Referral { Id = 3, CustomerId = 1, PatientId = 3, ReferralDetails = "Initial consultation for back pain.", DocumentId = "DOC-1003" },
					new Referral { Id = 4, CustomerId = 2, PatientId = 4, ReferralDetails = "Annual physical examination.", DocumentId = "DOC-1004" },
					new Referral { Id = 5, CustomerId = 2, PatientId = 5, ReferralDetails = "Referral for allergy testing.", DocumentId = "DOC-1005" },
					new Referral { Id = 6, CustomerId = 2, PatientId = 6, ReferralDetails = "Consultation for asthma symptoms.", DocumentId = "DOC-1006" },
					new Referral { Id = 7, CustomerId = 3, PatientId = 7, ReferralDetails = "Pre-surgery evaluation.", DocumentId = "DOC-1007" },
					new Referral { Id = 8, CustomerId = 3, PatientId = 8, ReferralDetails = "Post-operative follow-up.", DocumentId = "DOC-1008" },
					new Referral { Id = 9, CustomerId = 3, PatientId = 9, ReferralDetails = "Referral for physical therapy.", DocumentId = "DOC-1009" },
					new Referral { Id = 10, CustomerId = 3, PatientId = 10, ReferralDetails = "Consultation for migraine headaches.", DocumentId = "DOC-1010" }
					);
		}
	}
}