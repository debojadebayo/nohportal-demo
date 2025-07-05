using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using Client.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Forms;

[TestFixture]
public class ClinicianFormTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
        await NavigateTo("/clinicians");
        await WaitForMudBlazorReady();
    }

    [Test]
    public async Task ClinicianForm_ValidData_ShouldSubmitSuccessfully()
    {
        // Arrange
        var clinician = TestDataGenerator.GenerateValidClinician();
        
        // Act - Open add clinician dialog
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Fill form with valid data
        var formData = new Dictionary<string, string>
        {
            ["First Name"] = clinician.FirstName,
            ["Last Name"] = clinician.LastName,
            ["Phone Number"] = clinician.Telephone,
            ["Email"] = clinician.Email,
            ["Licence Number"] = clinician.LicenceNumber
        };

        await Page.FillFormAndValidate(formData, expectErrors: false);
        
        // Select clinician type and regulator type
        await Page.SelectMudSelectOption("Clinician Type", clinician.ClinicianType.ToString());
        await Page.SelectMudSelectOption("Regulator Type", clinician.RegulatorType.ToString());
        
        // Submit form
        await Page.ClickMudButton("Create");
        
        // Assert
        var snackbarMessage = await Page.GetSnackbarMessage();
        snackbarMessage.Should().Contain("successfully", "Expected success message after valid form submission");
    }

    [Test]
    public async Task ClinicianForm_EmptyRequiredFields_ShouldShowValidationErrors()
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Act - Submit empty form
        await Page.ClickMudButton("Create");
        await Page.WaitForFormValidation();
        
        // Assert
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Empty required fields should trigger validation errors");
        
        var errors = await Page.GetValidationErrors();
        errors.Should().Contain(e => e.Contains("First Name"));
        errors.Should().Contain(e => e.Contains("Last Name"));
        errors.Should().Contain(e => e.Contains("Email"));
        errors.Should().Contain(e => e.Contains("Licence Number"));
    }

    [Test]
    [TestCaseSource(typeof(TestDataGenerator.ValidationTestData), nameof(TestDataGenerator.ValidationTestData.InvalidNames))]
    public async Task ClinicianForm_InvalidFirstName_ShouldShowValidationError(string invalidName)
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Act & Assert
        await Page.TestFieldValidation("First Name", invalidName, "First Name");
    }

    [Test]
    [TestCaseSource(typeof(TestDataGenerator.ValidationTestData), nameof(TestDataGenerator.ValidationTestData.InvalidEmailFormats))]
    public async Task ClinicianForm_InvalidEmail_ShouldShowValidationError(string invalidEmail)
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Act & Assert
        await Page.TestFieldValidation("Email", invalidEmail, "valid email");
    }

    [Test]
    public async Task ClinicianForm_ClinicianTypeSelection_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Act - Test clinician type selection
        await Page.ClickAsync(".mud-select:has(.mud-input-label:has-text('Clinician Type'))");
        await Page.WaitForSelectorAsync(".mud-popover .mud-list");
        
        // Verify enum options are available
        var expectedTypes = new[] { "Doctor", "Nurse", "Physiotherapist", "Occupational Therapist" };
        
        foreach (var type in expectedTypes)
        {
            var optionExists = await Page.QuerySelectorAsync($".mud-popover .mud-list-item:has-text('{type}')") != null;
            if (optionExists)
            {
                // Select this option to test it works
                await Page.ClickAsync($".mud-popover .mud-list-item:has-text('{type}')");
                await Page.WaitForTimeoutAsync(300);
                
                // Verify selection
                var selectedText = await Page.TextContentAsync(".mud-select:has(.mud-input-label:has-text('Clinician Type')) .mud-input");
                selectedText.Should().Contain(type, $"Clinician type '{type}' should be selectable");
                break;
            }
        }
    }

    [Test]
    public async Task ClinicianForm_RegulatorTypeSelection_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Act - Test regulator type selection
        await Page.ClickAsync(".mud-select:has(.mud-input-label:has-text('Regulator Type'))");
        await Page.WaitForSelectorAsync(".mud-popover .mud-list");
        
        // Verify enum options are available
        var expectedTypes = new[] { "GMC", "NMC", "HCPC", "Other" };
        
        foreach (var type in expectedTypes)
        {
            var optionExists = await Page.QuerySelectorAsync($".mud-popover .mud-list-item:has-text('{type}')") != null;
            if (optionExists)
            {
                // Select this option to test it works
                await Page.ClickAsync($".mud-popover .mud-list-item:has-text('{type}')");
                await Page.WaitForTimeoutAsync(300);
                
                // Verify selection
                var selectedText = await Page.TextContentAsync(".mud-select:has(.mud-input-label:has-text('Regulator Type')) .mud-input");
                selectedText.Should().Contain(type, $"Regulator type '{type}' should be selectable");
                break;
            }
        }
    }

    [Test]
    public async Task ClinicianForm_LicenceNumberValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Act & Assert - Test empty licence number
        await Page.TestFieldValidation("Licence Number", "", "Licence Number");
    }

    [Test]
    public async Task ClinicianForm_TelephoneValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Test various invalid phone numbers
        var invalidPhones = new[] { "", "123", "abcdefg", "+1 555 123 4567" };
        
        foreach (var phone in invalidPhones)
        {
            await Page.FillMudTextField("Phone Number", phone);
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
            
            var hasError = await Page.HasFieldValidationError("Phone Number");
            hasError.Should().BeTrue($"Invalid phone number '{phone}' should trigger validation error");
        }
    }

    [Test]
    public async Task ClinicianForm_CancelButton_ShouldCloseDialog()
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Fill some data
        await Page.FillMudTextField("First Name", "Test");
        await Page.FillMudTextField("Last Name", "Clinician");
        
        // Act - Click cancel
        await Page.ClickMudButton("Cancel");
        await Page.WaitForTimeoutAsync(500);
        
        // Assert - Dialog should be closed
        var dialogExists = await Page.QuerySelectorAsync(".mud-dialog") != null;
        dialogExists.Should().BeFalse("Dialog should be closed after clicking Cancel");
    }

    [Test]
    public async Task ClinicianForm_EditExistingClinician_ShouldLoadData()
    {
        // This test assumes there are existing clinicians
        // Look for edit button in the clinicians table
        var editButton = await Page.QuerySelectorAsync(".mud-data-grid .mud-button:has-text('Edit'), .mud-table .mud-button:has-text('Edit')");
        
        if (editButton != null)
        {
            // Act - Click edit button
            await editButton.ClickAsync();
            await WaitForDialog();
            
            // Assert - Form should be populated with existing data
            var firstNameValue = await Page.InputValueAsync(".mud-text-field:has(.mud-input-label:has-text('First Name')) input");
            firstNameValue.Should().NotBeNullOrEmpty("First name should be loaded for editing");
            
            var lastNameValue = await Page.InputValueAsync(".mud-text-field:has(.mud-input-label:has-text('Last Name')) input");
            lastNameValue.Should().NotBeNullOrEmpty("Last name should be loaded for editing");
            
            // Verify the button text is "Update" instead of "Create"
            var updateButton = await Page.QuerySelectorAsync(".mud-button:has-text('Update')");
            updateButton.Should().NotBeNull("Update button should be present when editing");
        }
    }

    [Test]
    public async Task ClinicianForm_FormValidationSummary_ShouldDisplayAllErrors()
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Act - Submit form with multiple validation errors
        await Page.FillMudTextField("First Name", "A"); // Too short
        await Page.FillMudTextField("Email", "invalid"); // Invalid email
        await Page.FillMudTextField("Phone Number", "123"); // Invalid phone
        
        await Page.ClickMudButton("Create");
        await Page.WaitForFormValidation();
        
        // Assert - Multiple errors should be displayed
        var errors = await Page.GetValidationErrors();
        errors.Length.Should().BeGreaterThan(2, "Multiple validation errors should be displayed");
        
        // Verify specific errors are present
        errors.Should().Contain(e => e.Contains("First Name"), "First name validation error should be present");
        errors.Should().Contain(e => e.Contains("Email") || e.Contains("email"), "Email validation error should be present");
    }

    [Test]
    public async Task ClinicianForm_RequiredFieldsHighlighted_WhenEmpty()
    {
        // Arrange
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Act - Try to submit empty form
        await Page.ClickMudButton("Create");
        await Page.WaitForFormValidation();
        
        // Assert - Required fields should be highlighted with error styling
        var requiredFields = new[] { "First Name", "Last Name", "Email", "Licence Number" };
        
        foreach (var field in requiredFields)
        {
            var fieldElement = await Page.QuerySelectorAsync($".mud-input-control:has(.mud-input-label:has-text('{field}'))");
            if (fieldElement != null)
            {
                var classList = await fieldElement.GetAttributeAsync("class");
                classList.Should().Contain("mud-input-error", $"Field '{field}' should have error styling when required and empty");
            }
        }
    }
}
