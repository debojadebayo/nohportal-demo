﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.Modules.Scheduling.Infrastructure.Database;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    [DbContext(typeof(SchedulingDbContext))]
    [Migration("20250506205112_ChangePatientToEmployee")]
    partial class ChangePatientToEmployee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("scheduling")
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Server.Modules.Scheduling.Entities.Clinician", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AvatarDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AvatarImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AvatarTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ClinicianType")
                        .HasColumnType("integer");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LicenceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RegulatorType")
                        .HasColumnType("integer");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clinicians", "scheduling");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AvatarDescription = "Senior Doctor, GMC",
                            AvatarImage = "https://randomuser.me/api/portraits/women/1.jpg",
                            AvatarTitle = "Dr. Alice Smith",
                            ClinicianType = 1,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "alice.smith@example.com",
                            FirstName = "Alice",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Smith",
                            LicenceNumber = "GMC1001",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 1,
                            Telephone = "555-1001"
                        },
                        new
                        {
                            Id = 2L,
                            AvatarDescription = "Junior Doctor, GMC",
                            AvatarImage = "https://randomuser.me/api/portraits/men/2.jpg",
                            AvatarTitle = "Dr. Bob Johnson",
                            ClinicianType = 2,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "bob.johnson@example.com",
                            FirstName = "Bob",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Johnson",
                            LicenceNumber = "GMC1002",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 1,
                            Telephone = "555-1002"
                        },
                        new
                        {
                            Id = 3L,
                            AvatarDescription = "Nurse, NMC",
                            AvatarImage = "https://randomuser.me/api/portraits/women/3.jpg",
                            AvatarTitle = "Nurse Carol Williams",
                            ClinicianType = 3,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "carol.williams@example.com",
                            FirstName = "Carol",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Williams",
                            LicenceNumber = "NMC1003",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 2,
                            Telephone = "555-1003"
                        },
                        new
                        {
                            Id = 4L,
                            AvatarDescription = "Senior Doctor, GMC",
                            AvatarImage = "https://randomuser.me/api/portraits/men/4.jpg",
                            AvatarTitle = "Dr. David Brown",
                            ClinicianType = 1,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "david.brown@example.com",
                            FirstName = "David",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Brown",
                            LicenceNumber = "GMC1004",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 1,
                            Telephone = "555-1004"
                        },
                        new
                        {
                            Id = 5L,
                            AvatarDescription = "Nurse, NMC",
                            AvatarImage = "https://randomuser.me/api/portraits/women/5.jpg",
                            AvatarTitle = "Nurse Eva Jones",
                            ClinicianType = 3,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "eva.jones@example.com",
                            FirstName = "Eva",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Jones",
                            LicenceNumber = "NMC1005",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 2,
                            Telephone = "555-1005"
                        },
                        new
                        {
                            Id = 6L,
                            AvatarDescription = "Junior Doctor, GMC",
                            AvatarImage = "https://randomuser.me/api/portraits/men/6.jpg",
                            AvatarTitle = "Dr. Frank Garcia",
                            ClinicianType = 2,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "frank.garcia@example.com",
                            FirstName = "Frank",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Garcia",
                            LicenceNumber = "GMC1006",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 1,
                            Telephone = "555-1006"
                        },
                        new
                        {
                            Id = 7L,
                            AvatarDescription = "Nurse, NMC",
                            AvatarImage = "https://randomuser.me/api/portraits/women/7.jpg",
                            AvatarTitle = "Nurse Grace Martinez",
                            ClinicianType = 3,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "grace.martinez@example.com",
                            FirstName = "Grace",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Martinez",
                            LicenceNumber = "NMC1007",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 2,
                            Telephone = "555-1007"
                        },
                        new
                        {
                            Id = 8L,
                            AvatarDescription = "Senior Doctor, GMC",
                            AvatarImage = "https://randomuser.me/api/portraits/men/8.jpg",
                            AvatarTitle = "Dr. Henry Lee",
                            ClinicianType = 1,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "henry.lee@example.com",
                            FirstName = "Henry",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Lee",
                            LicenceNumber = "GMC1008",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 1,
                            Telephone = "555-1008"
                        },
                        new
                        {
                            Id = 9L,
                            AvatarDescription = "Nurse, NMC",
                            AvatarImage = "https://randomuser.me/api/portraits/women/9.jpg",
                            AvatarTitle = "Nurse Ivy Walker",
                            ClinicianType = 3,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ivy.walker@example.com",
                            FirstName = "Ivy",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Walker",
                            LicenceNumber = "NMC1009",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 2,
                            Telephone = "555-1009"
                        },
                        new
                        {
                            Id = 10L,
                            AvatarDescription = "Junior Doctor, GMC",
                            AvatarImage = "https://randomuser.me/api/portraits/men/10.jpg",
                            AvatarTitle = "Dr. Jack Hall",
                            ClinicianType = 2,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jack.hall@example.com",
                            FirstName = "Jack",
                            IsActive = false,
                            LastModifiedBy = 0,
                            LastName = "Hall",
                            LicenceNumber = "GMC1010",
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RegulatorType = 1,
                            Telephone = "555-1010"
                        });
                });

            modelBuilder.Entity("Server.Modules.Scheduling.Entities.Referral", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("DocumentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReferralDetails")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Referrals", "scheduling");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1L,
                            DocumentId = "DOC-1001",
                            EmployeeId = 1L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Routine checkup for hypertension."
                        },
                        new
                        {
                            Id = 2L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1L,
                            DocumentId = "DOC-1002",
                            EmployeeId = 2L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Follow-up for diabetes management."
                        },
                        new
                        {
                            Id = 3L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1L,
                            DocumentId = "DOC-1003",
                            EmployeeId = 3L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Initial consultation for back pain."
                        },
                        new
                        {
                            Id = 4L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 2L,
                            DocumentId = "DOC-1004",
                            EmployeeId = 4L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Annual physical examination."
                        },
                        new
                        {
                            Id = 5L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 2L,
                            DocumentId = "DOC-1005",
                            EmployeeId = 5L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Referral for allergy testing."
                        },
                        new
                        {
                            Id = 6L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 2L,
                            DocumentId = "DOC-1006",
                            EmployeeId = 6L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Consultation for asthma symptoms."
                        },
                        new
                        {
                            Id = 7L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 3L,
                            DocumentId = "DOC-1007",
                            EmployeeId = 7L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Pre-surgery evaluation."
                        },
                        new
                        {
                            Id = 8L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 3L,
                            DocumentId = "DOC-1008",
                            EmployeeId = 8L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Post-operative follow-up."
                        },
                        new
                        {
                            Id = 9L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 3L,
                            DocumentId = "DOC-1009",
                            EmployeeId = 9L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Referral for physical therapy."
                        },
                        new
                        {
                            Id = 10L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 3L,
                            DocumentId = "DOC-1010",
                            EmployeeId = 10L,
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReferralDetails = "Consultation for migraine headaches."
                        });
                });

            modelBuilder.Entity("Server.Modules.Scheduling.Entities.Schedule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ClinicianId")
                        .HasColumnType("bigint");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long>("ReferralId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClinicianId");

                    b.ToTable("Schedules", "scheduling");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ClinicianId = 1L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1L,
                            EmployeeId = 1L,
                            End = new DateTime(2025, 4, 22, 10, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 0L,
                            ReferralId = 1L,
                            Start = new DateTime(2025, 4, 22, 9, 0, 0, 0, DateTimeKind.Utc),
                            Status = 0
                        },
                        new
                        {
                            Id = 2L,
                            ClinicianId = 2L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1L,
                            EmployeeId = 2L,
                            End = new DateTime(2025, 4, 22, 11, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 0L,
                            ReferralId = 2L,
                            Start = new DateTime(2025, 4, 22, 10, 0, 0, 0, DateTimeKind.Utc),
                            Status = 0
                        },
                        new
                        {
                            Id = 3L,
                            ClinicianId = 3L,
                            CreatedBy = 0,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1L,
                            EmployeeId = 3L,
                            End = new DateTime(2025, 4, 22, 12, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = false,
                            LastModifiedBy = 0,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 0L,
                            ReferralId = 3L,
                            Start = new DateTime(2025, 4, 22, 11, 0, 0, 0, DateTimeKind.Utc),
                            Status = 0
                        });
                });

            modelBuilder.Entity("Server.Modules.Scheduling.Entities.Schedule", b =>
                {
                    b.HasOne("Server.Modules.Scheduling.Entities.Clinician", null)
                        .WithMany("CalendarItems")
                        .HasForeignKey("ClinicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Modules.Scheduling.Entities.Clinician", b =>
                {
                    b.Navigation("CalendarItems");
                });
#pragma warning restore 612, 618
        }
    }
}
