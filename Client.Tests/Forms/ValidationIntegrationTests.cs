using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Forms;

/// <summary>
/// Comprehensive validation test suite that tests validation across all forms
/// </summary>
[TestFixture]
public class ValidationIntegrationTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
    }

    [Test]
    public async Task AllForms_ShouldPreventSubmissionWithValidationErrors()
    {
        // Test all major forms to ensure validation prevents submission
        var formsToTest = new[]
        {
            ("/customers", "Add New Customer", "Save Changes"),
            ("/employees", "Add Employee", "Save Changes"),
            ("/clinicians", "Add Clinician", "Create"),
            ("/finance", "Generate Invoice", "Generate Invoice")
        };

        foreach (var (route, addButton, submitButton) in formsToTest)
        {
            await NavigateTo(route);
            await WaitForMudBlazorReady();
            
            // Try to find and click the add button
            var addBtn = await Page.QuerySelectorAsync($".mud-button:has-text('{addButton}')");
            if (addBtn != null)
            {
                await addBtn.ClickAsync();
                await Page.WaitForTimeoutAsync(1000);
                
                // Try to submit empty form
                var submitBtn = await Page.QuerySelectorAsync($".mud-button:has-text('{submitButton}')");
                if (submitBtn != null)
                {
                    await submitBtn.ClickAsync();
                    await Page.WaitForFormValidation();
                    
                    // Should have validation errors or be prevented from submitting
                    var hasErrors = await Page.HasValidationErrors();
                    var isDisabled = await Page.IsSubmitButtonDisabled(submitButton);
                    
                    (hasErrors || isDisabled).Should().BeTrue($"Form at {route} should prevent empty submission");
                }
                
                // Close dialog/form
                await Page.Keyboard.PressAsync("Escape");
                await Page.WaitForTimeoutAsync(500);
            }
        }
    }

    [Test]
    public async Task EmailValidation_ShouldBeConsistentAcrossAllForms()
    {
        var invalidEmails = new[] { "", "invalid", "@test.com", "user@", "user@.com" };
        
        // Test customer form email validation
        await NavigateTo("/customers");
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        foreach (var invalidEmail in invalidEmails)
        {
            await Page.FillMudTextField("Email", invalidEmail);
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
            
            var hasError = await Page.HasFieldValidationError("Email");
            hasError.Should().BeTrue($"Invalid email '{invalidEmail}' should trigger error in customer form");
        }
        
        await Page.Keyboard.PressAsync("Escape"); // Close form
        
        // Test employee form email validation
        await NavigateTo("/employees");
        var addEmployeeBtn = await Page.QuerySelectorAsync(".mud-button:has-text('Add Employee')");
        if (addEmployeeBtn != null)
        {
            await addEmployeeBtn.ClickAsync();
            await WaitForPageReady();
            
            foreach (var invalidEmail in invalidEmails)
            {
                await Page.FillMudTextField("Email", invalidEmail);
                await Page.Keyboard.PressAsync("Tab");
                await Page.WaitForFormValidation();
                
                var hasError = await Page.HasFieldValidationError("Email");
                hasError.Should().BeTrue($"Invalid email '{invalidEmail}' should trigger error in employee form");
            }
        }
    }

    [Test]
    public async Task PhoneNumberValidation_ShouldBeConsistentAcrossAllForms()
    {
        var invalidPhones = new[] { "", "123", "abcdef", "+1 555 123 4567" };
        
        var formsWithPhones = new[]
        {
            ("/customers", "Add New Customer", "Phone Number"),
            ("/employees", "Add Employee", "Telephone"),
            ("/clinicians", "Add Clinician", "Phone Number")
        };
        
        foreach (var (route, addButton, phoneFieldLabel) in formsWithPhones)
        {
            await NavigateTo(route);
            await Page.ClickMudButton(addButton);
            await Page.WaitForTimeoutAsync(1000);
            
            foreach (var invalidPhone in invalidPhones)
            {
                await Page.FillMudTextField(phoneFieldLabel, invalidPhone);
                await Page.Keyboard.PressAsync("Tab");
                await Page.WaitForFormValidation();
                
                var hasError = await Page.HasFieldValidationError(phoneFieldLabel);
                hasError.Should().BeTrue($"Invalid phone '{invalidPhone}' should trigger error in {route}");
            }
            
            await Page.Keyboard.PressAsync("Escape"); // Close form
            await Page.WaitForTimeoutAsync(500);
        }
    }

    [Test]
    public async Task RequiredFieldValidation_ShouldShowErrorsImmediately()
    {
        // Test that required field validation appears when user leaves empty field
        await NavigateTo("/customers");
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Focus on required field and leave empty
        await Page.ClickAsync(".mud-text-field:has(.mud-input-label:has-text('Name')) input");
        await Page.Keyboard.PressAsync("Tab"); // Leave field empty
        await Page.WaitForFormValidation();
        
        var hasError = await Page.HasFieldValidationError("Name");
        hasError.Should().BeTrue("Required field should show error when left empty");
        
        var error = await Page.GetFieldValidationError("Name");
        error.Should().Contain("required", "Error message should indicate field is required");
    }

    [Test]
    public async Task ValidationErrors_ShouldDisappearWhenCorrected()
    {
        await NavigateTo("/customers");
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Enter invalid data
        await Page.FillMudTextField("Email", "invalid-email");
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        var hasInitialError = await Page.HasFieldValidationError("Email");
        hasInitialError.Should().BeTrue("Invalid email should show error");
        
        // Correct the data
        await Page.FillMudTextField("Email", "valid@example.com");
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        var hasErrorAfterCorrection = await Page.HasFieldValidationError("Email");
        hasErrorAfterCorrection.Should().BeFalse("Valid email should clear the error");
    }

    [Test]
    public async Task FormValidation_ShouldWorkInDialogs()
    {
        // Test validation in MudBlazor dialogs
        await NavigateTo("/clinicians");
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Submit empty form in dialog
        await Page.ClickMudButton("Create");
        await Page.WaitForFormValidation();
        
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Dialog forms should validate just like regular forms");
        
        // Dialog should remain open with errors
        var dialogStillOpen = await Page.QuerySelectorAsync(".mud-dialog") != null;
        dialogStillOpen.Should().BeTrue("Dialog should remain open when validation fails");
    }

    [Test]
    public async Task LongFormValidation_ShouldHandleMultipleSteps()
    {
        // Test complex forms like finance forms that might have multiple steps
        await NavigateTo("/finance");
        await WaitForMudBlazorReady();
        
        // Try to generate invoice without required data
        await Page.ClickMudButton("Generate Invoice");
        await Page.WaitForTimeoutAsync(2000);
        
        // Should either show validation errors or warning message
        var hasValidationErrors = await Page.HasValidationErrors();
        var snackbar = await Page.QuerySelectorAsync(".mud-snackbar");
        
        (hasValidationErrors || snackbar != null).Should().BeTrue("Complex forms should validate before processing");
    }

    [Test]
    public async Task ValidationStyling_ShouldBeConsistent()
    {
        await NavigateTo("/customers");
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Trigger validation error
        await Page.FillMudTextField("Email", "invalid");
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        // Check that error styling is applied
        var emailField = await Page.QuerySelectorAsync(".mud-input-control:has(.mud-input-label:has-text('Email'))");
        if (emailField != null)
        {
            var classList = await emailField.GetAttributeAsync("class");
            classList.Should().Contain("mud-input-error", "Field with validation error should have error styling");
        }
        
        // Check that error message is visible and styled correctly
        var errorMessage = await Page.QuerySelectorAsync(".mud-input-helper-text.mud-input-error");
        errorMessage.Should().NotBeNull("Error message should be visible");
        
        if (errorMessage != null)
        {
            var isVisible = await errorMessage.IsVisibleAsync();
            isVisible.Should().BeTrue("Error message should be visible");
        }
    }

    [Test]
    public async Task FieldValidation_ShouldWorkWithDynamicContent()
    {
        // Test validation in forms with dynamic content (like dependent dropdowns)
        await NavigateTo("/");
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Select customer first
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input", "test");
        await Page.WaitForTimeoutAsync(1000);
        
        var customerResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (customerResults.Count > 0)
        {
            await customerResults[0].ClickAsync();
            await Page.WaitForTimeoutAsync(500);
            
            // Product dropdown should now be populated/enabled
            var productSelect = await Page.QuerySelectorAsync(".mud-select:has(.mud-input-label:has-text('Product'))");
            if (productSelect != null)
            {
                var isDisabled = await productSelect.GetAttributeAsync("disabled");
                isDisabled.Should().BeNull("Product select should be enabled after customer selection");
            }
        }
    }
}
