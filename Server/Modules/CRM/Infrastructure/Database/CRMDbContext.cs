using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.CRM.Entities;

namespace Server.Modules.CRM.Infrastructure.Database
{
	public sealed class CRMDbContext(DbContextOptions<CRMDbContext> options) : BaseDbContext<CRMDbContext>(options), IDbContext<CRMDbContext>
	{
		public DbSet<Contract> Contracts { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Customer> Customers { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema.CRM);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRMDbContext).Assembly);
			modelBuilder.Entity<ProductType>()
				.HasData(
					new ProductType { Id = 1, Name = "OHP Full Day", Description = "OHP Full Day", DefaultPrice = 85, ChargeCode = "OHPFULL", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 2, Name = "OHP Half Day", Description = "OHP Half Day", DefaultPrice = 1000, ChargeCode = "OHPHALF", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 3, Name = "OHA Full Day", Description = "OHA Full Day", DefaultPrice = 85, ChargeCode = "OHAFULL", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 4, Name = "OHA Half Day", Description = "OHA Half Day", DefaultPrice = 85, ChargeCode = "OHAHALF", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 5, Name = "OHT Full Day", Description = "OHT Full Day", DefaultPrice = 85, ChargeCode = "OHTFULLDAY", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 6, Name = "OHP Appointment", Description = "OHP Appointment", DefaultPrice = 85, ChargeCode = "OHPAPP", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 7, Name = "Pensions Case", Description = "Pensions Case", DefaultPrice = 85, ChargeCode = "OHPPENS", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 8, Name = "Audiometry Reviews (per 15 mins)", Description = "Audiometry Reviews (per 15 mins)", DefaultPrice = 85, ChargeCode = "OHPAUDIO", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 9, Name = "Monthly Retainer Fee", Description = "Monthly Retainer Fee", DefaultPrice = 85, ChargeCode = "RETAIN", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 10, Name = "OHP Consultancy Time (per 15 mins)", Description = "OHP Consultancy Time (per 15 mins)", DefaultPrice = 85, ChargeCode = "OHPTIME", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 11, Name = "Pre Placement Health Assessment", Description = "Pre Placement Health Assessment", DefaultPrice = 85, ChargeCode = "PPHA", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 12, Name = "Pre Placement Health Assessment (per 15 mins)", Description = "Pre Placement Health Assessment (per 15 mins)", DefaultPrice = 85, ChargeCode = "PPHA15", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 13, Name = "Mileage and Travel Re-Charged to Customer (per mile)", Description = "Mileage and Travel Re-Charged to Customer (per mile)", DefaultPrice = 85, ChargeCode = "MTRC", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 14, Name = "Accommodation Recharged to Customer", Description = "Accommodation Recharged to Customer", DefaultPrice = 85, ChargeCode = "ARC", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 15, Name = "Consumables Recharged to Customer", Description = "Consumables Recharged to Customer", DefaultPrice = 85, ChargeCode = "CRC", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 16, Name = "Physiotherapy Services Recharged to Customer", Description = "Physiotherapy Services Recharged to Customer", DefaultPrice = 85, ChargeCode = "PSRC", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 17, Name = "GP / Specialist Report Recharged", Description = "GP / Specialist Report Recharged", DefaultPrice = 85, ChargeCode = "GPSR", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 18, Name = "Administration Time", Description = "Administration Time", DefaultPrice = 85, ChargeCode = "ADMIN", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 19, Name = "HAVS Tier 1", Description = "HAVS Tier 1", DefaultPrice = 85, ChargeCode = "HAVS1", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 20, Name = "HAVS Tier 2", Description = "HAVS Tier 2", DefaultPrice = 85, ChargeCode = "HAVS2", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 21, Name = "HAVS Tier 3", Description = "HAVS Tier 3", DefaultPrice = 85, ChargeCode = "HAVS3", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 22, Name = "HAVS Tier 4", Description = "HAVS Tier 4", DefaultPrice = 85, ChargeCode = "HAVS4", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 23, Name = "OHP Full Complex", Description = "OHP Full Complex", DefaultPrice = 85, ChargeCode = "OHPFULLCOMPLEX", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 24, Name = "PTS / Rail Work mini audit", Description = "PTS / Rail Work mini audit", DefaultPrice = 85, ChargeCode = "PTSMINI", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 25, Name = "PTS / Rail Work paper based review", Description = "PTS / Rail Work paper based review", DefaultPrice = 85, ChargeCode = "PTSPAPER", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 26, Name = "PTS / Rail Work audit of cases", Description = "PTS / Rail Work audit of cases", DefaultPrice = 85, ChargeCode = "PTSAUDIT", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 27, Name = "PTS / Rail Work Retainer (per month)", Description = "PTS / Rail Work Retainer (per month)", DefaultPrice = 85, ChargeCode = "PTSRETAIN", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
					new ProductType { Id = 28, Name = "Additional PTS or MRO work or reporting (per 15 mins)", Description = "Additional PTS or MRO work or reporting (per 15 mins)", DefaultPrice = 85, ChargeCode = "PTSADD", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc) }
				);

			modelBuilder.Entity<Customer>().HasData(
				new Customer {
					Id = 1,
					Name = "Acme Corp",
					Telephone = "01234 567890",
					NumberOfEmployees = 100,
					Site = "London",
					OHServicesRequired = "Full OH Service",
					Address = "1 Acme Street, London",
					Industry = "Technology",
					Postcode = "AC1 2ME",
					Website = "https://acme.example.com",
					Email = "info@acme.example.com",
					InvoiceEmail = "accounts@acme.example.com",
					Notes = "Key client.",
					IsActive = true,
					CreatedBy = 1,
					LastModifiedBy = 1,
					CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
					ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc)
				},
				new Customer {
					Id = 2,
					Name = "Beta Ltd",
					Telephone = "02345 678901",
					NumberOfEmployees = 50,
					Site = "Manchester",
					OHServicesRequired = "Health Surveillance",
					Address = "2 Beta Road, Manchester",
					Industry = "Manufacturing",
					Postcode = "BT2 3LT",
					Website = "https://beta.example.com",
					Email = "contact@beta.example.com",
					InvoiceEmail = "finance@beta.example.com",
					Notes = "Annual contract.",
					IsActive = true,
					CreatedBy = 1,
					LastModifiedBy = 1,
					CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
					ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc)
				},
				new Customer {
					Id = 3,
					Name = "Gamma Inc",
					Telephone = "03456 789012",
					NumberOfEmployees = 200,
					Site = "Birmingham",
					OHServicesRequired = "Ad hoc assessments",
					Address = "3 Gamma Avenue, Birmingham",
					Industry = "Logistics",
					Postcode = "GM3 4IN",
					Website = "https://gamma.example.com",
					Email = "hello@gamma.example.com",
					InvoiceEmail = "billing@gamma.example.com",
					Notes = "Occasional work.",
					IsActive = true,
					CreatedBy = 1,
					LastModifiedBy = 1,
					CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
					ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc)
				}
			);

			modelBuilder.Entity<Employee>().HasData(
				new Employee { Id = 1, FirstName = "Alice", LastName = "Smith", DOB = new DateTime(1990,1,1,0,0,0,DateTimeKind.Utc), Address1 = "1 Main St", Address2 = "Apt 1", Address3 = "", Postcode = "EMP1 1AA", Email = "alice.smith@example.com", Telephone = "07111 111111", CustomerId = 1, JobRole = "Manager", Department = "HR", LineManager = "Bob Jones", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 2, FirstName = "Bob", LastName = "Jones", DOB = new DateTime(1985,2,2,0,0,0,DateTimeKind.Utc), Address1 = "2 Main St", Address2 = "Apt 2", Address3 = "", Postcode = "EMP2 2BB", Email = "bob.jones@example.com", Telephone = "07222 222222", CustomerId = 2, JobRole = "Engineer", Department = "IT", LineManager = "Carol White", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 3, FirstName = "Carol", LastName = "White", DOB = new DateTime(1992,3,3,0,0,0,DateTimeKind.Utc), Address1 = "3 Main St", Address2 = "Apt 3", Address3 = "", Postcode = "EMP3 3CC", Email = "carol.white@example.com", Telephone = "07333 333333", CustomerId = 3, JobRole = "Analyst", Department = "Finance", LineManager = "David Black", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 4, FirstName = "David", LastName = "Black", DOB = new DateTime(1988,4,4,0,0,0,DateTimeKind.Utc), Address1 = "4 Main St", Address2 = "Apt 4", Address3 = "", Postcode = "EMP4 4DD", Email = "david.black@example.com", Telephone = "07444 444444", CustomerId = 1, JobRole = "Consultant", Department = "Consulting", LineManager = "Alice Smith", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 5, FirstName = "Eve", LastName = "Green", DOB = new DateTime(1995,5,5,0,0,0,DateTimeKind.Utc), Address1 = "5 Main St", Address2 = "Apt 5", Address3 = "", Postcode = "EMP5 5EE", Email = "eve.green@example.com", Telephone = "07555 555555", CustomerId = 2, JobRole = "Nurse", Department = "Medical", LineManager = "Bob Jones", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 6, FirstName = "Frank", LastName = "Blue", DOB = new DateTime(1983,6,6,0,0,0,DateTimeKind.Utc), Address1 = "6 Main St", Address2 = "Apt 6", Address3 = "", Postcode = "EMP6 6FF", Email = "frank.blue@example.com", Telephone = "07666 666666", CustomerId = 3, JobRole = "Technician", Department = "Support", LineManager = "Carol White", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 7, FirstName = "Grace", LastName = "Brown", DOB = new DateTime(1991,7,7,0,0,0,DateTimeKind.Utc), Address1 = "7 Main St", Address2 = "Apt 7", Address3 = "", Postcode = "EMP7 7GG", Email = "grace.brown@example.com", Telephone = "07777 777777", CustomerId = 1, JobRole = "Advisor", Department = "Advisory", LineManager = "David Black", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 8, FirstName = "Henry", LastName = "Gray", DOB = new DateTime(1987,8,8,0,0,0,DateTimeKind.Utc), Address1 = "8 Main St", Address2 = "Apt 8", Address3 = "", Postcode = "EMP8 8HH", Email = "henry.gray@example.com", Telephone = "07888 888888", CustomerId = 2, JobRole = "Driver", Department = "Logistics", LineManager = "Eve Green", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 9, FirstName = "Ivy", LastName = "Violet", DOB = new DateTime(1993,9,9,0,0,0,DateTimeKind.Utc), Address1 = "9 Main St", Address2 = "Apt 9", Address3 = "", Postcode = "EMP9 9II", Email = "ivy.violet@example.com", Telephone = "07999 999999", CustomerId = 3, JobRole = "Receptionist", Department = "Admin", LineManager = "Frank Blue", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) },
				new Employee { Id = 10, FirstName = "Jack", LastName = "White", DOB = new DateTime(1989,10,10,0,0,0,DateTimeKind.Utc), Address1 = "10 Main St", Address2 = "Apt 10", Address3 = "", Postcode = "EMP10 0JJ", Email = "jack.white@example.com", Telephone = "07000 000000", CustomerId = 1, JobRole = "Cleaner", Department = "Facilities", LineManager = "Grace Brown", IsActive = true, CreatedBy = 1, LastModifiedBy = 1, CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc) }
			);
		}
	}
}