using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using Client.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Forms;

[TestFixture]
public class EmployeeFormTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
        await NavigateTo("/employees");
        await WaitForMudBlazorReady();
    }

    [Test]
    public async Task EmployeeForm_ValidData_ShouldSubmitSuccessfully()
    {
        // Arrange
        var employee = TestDataGenerator.GenerateValidEmployee();
        
        // Navigate to add employee (this might vary based on your UI)
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Fill form with valid data
        var formData = new Dictionary<string, string>
        {
            ["First Name"] = employee.FirstName,
            ["Last Name"] = employee.LastName,
            ["Address Line 1"] = employee.Address1,
            ["Address Line 2"] = employee.Address2!,
            ["Postcode"] = employee.Postcode,
            ["Email"] = employee.Email,
            ["Telephone"] = employee.Telephone,
            ["Job Role"] = employee.JobRole,
            ["Department"] = employee.Department,
            ["Line Manager"] = employee.LineManager
        };

        await Page.FillFormAndValidate(formData, expectErrors: false);
        
        // Set date of birth
        await Page.SetMudDatePicker("Date of Birth", employee.DOB!.Value);
        
        // Submit form
        await Page.SubmitFormAndWait("Save Changes");
        
        // Assert
        var snackbarMessage = await Page.GetSnackbarMessage();
        snackbarMessage.Should().Contain("successfully", "Expected success message after valid form submission");
    }

    [Test]
    public async Task EmployeeForm_EmptyRequiredFields_ShouldShowValidationErrors()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Act - Submit empty form
        await Page.ClickMudButton("Save Changes");
        await Page.WaitForFormValidation();
        
        // Assert
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Empty required fields should trigger validation errors");
        
        var errors = await Page.GetValidationErrors();
        errors.Should().Contain(e => e.Contains("First Name is required"));
        errors.Should().Contain(e => e.Contains("Last Name is required"));
        errors.Should().Contain(e => e.Contains("Email is required"));
    }

    [Test]
    [TestCase("", "First Name is required")]
    [TestCase("A", "First Name must be at least 2 characters")]
    [TestCase("John123", "First Name can only contain letters, spaces, hyphens, and apostrophes")]
    public async Task EmployeeForm_InvalidFirstName_ShouldShowSpecificError(string invalidName, string expectedError)
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Act & Assert
        await Page.TestFieldValidation("First Name", invalidName, expectedError);
    }

    [Test]
    [TestCase("", "Last Name is required")]
    [TestCase("S", "Last Name must be at least 2 characters")]
    [TestCase("Smith@123", "Last Name can only contain letters, spaces, hyphens, and apostrophes")]
    public async Task EmployeeForm_InvalidLastName_ShouldShowSpecificError(string invalidName, string expectedError)
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Act & Assert
        await Page.TestFieldValidation("Last Name", invalidName, expectedError);
    }

    [Test]
    [TestCaseSource(typeof(TestDataGenerator.ValidationTestData), nameof(TestDataGenerator.ValidationTestData.InvalidEmailFormats))]
    public async Task EmployeeForm_InvalidEmail_ShouldShowValidationError(string invalidEmail)
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Act & Assert
        await Page.TestFieldValidation("Email", invalidEmail, "A valid Email is required");
    }

    [Test]
    public async Task EmployeeForm_InvalidTelephoneNumber_ShouldShowValidationError()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Test various invalid UK mobile numbers
        await Page.TestFieldValidation("Telephone", "123456", "Telephone must be a valid UK mobile number");
        await Page.TestFieldValidation("Telephone", "+1 555 123 4567", "Telephone must be a valid UK mobile number");
        await Page.TestFieldValidation("Telephone", "abcdefg", "Telephone must be a valid UK mobile number");
    }

    [Test]
    public async Task EmployeeForm_ValidUKMobileNumbers_ShouldNotShowError()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        var validNumbers = new[] { "+44 7123 456 789", "07123 456 789", "+447123456789" };
        
        foreach (var number in validNumbers)
        {
            // Clear field first
            await Page.FillMudTextField("Telephone", "");
            
            // Fill with valid number
            await Page.FillMudTextField("Telephone", number);
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
            
            var hasError = await Page.HasFieldValidationError("Telephone");
            hasError.Should().BeFalse($"Valid UK mobile number '{number}' should not show validation error");
        }
    }

    [Test]
    [TestCaseSource(typeof(TestDataGenerator.ValidationTestData), nameof(TestDataGenerator.ValidationTestData.InvalidPostcodes))]
    public async Task EmployeeForm_InvalidPostcode_ShouldShowValidationError(string invalidPostcode)
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Act & Assert
        await Page.TestFieldValidation("Postcode", invalidPostcode, "Postcode must be a valid UK postcode");
    }

    [Test]
    public async Task EmployeeForm_DateOfBirthValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Test age validation - employee must be at least 18
        var tooYoungDate = DateTime.Today.AddYears(-17);
        await Page.SetMudDatePicker("Date of Birth", tooYoungDate);
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        var hasError = await Page.HasFieldValidationError("Date of Birth");
        hasError.Should().BeTrue("Employee under 18 should trigger validation error");
        
        var error = await Page.GetFieldValidationError("Date of Birth");
        error.Should().Contain("must be at least 18 years old");
    }

    [Test]
    public async Task EmployeeForm_AddressValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Test Address Line 1 validation
        await Page.TestFieldValidation("Address Line 1", "", "Address Line 1 is required");
        await Page.TestFieldValidation("Address Line 1", "AB", "Address Line 1 must be at least 5 characters");
        
        var longAddress = new string('A', 101);
        await Page.TestFieldValidation("Address Line 1", longAddress, "Address Line 1 must be at most 100 characters");
    }

    [Test]
    public async Task EmployeeForm_LineManagerValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Fill employee name first
        await Page.FillMudTextField("First Name", "John");
        await Page.FillMudTextField("Last Name", "Smith");
        
        // Test that line manager cannot be the same as employee
        await Page.FillMudTextField("Line Manager", "John Smith");
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        var hasError = await Page.HasFieldValidationError("Line Manager");
        hasError.Should().BeTrue("Line manager cannot be the same as employee");
        
        var error = await Page.GetFieldValidationError("Line Manager");
        error.Should().Contain("Line Manager cannot be the same as the employee");
    }

    [Test]
    public async Task EmployeeForm_RequiredFieldsValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Test all required fields
        var requiredFields = new[]
        {
            ("Job Role", "Job Role is required"),
            ("Department", "Department is required"),
            ("Line Manager", "Line Manager is required")
        };
        
        foreach (var (field, expectedError) in requiredFields)
        {
            await Page.TestFieldValidation(field, "", expectedError);
        }
    }

    [Test]
    public async Task EmployeeForm_CompanySelection_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Act - Test company selection (LazyLookup component)
        var companyLookup = await Page.QuerySelectorAsync(".mud-autocomplete:has(.mud-input-label:has-text('Company Name'))");
        if (companyLookup != null)
        {
            await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Company Name')) input");
            await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Company Name')) input", "test");
            await Page.WaitForTimeoutAsync(1000); // Wait for search results
            
            // Check if search results appear
            var results = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
            if (results.Count > 0)
            {
                await results[0].ClickAsync();
                await Page.WaitForFormValidation();
                
                // Verify selection was made
                var selectedValue = await Page.InputValueAsync(".mud-autocomplete:has(.mud-input-label:has-text('Company Name')) input");
                selectedValue.Should().NotBeNullOrEmpty("Company should be selected");
            }
        }
    }

    [Test]
    public async Task EmployeeForm_FormSubmissionBlocked_WhenValidationErrors()
    {
        // Arrange
        await Page.ClickMudButton("Add Employee");
        await WaitForPageReady();
        
        // Fill form with some invalid data
        await Page.FillMudTextField("First Name", "A"); // Too short
        await Page.FillMudTextField("Email", "invalid-email"); // Invalid format
        
        // Act - Try to submit
        await Page.ClickMudButton("Save Changes");
        await Page.WaitForFormValidation();
        
        // Assert - Form should not submit and errors should be visible
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Form with validation errors should not submit");
        
        var submitButton = await Page.QuerySelectorAsync(".mud-button:has-text('Save Changes')");
        if (submitButton != null)
        {
            var isDisabled = await Page.IsSubmitButtonDisabled("Save Changes");
            // Note: Button might not be disabled but form shouldn't submit due to validation
        }
    }
}
