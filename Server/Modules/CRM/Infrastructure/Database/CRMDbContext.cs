using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
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
        public DbSet<CustomerDocument> CustomerDocuments { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<Manager> Managers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema.CRM);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRMDbContext).Assembly);

            modelBuilder.Entity<Contract>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<ProductType>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Employee>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Customer>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<CustomerDocument>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<EmployeeDocument>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Manager>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Product>()
                .Navigation(p => p.ProductType)
                .AutoInclude();

            modelBuilder.Entity<ProductType>()
                .HasData(
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "OHP Full Day", Description = "OHP Full Day", DefaultPrice = 85, ChargeCode = "OHPFULL", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("22222222-2222-2222-2222-222222222222"), Name = "OHP Half Day", Description = "OHP Half Day", DefaultPrice = 1000, ChargeCode = "OHPHALF", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("33333333-3333-3333-3333-333333333333"), Name = "OHA Full Day", Description = "OHA Full Day", DefaultPrice = 85, ChargeCode = "OHAFULL", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("44444444-4444-4444-4444-444444444444"), Name = "OHA Half Day", Description = "OHA Half Day", DefaultPrice = 85, ChargeCode = "OHAHALF", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("55555555-5555-5555-5555-555555555555"), Name = "OHT Full Day", Description = "OHT Full Day", DefaultPrice = 85, ChargeCode = "OHTFULLDAY", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("66666666-6666-6666-6666-666666666666"), Name = "OHP Appointment", Description = "OHP Appointment", DefaultPrice = 85, ChargeCode = "OHPAPP", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("77777777-7777-7777-7777-777777777777"), Name = "Pensions Case", Description = "Pensions Case", DefaultPrice = 85, ChargeCode = "OHPPENS", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("88888888-8888-8888-8888-888888888888"), Name = "Audiometry Reviews (per 15 mins)", Description = "Audiometry Reviews (per 15 mins)", DefaultPrice = 85, ChargeCode = "OHPAUDIO", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("99999999-9999-9999-9999-999999999999"), Name = "Monthly Retainer Fee", Description = "Monthly Retainer Fee", DefaultPrice = 85, ChargeCode = "RETAIN", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("10101010-1010-1010-1010-101010101010"), Name = "OHP Consultancy Time (per 15 mins)", Description = "OHP Consultancy Time (per 15 mins)", DefaultPrice = 85, ChargeCode = "OHPTIME", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111112"), Name = "Pre Placement Health Assessment", Description = "Pre Placement Health Assessment", DefaultPrice = 85, ChargeCode = "PPHA", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111113"), Name = "Pre Placement Health Assessment (per 15 mins)", Description = "Pre Placement Health Assessment (per 15 mins)", DefaultPrice = 85, ChargeCode = "PPHA15", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111114"), Name = "Mileage and Travel Re-Charged to Customer (per mile)", Description = "Mileage and Travel Re-Charged to Customer (per mile)", DefaultPrice = 85, ChargeCode = "MTRC", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111115"), Name = "Accommodation Recharged to Customer", Description = "Accommodation Recharged to Customer", DefaultPrice = 85, ChargeCode = "ARC", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111116"), Name = "Consumables Recharged to Customer", Description = "Consumables Recharged to Customer", DefaultPrice = 85, ChargeCode = "CRC", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111117"), Name = "Physiotherapy Services Recharged to Customer", Description = "Physiotherapy Services Recharged to Customer", DefaultPrice = 85, ChargeCode = "PSRC", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111118"), Name = "GP / Specialist Report Recharged", Description = "GP / Specialist Report Recharged", DefaultPrice = 85, ChargeCode = "GPSR", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111119"), Name = "Administration Time", Description = "Administration Time", DefaultPrice = 85, ChargeCode = "ADMIN", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111120"), Name = "HAVS Tier 1", Description = "HAVS Tier 1", DefaultPrice = 85, ChargeCode = "HAVS1", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111121"), Name = "HAVS Tier 2", Description = "HAVS Tier 2", DefaultPrice = 85, ChargeCode = "HAVS2", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111122"), Name = "HAVS Tier 3", Description = "HAVS Tier 3", DefaultPrice = 85, ChargeCode = "HAVS3", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111123"), Name = "HAVS Tier 4", Description = "HAVS Tier 4", DefaultPrice = 85, ChargeCode = "HAVS4", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111124"), Name = "OHP Full Complex", Description = "OHP Full Complex", DefaultPrice = 85, ChargeCode = "OHPFULLCOMPLEX", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111125"), Name = "PTS / Rail Work mini audit", Description = "PTS / Rail Work mini audit", DefaultPrice = 85, ChargeCode = "PTSMINI", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111126"), Name = "PTS / Rail Work paper based review", Description = "PTS / Rail Work paper based review", DefaultPrice = 85, ChargeCode = "PTSPAPER", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111127"), Name = "PTS / Rail Work audit of cases", Description = "PTS / Rail Work audit of cases", DefaultPrice = 85, ChargeCode = "PTSAUDIT", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111128"), Name = "PTS / Rail Work Retainer (per month)", Description = "PTS / Rail Work Retainer (per month)", DefaultPrice = 85, ChargeCode = "PTSRETAIN", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" },
                    new ProductType { Id = new Guid("11111111-1111-1111-1111-111111111129"), Name = "Additional PTS or MRO work or reporting (per 15 mins)", Description = "Additional PTS or MRO work or reporting (per 15 mins)", DefaultPrice = 85, ChargeCode = "PTSADD", StartTime = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2026, 4, 16, 17, 0, 0, DateTimeKind.Utc), CreatedBy = "System", LastModifiedBy = "System" }
                );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
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
                    CreatedBy = "System",
                    LastModifiedBy = "System",
                    CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
                    ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
                    SearchTags = $"1 Acme Corp 1 Acme Street London AC1 2ME 01234 567890",
                    Domain = "acme.example.com"
                },
                new Customer
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
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
                    CreatedBy = "System",
                    LastModifiedBy = "System",
                    CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
                    ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
                    SearchTags = $"2 Beta Ltd 2 Beta Road Manchester BT2 3LT 02345 678901",
                    Domain = "beta.example.com"
                },
                new Customer
                {
                    Id = new Guid("33333333-3333-3333-3333-333333333333"),
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
                    CreatedBy = "System",
                    LastModifiedBy = "System",
                    CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
                    ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
                    Domain = "gamma.example.com"
                },
                new Customer
                {
                    Id = new Guid("20202020-2020-2020-2020-202020202020"),
                    Name = "Nation Occupational Health",
                    Telephone = "01147 004 362",
                    NumberOfEmployees = 200,
                    Site = "Birmingham",
                    OHServicesRequired = "Ad hoc assessments",
                    Address = "First Floor, Swan Buildings, 20 Swan Street, Manchester",
                    Industry = "Occupatioanl Health",
                    Postcode = "M4 5JW",
                    Website = "https://www.nationoh.co.uk",
                    Email = "contact@nationoh.co.uk",
                    InvoiceEmail = "contact@nationoh.co.uk",
                    Notes = "",
                    IsActive = true,
                    CreatedBy = "System",
                    LastModifiedBy = "System",
                    CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
                    ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc),
                    SearchTags = $"20 Nation Occupational Health First Floor Swan Buildings 20 Swan Street Manchester M4 5JW 01147 004 362",
                    Domain = "nationoh.co.uk"
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), FirstName = "Alice", LastName = "Smith", Username = "AliceSmith", DOB = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), Address1 = "1 Main St", Address2 = "Apt 1", Address3 = "", Postcode = "EMP1 1AA", Email = "alice.smith@example.com", Telephone = "07111 111111", CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), JobRole = "Manager", Department = "HR", LineManager = "Bob Jones", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"1 Alice Smith 1 Main St Apt 1 EMP1 1AA 07111 111111" },
                new Employee { Id = new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), FirstName = "Bob", LastName = "Jones", Username = "BobJones", DOB = new DateTime(1985, 2, 2, 0, 0, 0, DateTimeKind.Utc), Address1 = "2 Main St", Address2 = "Apt 2", Address3 = "", Postcode = "EMP2 2BB", Email = "bob.jones@example.com", Telephone = "07222 222222", CustomerId = new Guid("22222222-2222-2222-2222-222222222222"), JobRole = "Engineer", Department = "IT", LineManager = "Carol White", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"2 Bob Jones 2 Main St Apt 2 EMP2 2BB 07222 222222" },
                new Employee { Id = new Guid("33333333-cccc-cccc-cccc-333333333333"), FirstName = "Carol", LastName = "White", Username = "CarolWhite", DOB = new DateTime(1992, 3, 3, 0, 0, 0, DateTimeKind.Utc), Address1 = "3 Main St", Address2 = "Apt 3", Address3 = "", Postcode = "EMP3 3CC", Email = "carol.white@example.com", Telephone = "07333 333333", CustomerId = new Guid("33333333-3333-3333-3333-333333333333"), JobRole = "Analyst", Department = "Finance", LineManager = "David Black", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"3 Carol White 3 Main St Apt 3 EMP3 3CC 07333 333333" },
                new Employee { Id = new Guid("44444444-dddd-dddd-dddd-444444444444"), FirstName = "David", LastName = "Black", Username = "DavidBlack", DOB = new DateTime(1988, 4, 4, 0, 0, 0, DateTimeKind.Utc), Address1 = "4 Main St", Address2 = "Apt 4", Address3 = "", Postcode = "EMP4 4DD", Email = "david.black@example.com", Telephone = "07444 444444", CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), JobRole = "Consultant", Department = "Consulting", LineManager = "Alice Smith", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"4 David Black 4 Main St Apt 4 EMP4 4DD 07444 444444" },
                new Employee { Id = new Guid("55555555-eeee-eeee-eeee-555555555555"), FirstName = "Eve", LastName = "Green", Username = "EveGreen", DOB = new DateTime(1995, 5, 5, 0, 0, 0, DateTimeKind.Utc), Address1 = "5 Main St", Address2 = "Apt 5", Address3 = "", Postcode = "EMP5 5EE", Email = "eve.green@example.com", Telephone = "07555 555555", CustomerId = new Guid("22222222-2222-2222-2222-222222222222"), JobRole = "Nurse", Department = "Medical", LineManager = "Bob Jones", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"5 Eve Green 5 Main St Apt 5 EMP5 5EE 07555 555555" },
                new Employee { Id = new Guid("66666666-ffff-ffff-ffff-666666666666"), FirstName = "Frank", LastName = "Blue", Username = "FrankBlue", DOB = new DateTime(1983, 6, 6, 0, 0, 0, DateTimeKind.Utc), Address1 = "6 Main St", Address2 = "Apt 6", Address3 = "", Postcode = "EMP6 6FF", Email = "frank.blue@example.com", Telephone = "07666 666666", CustomerId = new Guid("33333333-3333-3333-3333-333333333333"), JobRole = "Technician", Department = "Support", LineManager = "Carol White", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"6 Frank Blue 6 Main St Apt 6 EMP6 6FF 07666 666666" },
                new Employee { Id = new Guid("77777777-aaaa-aaaa-aaaa-777777777777"), FirstName = "Grace", LastName = "Brown", Username = "GraceBrown", DOB = new DateTime(1991, 7, 7, 0, 0, 0, DateTimeKind.Utc), Address1 = "7 Main St", Address2 = "Apt 7", Address3 = "", Postcode = "EMP7 7GG", Email = "grace.brown@example.com", Telephone = "07777 777777", CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), JobRole = "Advisor", Department = "Advisory", LineManager = "David Black", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"7 Grace Brown 7 Main St Apt 7 EMP7 7GG 07777 777777" },
                new Employee { Id = new Guid("88888888-bbbb-bbbb-bbbb-888888888888"), FirstName = "Henry", LastName = "Gray", Username = "HenryGray", DOB = new DateTime(1987, 8, 8, 0, 0, 0, DateTimeKind.Utc), Address1 = "8 Main St", Address2 = "Apt 8", Address3 = "", Postcode = "EMP8 8HH", Email = "henry.gray@example.com", Telephone = "07888 888888", CustomerId = new Guid("22222222-2222-2222-2222-222222222222"), JobRole = "Driver", Department = "Logistics", LineManager = "Eve Green", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"8 Henry Gray 8 Main St Apt 8 EMP8 8HH 07888 888888" },
                new Employee { Id = new Guid("99999999-cccc-cccc-cccc-999999999999"), FirstName = "Ivy", LastName = "Violet", Username = "IvyViolet", DOB = new DateTime(1993, 9, 9, 0, 0, 0, DateTimeKind.Utc), Address1 = "9 Main St", Address2 = "Apt 9", Address3 = "", Postcode = "EMP9 9II", Email = "ivy.violet@example.com", Telephone = "07999 999999", CustomerId = new Guid("33333333-3333-3333-3333-333333333333"), JobRole = "Receptionist", Department = "Admin", LineManager = "Frank Blue", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"9 Ivy Violet 9 Main St Apt 9 EMP9 9II 07999 999999" },
                new Employee { Id = new Guid("10101010-dddd-dddd-dddd-101010101010"), FirstName = "Jack", LastName = "White", Username = "JackWhite", DOB = new DateTime(1989, 10, 10, 0, 0, 0, DateTimeKind.Utc), Address1 = "10 Main St", Address2 = "Apt 10", Address3 = "", Postcode = "EMP10 0JJ", Email = "jack.white@example.com", Telephone = "07000 000000", CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), JobRole = "Cleaner", Department = "Facilities", LineManager = "Grace Brown", IsActive = true, CreatedBy = "System", LastModifiedBy = "System", CreatedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), ModifiedDate = new DateTime(2025, 4, 16, 17, 0, 0, DateTimeKind.Utc), SearchTags = $"10 Jack White 10 Main St Apt 10 EMP10 0JJ 07000 000000" }
            );
            modelBuilder.Entity<Manager>().HasData(
                new Manager { Id = new Guid("11111111-5555-5555-5555-111111111111"), FirstName = "Thompson", LastName = "Smith", Username = "ThompsonSmith", Email = "thompson.smith@example.com", Telephone = "07111 111111", Department = "HR", CustomerId = new Guid("11111111-1111-1111-1111-111111111111"), CreatedBy = "System", LastModifiedBy = "System", SearchTags = "1 Thompson Smith 1 Acme Street London AC1 2ME 07111 111111" },
                new Manager { Id = new Guid("22222222-6666-6666-6666-222222222222"), FirstName = "Emily", LastName = "Johnson", Username = "EmilyJohnson", Email = "emily.johnson@example.com", Telephone = "07222 222222", Department = "Finance", CustomerId = new Guid("22222222-2222-2222-2222-222222222222"), CreatedBy = "System", LastModifiedBy = "System", SearchTags = "2 Emily Johnson 2 Beta Road Manchester BT2 3LT 07222 222222" },
                new Manager { Id = new Guid("33333333-7777-7777-7777-333333333333"), FirstName = "Michael", LastName = "Brown", Username = "MichaelBrown", Email = "michael.brown@example.com", Telephone = "07333 333333", Department = "IT", CustomerId = new Guid("33333333-3333-3333-3333-333333333333"), CreatedBy = "System", LastModifiedBy = "System", SearchTags = "3 Michael Brown 3 Gamma Avenue Birmingham GM3 4IN 07333 333333" }
            );
        }
    }
}