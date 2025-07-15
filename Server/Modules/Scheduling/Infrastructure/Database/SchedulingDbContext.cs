using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Scheduling.Entities;
using Shared.Enums;

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
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Schedule>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Referral>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Clinician>()

                .HasData(
                    new Clinician { Id = new Guid("11111111-1111-1111-1111-111111111111"), FirstName = "Alice", LastName = "Smith", Username = "AliceSmith", Telephone = "555-1001", Email = "alice.smith@example.com", ClinicianType = ClinicianTypeEnum.SeniorDoctor, RegulatorType = RegulatorTypeEnum.GMC, LicenceNumber = "GMC1001", AvatarImage = "https://randomuser.me/api/portraits/women/1.jpg", AvatarTitle = "Dr. Alice Smith", AvatarDescription = "Senior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "1 Alice Smith, Senior Doctor, GMC, GMC1001" },
                    new Clinician { Id = new Guid("22222222-2222-2222-2222-222222222222"), FirstName = "Bob", LastName = "Johnson", Username = "BobJohnson", Telephone = "555-1002", Email = "bob.johnson@example.com", ClinicianType = ClinicianTypeEnum.JuniorDoctor, RegulatorType = RegulatorTypeEnum.GMC, LicenceNumber = "GMC1002", AvatarImage = "https://randomuser.me/api/portraits/men/2.jpg", AvatarTitle = "Dr. Bob Johnson", AvatarDescription = "Junior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "2 Bob Johnson, Junior Doctor, GMC, GMC1002" },
                    new Clinician { Id = new Guid("33333333-3333-3333-3333-333333333333"), FirstName = "Carol", LastName = "Williams", Username = "CarolWilliams", Telephone = "555-1003", Email = "carol.williams@example.com", ClinicianType = ClinicianTypeEnum.Nurse, RegulatorType = RegulatorTypeEnum.NMC, LicenceNumber = "NMC1003", AvatarImage = "https://randomuser.me/api/portraits/women/3.jpg", AvatarTitle = "Nurse Carol Williams", AvatarDescription = "Nurse, NMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "3 Carol Williams, Nurse, NMC, NMC1003" },
                    new Clinician { Id = new Guid("44444444-4444-4444-4444-444444444444"), FirstName = "David", LastName = "Brown", Username = "DavidBrown", Telephone = "555-1004", Email = "david.brown@example.com", ClinicianType = ClinicianTypeEnum.SeniorDoctor, RegulatorType = RegulatorTypeEnum.GMC, LicenceNumber = "GMC1004", AvatarImage = "https://randomuser.me/api/portraits/men/4.jpg", AvatarTitle = "Dr. David Brown", AvatarDescription = "Senior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "4 David Brown, Senior Doctor, GMC, GMC1004" },
                    new Clinician { Id = new Guid("55555555-5555-5555-5555-555555555555"), FirstName = "Eva", LastName = "Jones", Username = "EvaJones", Telephone = "555-1005", Email = "eva.jones@example.com", ClinicianType = ClinicianTypeEnum.Nurse, RegulatorType = RegulatorTypeEnum.NMC, LicenceNumber = "NMC1005", AvatarImage = "https://randomuser.me/api/portraits/women/5.jpg", AvatarTitle = "Nurse Eva Jones", AvatarDescription = "Nurse, NMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "5 Eva Jones, Nurse, NMC, NMC1005" },
                    new Clinician { Id = new Guid("66666666-6666-6666-6666-666666666666"), FirstName = "Frank", LastName = "Garcia", Username = "FrankGarcia", Telephone = "555-1006", Email = "frank.garcia@example.com", ClinicianType = ClinicianTypeEnum.JuniorDoctor, RegulatorType = RegulatorTypeEnum.GMC, LicenceNumber = "GMC1006", AvatarImage = "https://randomuser.me/api/portraits/men/6.jpg", AvatarTitle = "Dr. Frank Garcia", AvatarDescription = "Junior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "6 Frank Garcia, Junior Doctor, GMC, GMC1006" },
                    new Clinician { Id = new Guid("77777777-7777-7777-7777-777777777777"), FirstName = "Grace", LastName = "Martinez", Username = "GraceMartinez", Telephone = "555-1007", Email = "grace.martinez@example.com", ClinicianType = ClinicianTypeEnum.Nurse, RegulatorType = RegulatorTypeEnum.NMC, LicenceNumber = "NMC1007", AvatarImage = "https://randomuser.me/api/portraits/women/7.jpg", AvatarTitle = "Nurse Grace Martinez", AvatarDescription = "Nurse, NMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "7 Grace Martinez, Nurse, NMC, NMC1007" },
                    new Clinician { Id = new Guid("88888888-8888-8888-8888-888888888888"), FirstName = "Henry", LastName = "Lee", Username = "HenryLee", Telephone = "555-1008", Email = "henry.lee@example.com", ClinicianType = ClinicianTypeEnum.SeniorDoctor, RegulatorType = RegulatorTypeEnum.GMC, LicenceNumber = "GMC1008", AvatarImage = "https://randomuser.me/api/portraits/men/8.jpg", AvatarTitle = "Dr. Henry Lee", AvatarDescription = "Senior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "8 Henry Lee, Senior Doctor, GMC, GMC1008" },
                    new Clinician { Id = new Guid("99999999-9999-9999-9999-999999999999"), FirstName = "Ivy", LastName = "Walker", Username = "IvyWalker", Telephone = "555-1009", Email = "ivy.walker@example.com", ClinicianType = ClinicianTypeEnum.Nurse, RegulatorType = RegulatorTypeEnum.NMC, LicenceNumber = "NMC1009", AvatarImage = "https://randomuser.me/api/portraits/women/9.jpg", AvatarTitle = "Nurse Ivy Walker", AvatarDescription = "Nurse, NMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "9 Ivy Walker, Nurse, NMC, NMC1009" },
                    new Clinician { Id = new Guid("10101010-1010-1010-1010-101010101010"), FirstName = "Jack", LastName = "Hall", Username = "JackHall", Telephone = "555-1010", Email = "jack.hall@example.com", ClinicianType = ClinicianTypeEnum.JuniorDoctor, RegulatorType = RegulatorTypeEnum.GMC, LicenceNumber = "GMC1010", AvatarImage = "https://randomuser.me/api/portraits/men/10.jpg", AvatarTitle = "Dr. Jack Hall", AvatarDescription = "Junior Doctor, GMC", CreatedBy = "System", LastModifiedBy = "System", SearchTags = "10 Jack Hall, Junior Doctor, GMC, GMC1010" }
                );
            modelBuilder.Entity<Referral>()
                .HasData(
                    new Referral { Id = new Guid("11111111-1111-1111-1111-111111111111"), CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), EmployeeId = new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), ReferralDetails = "Routine checkup for hypertension.", Title = "Hypertension Checkup", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("22222222-2222-2222-2222-222222222222"), CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), EmployeeId = new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), ReferralDetails = "Follow-up for diabetes management.", Title = "Diabetes Follow-up", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("33333333-3333-3333-3333-333333333333"), CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), EmployeeId = new Guid("33333333-cccc-cccc-cccc-333333333333"), ReferralDetails = "Initial consultation for back pain.", Title = "Back Pain Consultation", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("44444444-4444-4444-4444-444444444444"), CustomerId = new Guid("22222222-2222-2222-2222-222222222222"), EmployeeId = new Guid("44444444-dddd-dddd-dddd-444444444444"), ReferralDetails = "Annual physical examination.", Title = "Annual Physical Exam", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("55555555-5555-5555-5555-555555555555"), CustomerId = new Guid("22222222-2222-2222-2222-222222222222"), EmployeeId = new Guid("55555555-eeee-eeee-eeee-555555555555"), ReferralDetails = "Referral for allergy testing.", Title = "Allergy Testing", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("66666666-6666-6666-6666-666666666666"), CustomerId = new Guid("22222222-2222-2222-2222-222222222222"), EmployeeId = new Guid("66666666-ffff-ffff-ffff-666666666666"), ReferralDetails = "Consultation for asthma symptoms.", Title = "Asthma Consultation", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("77777777-7777-7777-7777-777777777777"), CustomerId = new Guid("33333333-3333-3333-3333-333333333333"), EmployeeId = new Guid("77777777-aaaa-aaaa-aaaa-777777777777"), ReferralDetails = "Pre-surgery evaluation.", Title = "Pre-Surgery Evaluation", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("88888888-8888-8888-8888-888888888888"), CustomerId = new Guid("33333333-3333-3333-3333-333333333333"), EmployeeId = new Guid("88888888-bbbb-bbbb-bbbb-888888888888"), ReferralDetails = "Post-operative follow-up.", Title = "Post-Op Follow-up", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("99999999-9999-9999-9999-999999999999"), CustomerId = new Guid("33333333-3333-3333-3333-333333333333"), EmployeeId = new Guid("99999999-cccc-cccc-cccc-999999999999"), ReferralDetails = "Referral for physical therapy.", Title = "Physical Therapy Referral", CreatedBy = "System", LastModifiedBy = "System" },
                    new Referral { Id = new Guid("10101010-1010-1010-1010-101010101010"), CustomerId = new Guid("33333333-3333-3333-3333-333333333333"), EmployeeId = new Guid("10101010-dddd-dddd-dddd-101010101010"), ReferralDetails = "Consultation for migraine headaches.", Title = "Migraine Consultation", CreatedBy = "System", LastModifiedBy = "System" }
                        );
            modelBuilder.Entity<Schedule>()
                .HasData(
                    new Schedule { Id = new Guid("11111111-1111-1111-1111-111111111111"), ClinicianId = new Guid("11111111-1111-1111-1111-111111111111"), CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), ReferralId = new Guid("11111111-1111-1111-1111-111111111111"), EmployeeId = new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), Start = new DateTime(2025, 4, 22, 9, 0, 0, DateTimeKind.Utc), End = new DateTime(2025, 4, 22, 10, 0, 0, DateTimeKind.Utc), Title = "Blood Pressure Check-up", Description = "Regular blood pressure monitoring and medication review", CreatedBy = "System", LastModifiedBy = "System" },
                    new Schedule { Id = new Guid("22222222-2222-2222-2222-222222222222"), ClinicianId = new Guid("22222222-2222-2222-2222-222222222222"), CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), ReferralId = new Guid("22222222-2222-2222-2222-222222222222"), EmployeeId = new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), Start = new DateTime(2025, 4, 22, 10, 0, 0, DateTimeKind.Utc), End = new DateTime(2025, 4, 22, 11, 0, 0, DateTimeKind.Utc), Title = "Diabetes Follow-up", Description = "Review of blood sugar levels and medication adjustment", CreatedBy = "System", LastModifiedBy = "System" },
                    new Schedule { Id = new Guid("33333333-3333-3333-3333-333333333333"), ClinicianId = new Guid("33333333-3333-3333-3333-333333333333"), CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), ReferralId = new Guid("33333333-3333-3333-3333-333333333333"), EmployeeId = new Guid("33333333-cccc-cccc-cccc-333333333333"), Start = new DateTime(2025, 4, 22, 11, 0, 0, DateTimeKind.Utc), End = new DateTime(2025, 4, 22, 12, 0, 0, DateTimeKind.Utc), Title = "Back Pain Assessment", Description = "Initial evaluation of chronic lower back pain", CreatedBy = "System", LastModifiedBy = "System" }
                );
        }
    }
}