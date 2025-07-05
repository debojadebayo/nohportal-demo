using Bogus;
using Shared.DTOs.CRM;
using Shared.DTOs.Scheduling;
using Shared.Enums;

namespace Client.Tests.TestData;

public static class TestDataGenerator
{
    private static readonly Faker _faker = new();

    public static CustomerDto GenerateValidCustomer()
    {
        return new Faker<CustomerDto>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Company.CompanyName())
            .RuleFor(c => c.Domain, f => f.Internet.DomainName())
            .RuleFor(c => c.Telephone, f => GenerateUKPhoneNumber())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.InvoiceEmail, f => f.Internet.Email())
            .RuleFor(c => c.Address, f => f.Address.FullAddress())
            .RuleFor(c => c.Postcode, f => GenerateUKPostcode())
            .RuleFor(c => c.Site, f => f.Address.City())
            .RuleFor(c => c.Industry, f => f.PickRandom("Healthcare", "Manufacturing", "Construction", "Education", "Finance", "Retail", "Technology"))
            .RuleFor(c => c.OHServicesRequired, f => f.Lorem.Sentence(5))
            .RuleFor(c => c.NumberOfEmployees, f => f.Random.Int(1, 1000))
            .RuleFor(c => c.Website, f => f.Internet.Url())
            .Generate();
    }

    public static CustomerDto GenerateInvalidCustomer()
    {
        return new CustomerDto
        {
            Id = Guid.NewGuid(),
            Name = "", // Invalid: empty name
            Domain = "invalid-domain", // Invalid domain format
            Telephone = "123", // Invalid phone format
            Email = "invalid-email", // Invalid email format
            InvoiceEmail = "", // Invalid: empty email
            Address = "A", // Too short
            Postcode = "123", // Invalid UK postcode
            Site = "", // Empty site
            Industry = "InvalidIndustry", // Not in allowed list
            OHServicesRequired = "", // Empty
            NumberOfEmployees = 0, // Invalid: must be > 0
            Website = "not-a-url" // Invalid URL
        };
    }

    public static EmployeeDto GenerateValidEmployee()
    {
        return new Faker<EmployeeDto>()
            .RuleFor(e => e.Id, f => Guid.NewGuid())
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.DOB, f => f.Date.Past(50, DateTime.Today.AddYears(-18)))
            .RuleFor(e => e.Address1, f => f.Address.StreetAddress())
            .RuleFor(e => e.Address2, f => f.Address.SecondaryAddress())
            .RuleFor(e => e.Postcode, f => GenerateUKPostcode())
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(e => e.Telephone, f => GenerateUKMobileNumber())
            .RuleFor(e => e.JobRole, f => f.Name.JobTitle())
            .RuleFor(e => e.Department, f => f.Commerce.Department())
            .RuleFor(e => e.LineManager, f => f.Name.FullName())
            .RuleFor(e => e.CustomerId, f => Guid.NewGuid())
            .Generate();
    }

    public static EmployeeDto GenerateInvalidEmployee()
    {
        return new EmployeeDto
        {
            Id = Guid.NewGuid(),
            FirstName = "A", // Too short
            LastName = "", // Empty
            DOB = DateTime.Today.AddYears(-10), // Too young
            Address1 = "AB", // Too short
            Address2 = "",
            Postcode = "INVALID", // Invalid format
            Email = "not-an-email", // Invalid email
            Telephone = "123", // Invalid phone
            JobRole = "", // Empty
            Department = "", // Empty
            LineManager = "", // Empty
            CustomerId = Guid.Empty // Invalid GUID
        };
    }

    public static ClinicianDto GenerateValidClinician()
    {
        return new Faker<ClinicianDto>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Telephone, f => GenerateUKPhoneNumber())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.ClinicianType, f => f.PickRandom<ClinicianTypeEnum>())
            .RuleFor(c => c.RegulatorType, f => f.PickRandom<RegulatorTypeEnum>())
            .RuleFor(c => c.LicenceNumber, f => f.Random.AlphaNumeric(8))
            .RuleFor(c => c.AvatarImage, f => "")
            .RuleFor(c => c.AvatarTitle, f => "")
            .RuleFor(c => c.AvatarDescription, f => "")
            .Generate();
    }

    public static ClinicianDto GenerateInvalidClinician()
    {
        return new ClinicianDto
        {
            Id = Guid.NewGuid(),
            FirstName = "", // Empty
            LastName = "", // Empty
            Telephone = "", // Empty
            Email = "invalid-email", // Invalid format
            ClinicianType = (ClinicianTypeEnum)999, // Invalid enum
            RegulatorType = (RegulatorTypeEnum)999, // Invalid enum
            LicenceNumber = "", // Empty
            AvatarImage = "",
            AvatarTitle = "",
            AvatarDescription = ""
        };
    }

    public static ReferralDto GenerateValidReferral()
    {
        return new Faker<ReferralDto>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.CustomerId, f => Guid.NewGuid())
            .RuleFor(r => r.EmployeeId, f => Guid.NewGuid())
            .RuleFor(r => r.Title, f => f.Lorem.Sentence(3))
            .RuleFor(r => r.ReferralDetails, f => f.Lorem.Paragraph())
            .RuleFor(r => r.CreatedDate, f => f.Date.Recent())
            .Generate();
    }

    public static ContractDto GenerateValidContract()
    {
        return new Faker<ContractDto>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Reference, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.Notes, f => f.Lorem.Paragraph())
            .RuleFor(c => c.RepresentativeId, f => Guid.NewGuid())
            .RuleFor(c => c.StartTime, f => f.Date.Recent())
            .RuleFor(c => c.EndTime, f => f.Date.Future())
            .Generate();
    }

    public static ProductDto GenerateValidProduct()
    {
        return new Faker<ProductDto>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000))
            .RuleFor(p => p.StartTime, f => f.Date.Recent())
            .RuleFor(p => p.EndTime, f => f.Date.Future())
            .Generate();
    }

    private static string GenerateUKPhoneNumber()
    {
        var formats = new[]
        {
            "+44 20 #### ####",
            "+44 1### ### ###",
            "020 #### ####",
            "01### ### ###"
        };
        
        var format = _faker.PickRandom(formats);
        return _faker.Phone.PhoneNumber(format);
    }

    private static string GenerateUKMobileNumber()
    {
        var formats = new[]
        {
            "+44 7### ### ###",
            "07### ### ###"
        };
        
        var format = _faker.PickRandom(formats);
        return _faker.Phone.PhoneNumber(format);
    }

    private static string GenerateUKPostcode()
    {
        var area = _faker.Random.String2(1, 2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        var district = _faker.Random.Int(1, 99);
        var sector = _faker.Random.Int(0, 9);
        var unit = _faker.Random.String2(2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        
        return $"{area}{district} {sector}{unit}";
    }

    /// <summary>
    /// Generate test data specifically for validation testing
    /// </summary>
    public static class ValidationTestData
    {
        public static IEnumerable<object[]> InvalidEmailFormats()
        {
            yield return new object[] { "" };
            yield return new object[] { "not-an-email" };
            yield return new object[] { "@example.com" };
            yield return new object[] { "user@" };
            yield return new object[] { "user@.com" };
            yield return new object[] { "user space@example.com" };
        }

        public static IEnumerable<object[]> InvalidPhoneNumbers()
        {
            yield return new object[] { "" };
            yield return new object[] { "123" };
            yield return new object[] { "abcdefg" };
            yield return new object[] { "+1 123 456 7890" }; // US format, not UK
        }

        public static IEnumerable<object[]> InvalidPostcodes()
        {
            yield return new object[] { "" };
            yield return new object[] { "123" };
            yield return new object[] { "INVALID" };
            yield return new object[] { "12345" }; // US format
        }

        public static IEnumerable<object[]> InvalidNames()
        {
            yield return new object[] { "" };
            yield return new object[] { "A" }; // Too short
            yield return new object[] { new string('A', 101) }; // Too long
            yield return new object[] { "123" }; // Numbers only
            yield return new object[] { "Name@123" }; // Invalid characters
        }

        public static IEnumerable<object[]> InvalidDates()
        {
            yield return new object[] { DateTime.Today.AddYears(-10) }; // Too young for employee
            yield return new object[] { DateTime.Today.AddDays(1) }; // Future date where past required
        }
    }
}
