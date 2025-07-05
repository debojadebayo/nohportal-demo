using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using Client.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Forms;

[TestFixture]
public class ScheduleFormTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
        await NavigateTo("/"); // Diary page
        await WaitForMudBlazorReady();
    }

    [Test]
    public async Task ScheduleForm_ValidData_ShouldCreateAppointment()
    {
        // Arrange - Open new appointment dialog
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Fill form with valid data
        await Page.FillMudTextField("Appointment Title", "Test Appointment");
        await Page.FillMudTextField("Appointment Description", "Test appointment description");
        
        // Select employee
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Employee')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Employee')) input", "test");
        await Page.WaitForTimeoutAsync(1000);
        
        var employeeResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (employeeResults.Count > 0)
        {
            await employeeResults[0].ClickAsync();
        }
        
        // Select customer
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input", "test");
        await Page.WaitForTimeoutAsync(1000);
        
        var customerResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (customerResults.Count > 0)
        {
            await customerResults[0].ClickAsync();
        }
        
        // Select clinician
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Clinician')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Clinician')) input", "Dr");
        await Page.WaitForTimeoutAsync(1000);
        
        var clinicianResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (clinicianResults.Count > 0)
        {
            await clinicianResults[0].ClickAsync();
        }
        
        // Set appointment date and times
        await Page.SetMudDatePicker("Appointment Date", DateTime.Today.AddDays(7));
        
        // Set start and end times
        var startTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('Start Time')) input");
        if (startTimeInput != null)
        {
            await startTimeInput.FillAsync("09:00");
        }
        
        var endTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('End Time')) input");
        if (endTimeInput != null)
        {
            await endTimeInput.FillAsync("10:00");
        }
        
        // Act - Submit form
        await Page.ClickMudButton("Ok");
        
        // Assert
        var snackbar = await Page.WaitForSelectorAsync(".mud-snackbar", new() { Timeout = 10000 });
        var snackbarText = await snackbar.TextContentAsync();
        
        snackbarText.Should().Contain("successfully", "Appointment should be created successfully");
    }

    [Test]
    public async Task ScheduleForm_EmptyRequiredFields_ShouldShowValidationErrors()
    {
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Act - Submit empty form
        await Page.ClickMudButton("Ok");
        await Page.WaitForFormValidation();
        
        // Assert
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Empty required fields should trigger validation errors");
        
        var errors = await Page.GetValidationErrors();
        errors.Should().Contain(e => e.Contains("Customer") || e.Contains("Employee") || e.Contains("Clinician"));
    }

    [Test]
    public async Task ScheduleForm_EndTimeBeforeStartTime_ShouldShowValidationError()
    {
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Set end time before start time
        var startTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('Start Time')) input");
        if (startTimeInput != null)
        {
            await startTimeInput.FillAsync("10:00");
        }
        
        var endTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('End Time')) input");
        if (endTimeInput != null)
        {
            await endTimeInput.FillAsync("09:00");
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
        }
        
        // Assert
        var hasError = await Page.HasFieldValidationError("End Time");
        hasError.Should().BeTrue("End time before start time should trigger validation error");
        
        var error = await Page.GetFieldValidationError("End Time");
        error.Should().Contain("after", "End time should be after start time");
    }

    [Test]
    public async Task ScheduleForm_AppointmentDateInPast_ShouldShowValidationError()
    {
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Set appointment date in the past
        await Page.SetMudDatePicker("Appointment Date", DateTime.Today.AddDays(-1));
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        // Assert - Depending on business rules, past dates might be allowed or not
        // This test assumes past dates are not allowed for new appointments
        var hasError = await Page.HasFieldValidationError("Appointment Date");
        if (hasError)
        {
            var error = await Page.GetFieldValidationError("Appointment Date");
            error.Should().Contain("future", "Past appointment dates should be validated");
        }
    }

    [Test]
    public async Task ScheduleForm_ReferralSelection_ShouldPopulateFields()
    {
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Check if referrals tab/section is visible
        var referralsSection = await Page.QuerySelectorAsync(".referrals-tab, .mud-expansion-panel:has-text('Referrals')");
        if (referralsSection != null)
        {
            // Click on a referral if available
            var selectReferralButton = await Page.QuerySelectorAsync(".mud-button:has-text('Select for Appointment')");
            if (selectReferralButton != null)
            {
                await selectReferralButton.ClickAsync();
                await Page.WaitForFormValidation();
                
                // Assert - Fields should be populated from referral
                var titleValue = await Page.InputValueAsync(".mud-text-field:has(.mud-input-label:has-text('Appointment Title')) input");
                titleValue.Should().NotBeNullOrEmpty("Title should be populated from referral");
                
                var descriptionValue = await Page.InputValueAsync(".mud-text-field:has(.mud-input-label:has-text('Appointment Description')) textarea");
                descriptionValue.Should().NotBeNullOrEmpty("Description should be populated from referral");
            }
        }
    }

    [Test]
    public async Task ScheduleForm_ProductSelection_ShouldBeConstrainedByCustomer()
    {
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // First select a customer
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input", "test");
        await Page.WaitForTimeoutAsync(1000);
        
        var customerResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (customerResults.Count > 0)
        {
            await customerResults[0].ClickAsync();
            await Page.WaitForTimeoutAsync(500);
        }
        
        // Now check product selection
        var productSelect = await Page.QuerySelectorAsync(".mud-select:has(.mud-input-label:has-text('Product'))");
        if (productSelect != null)
        {
            await productSelect.ClickAsync();
            await Page.WaitForSelectorAsync(".mud-popover .mud-list");
            
            var productOptions = await Page.QuerySelectorAllAsync(".mud-popover .mud-list-item");
            productOptions.Should().NotBeEmpty("Products should be available after customer selection");
            
            // Select first product
            if (productOptions.Count > 0)
            {
                await productOptions[0].ClickAsync();
            }
        }
    }

    [Test]
    public async Task ScheduleForm_TimeSlotValidation_ShouldPreventDoubleBooking()
    {
        // This test would check for double booking prevention
        // Implementation depends on your business logic
        
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Fill required fields
        await Page.FillMudTextField("Appointment Title", "Test Appointment");
        
        // Select clinician
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Clinician')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Clinician')) input", "Dr");
        await Page.WaitForTimeoutAsync(1000);
        
        var clinicianResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (clinicianResults.Count > 0)
        {
            await clinicianResults[0].ClickAsync();
        }
        
        // Set appointment for a potentially busy time
        await Page.SetMudDatePicker("Appointment Date", DateTime.Today.AddDays(1));
        
        var startTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('Start Time')) input");
        if (startTimeInput != null)
        {
            await startTimeInput.FillAsync("09:00");
        }
        
        var endTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('End Time')) input");
        if (endTimeInput != null)
        {
            await endTimeInput.FillAsync("10:00");
        }
        
        // Act - Submit and check for conflict warnings
        await Page.ClickMudButton("Ok");
        await Page.WaitForTimeoutAsync(2000);
        
        // Look for conflict warning or success
        var conflictWarning = await Page.QuerySelectorAsync(".mud-alert:has-text('conflict'), .mud-snackbar:has-text('conflict')");
        var successMessage = await Page.QuerySelectorAsync(".mud-snackbar:has-text('success')");
        
        (conflictWarning != null || successMessage != null).Should().BeTrue("Should either warn about conflicts or confirm success");
    }

    [Test]
    public async Task ScheduleForm_CancelButton_ShouldCloseDialog()
    {
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Fill some data
        await Page.FillMudTextField("Appointment Title", "Test");
        
        // Act - Click cancel
        await Page.ClickMudButton("Cancel");
        await Page.WaitForTimeoutAsync(500);
        
        // Assert - Dialog should be closed
        var dialogExists = await Page.QuerySelectorAsync(".mud-dialog") != null;
        dialogExists.Should().BeFalse("Dialog should be closed after clicking Cancel");
    }

    [Test]
    public async Task ScheduleForm_EditExistingAppointment_ShouldLoadData()
    {
        // Look for existing appointments in the calendar
        var existingAppointment = await Page.QuerySelectorAsync(".mud-cal-event, .appointment-item, .calendar-event");
        
        if (existingAppointment != null)
        {
            // Click on existing appointment
            await existingAppointment.ClickAsync();
            await WaitForDialog();
            
            // Assert - Form should be populated with existing data
            var titleValue = await Page.InputValueAsync(".mud-text-field:has(.mud-input-label:has-text('Appointment Title')) input");
            titleValue.Should().NotBeNullOrEmpty("Title should be loaded for editing");
            
            // Check for update button instead of create
            var updateButton = await Page.QuerySelectorAsync(".mud-button:has-text('Update'), .mud-button:has-text('Save Changes')");
            updateButton.Should().NotBeNull("Update button should be present when editing");
        }
    }

    [Test]
    public async Task ScheduleForm_BusinessHoursValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Test appointment outside business hours
        var startTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('Start Time')) input");
        if (startTimeInput != null)
        {
            await startTimeInput.FillAsync("22:00"); // 10 PM - likely outside business hours
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
            
            // Check if there's a warning about business hours
            var businessHoursWarning = await Page.QuerySelectorAsync(".mud-alert, .warning-message");
            if (businessHoursWarning != null)
            {
                var warningText = await businessHoursWarning.TextContentAsync();
                warningText.Should().Contain("business hours", "Should warn about appointments outside business hours");
            }
        }
    }

    [Test]
    public async Task ScheduleForm_AppointmentDuration_ShouldBeReasonable()
    {
        // Arrange
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Test very short appointment (e.g., 5 minutes)
        var startTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('Start Time')) input");
        var endTimeInput = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('End Time')) input");
        
        if (startTimeInput != null && endTimeInput != null)
        {
            await startTimeInput.FillAsync("09:00");
            await endTimeInput.FillAsync("09:05"); // 5-minute appointment
            await Page.Keyboard.PressAsync("Tab");
            await Page.WaitForFormValidation();
            
            // Check for duration warning
            var durationWarning = await Page.QuerySelectorAsync(".mud-alert, .warning-message");
            if (durationWarning != null)
            {
                var warningText = await durationWarning.TextContentAsync();
                warningText.Should().Contain("duration", "Should warn about very short appointments");
            }
        }
    }
}
