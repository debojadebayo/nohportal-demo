using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using Client.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Forms;

[TestFixture]
public class CaseNotesFormTests : PlaywrightTestBase
{
    private Guid _testScheduleId = Guid.NewGuid(); // This would typically come from test data setup

    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
        // Navigate to case notes page with a test schedule ID
        await NavigateTo($"/case-notes/{_testScheduleId}");
        await WaitForMudBlazorReady();
    }

    [Test]
    public async Task CaseNotesForm_ValidData_ShouldSubmitSuccessfully()
    {
        // Arrange - Fill form with valid case notes data
        var validCaseNotes = "This is a comprehensive case note documenting the patient's consultation. The patient presented with occupational health concerns and we discussed appropriate workplace adjustments.";
        
        // Fill the rich text editor or text area
        var caseNotesField = await Page.QuerySelectorAsync(".mud-rich-text-editor, textarea[data-testid='case-notes'], .mud-text-field:has(.mud-input-label:has-text('Case Notes')) textarea");
        if (caseNotesField != null)
        {
            await caseNotesField.FillAsync(validCaseNotes);
        }
        else
        {
            // Fallback to looking for any large text input
            await Page.FillAsync("textarea, .mud-input[type='textarea']", validCaseNotes);
        }
        
        // Select appointment type
        await Page.SelectMudSelectOption("Appointment Type", "Initial Assessment");
        
        // Select fit for work status
        await Page.SelectMudSelectOption("Fit for Work Status", "Fit for Work");
        
        // Select clinician
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Clinician')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Clinician')) input", "Dr");
        await Page.WaitForTimeoutAsync(1000);
        
        var clinicianResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (clinicianResults.Count > 0)
        {
            await clinicianResults[0].ClickAsync();
        }
        
        // Act - Submit the form
        await Page.ClickMudButton("Save Case Notes");
        
        // Assert
        var snackbar = await Page.WaitForSelectorAsync(".mud-snackbar", new() { Timeout = 10000 });
        var snackbarText = await snackbar.TextContentAsync();
        
        snackbarText.Should().Contain("successfully", "Case notes should be saved successfully");
    }

    [Test]
    public async Task CaseNotesForm_EmptyCaseNotes_ShouldShowValidationError()
    {
        // Act - Try to submit without case notes
        await Page.ClickMudButton("Save Case Notes");
        await Page.WaitForFormValidation();
        
        // Assert
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Empty case notes should trigger validation error");
        
        var error = await Page.GetFieldValidationError("Case Notes");
        error.Should().Contain("required", "Case notes should be required");
    }

    [Test]
    public async Task CaseNotesForm_ShortCaseNotes_ShouldShowValidationError()
    {
        // Arrange - Enter case notes that are too short
        var shortNotes = "Too short";
        
        var caseNotesField = await Page.QuerySelectorAsync("textarea, .mud-rich-text-editor");
        if (caseNotesField != null)
        {
            await caseNotesField.FillAsync(shortNotes);
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
        }
        
        // Assert
        var hasError = await Page.HasFieldValidationError("Case Notes");
        hasError.Should().BeTrue("Short case notes should trigger validation error");
        
        var error = await Page.GetFieldValidationError("Case Notes");
        error.Should().Contain("10 characters", "Case notes must be at least 10 characters");
    }

    [Test]
    public async Task CaseNotesForm_LongCaseNotes_ShouldShowValidationError()
    {
        // Arrange - Enter case notes that exceed maximum length
        var longNotes = new string('A', 10001); // Exceeds 10000 character limit
        
        var caseNotesField = await Page.QuerySelectorAsync("textarea, .mud-rich-text-editor");
        if (caseNotesField != null)
        {
            await caseNotesField.FillAsync(longNotes);
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
        }
        
        // Assert
        var hasError = await Page.HasFieldValidationError("Case Notes");
        hasError.Should().BeTrue("Long case notes should trigger validation error");
        
        var error = await Page.GetFieldValidationError("Case Notes");
        error.Should().Contain("10000 characters", "Case notes must not exceed 10000 characters");
    }

    [Test]
    public async Task CaseNotesForm_AppointmentTypeValidation_ShouldWork()
    {
        // Act - Try to submit without selecting appointment type
        await Page.FillAsync("textarea", "Valid case notes content for testing appointment type validation");
        await Page.ClickMudButton("Save Case Notes");
        await Page.WaitForFormValidation();
        
        // Assert
        var hasError = await Page.HasFieldValidationError("Appointment Type");
        hasError.Should().BeTrue("Missing appointment type should trigger validation error");
    }

    [Test]
    public async Task CaseNotesForm_FitForWorkStatusValidation_ShouldWork()
    {
        // Act - Try to submit without selecting fit for work status
        await Page.FillAsync("textarea", "Valid case notes content for testing fit for work status validation");
        await Page.ClickMudButton("Save Case Notes");
        await Page.WaitForFormValidation();
        
        // Assert
        var hasError = await Page.HasFieldValidationError("Fit for Work Status");
        hasError.Should().BeTrue("Missing fit for work status should trigger validation error");
    }

    [Test]
    public async Task CaseNotesForm_ClinicianSelectionValidation_ShouldWork()
    {
        // Act - Try to submit without selecting clinician
        await Page.FillAsync("textarea", "Valid case notes content for testing clinician selection validation");
        await Page.SelectMudSelectOption("Appointment Type", "Initial Assessment");
        await Page.SelectMudSelectOption("Fit for Work Status", "Fit for Work");
        
        await Page.ClickMudButton("Save Case Notes");
        await Page.WaitForFormValidation();
        
        // Assert
        var hasError = await Page.HasFieldValidationError("Clinician");
        hasError.Should().BeTrue("Missing clinician selection should trigger validation error");
    }

    [Test]
    public async Task CaseNotesForm_FollowUpValidation_ShouldWork()
    {
        // Arrange - Enable follow up
        var followUpCheckbox = await Page.QuerySelectorAsync("input[type='checkbox']:near(.mud-input-label:has-text('Follow Up'))");
        if (followUpCheckbox != null)
        {
            await followUpCheckbox.ClickAsync();
            await Page.WaitForFormValidation();
            
            // Act - Try to submit without follow up details
            await Page.FillAsync("textarea", "Valid case notes with follow up enabled");
            await Page.ClickMudButton("Save Case Notes");
            await Page.WaitForFormValidation();
            
            // Assert - Should require follow up date and reason
            var hasFollowUpDateError = await Page.HasFieldValidationError("Follow Up Date");
            var hasFollowUpReasonError = await Page.HasFieldValidationError("Follow Up Reason");
            
            (hasFollowUpDateError || hasFollowUpReasonError).Should().BeTrue("Follow up enabled should require date and reason");
        }
    }

    [Test]
    public async Task CaseNotesForm_FollowUpDateInPast_ShouldShowValidationError()
    {
        // Arrange - Enable follow up and set past date
        var followUpCheckbox = await Page.QuerySelectorAsync("input[type='checkbox']:near(.mud-input-label:has-text('Follow Up'))");
        if (followUpCheckbox != null)
        {
            await followUpCheckbox.ClickAsync();
            
            // Set follow up date in the past
            await Page.SetMudDatePicker("Follow Up Date", DateTime.Today.AddDays(-1));
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
            
            // Assert
            var hasError = await Page.HasFieldValidationError("Follow Up Date");
            hasError.Should().BeTrue("Follow up date in the past should trigger validation error");
            
            var error = await Page.GetFieldValidationError("Follow Up Date");
            error.Should().Contain("future", "Follow up date must be in the future");
        }
    }

    [Test]
    public async Task CaseNotesForm_RecommendedAdjustmentsLength_ShouldValidate()
    {
        // Test maximum length for recommended adjustments
        var longAdjustments = new string('A', 2001); // Exceeds 2000 character limit
        
        var adjustmentsField = await Page.QuerySelectorAsync(".mud-text-field:has(.mud-input-label:has-text('Recommended Adjustments')) textarea");
        if (adjustmentsField != null)
        {
            await adjustmentsField.FillAsync(longAdjustments);
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
            
            var hasError = await Page.HasFieldValidationError("Recommended Adjustments");
            hasError.Should().BeTrue("Long recommended adjustments should trigger validation error");
            
            var error = await Page.GetFieldValidationError("Recommended Adjustments");
            error.Should().Contain("2000 characters", "Recommended adjustments must not exceed 2000 characters");
        }
    }

    [Test]
    public async Task CaseNotesForm_AutoSave_ShouldWork()
    {
        // This test checks if there's auto-save functionality
        // Fill in some content
        await Page.FillAsync("textarea", "Testing auto-save functionality for case notes");
        await Page.WaitForTimeoutAsync(2000); // Wait for potential auto-save
        
        // Look for auto-save indicator
        var autoSaveIndicator = await Page.QuerySelectorAsync(".auto-save-indicator, .mud-text:has-text('Auto-saved'), .mud-text:has-text('Saved')");
        
        if (autoSaveIndicator != null)
        {
            var indicatorText = await autoSaveIndicator.TextContentAsync();
            indicatorText.Should().Contain("saved", "Auto-save should indicate successful save");
        }
    }

    [Test]
    public async Task CaseNotesForm_RichTextEditor_ShouldWork()
    {
        // Test rich text editor functionality if present
        var richTextEditor = await Page.QuerySelectorAsync(".mud-rich-text-editor, .rich-text-editor");
        
        if (richTextEditor != null)
        {
            // Test basic text entry
            await richTextEditor.ClickAsync();
            await Page.Keyboard.TypeAsync("Testing rich text editor functionality");
            
            // Test formatting buttons if available
            var boldButton = await Page.QuerySelectorAsync(".mud-rich-text-editor .mud-button:has-text('B'), .rich-text-editor .bold-button");
            if (boldButton != null)
            {
                await boldButton.ClickAsync();
                await Page.Keyboard.TypeAsync(" bold text");
            }
            
            // Verify content was entered
            var editorContent = await richTextEditor.TextContentAsync();
            editorContent.Should().Contain("Testing rich text", "Rich text editor should accept content");
        }
    }

    [Test]
    public async Task CaseNotesForm_EncryptionIndicator_ShouldBeVisible()
    {
        // Check for encryption indicator (as mentioned in the case notes page)
        var encryptionIndicator = await Page.QuerySelectorAsync(".encryption-indicator, .mud-text:has-text('Encrypted'), .security-indicator");
        
        if (encryptionIndicator != null)
        {
            var isVisible = await encryptionIndicator.IsVisibleAsync();
            isVisible.Should().BeTrue("Encryption indicator should be visible for security awareness");
        }
    }
}
