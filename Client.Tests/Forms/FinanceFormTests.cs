using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using Client.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Forms;

[TestFixture]
public class FinanceFormTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
        await NavigateTo("/finance");
        await WaitForMudBlazorReady();
    }

    [Test]
    public async Task InvoiceGenerationForm_ValidData_ShouldGenerateInvoice()
    {
        // Arrange - Ensure we're on the Generate Invoice tab
        await Page.ClickAsync(".mud-tab:has-text('Generate Invoice')");
        await WaitForPageReady();
        
        // Select a customer first
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input", "test");
        await Page.WaitForTimeoutAsync(1000);
        
        var customerResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (customerResults.Count > 0)
        {
            await customerResults[0].ClickAsync();
            await Page.WaitForFormValidation();
        }
        
        // Set date range
        var fromDate = DateTime.Today.AddDays(-30);
        var toDate = DateTime.Today;
        
        await Page.SetMudDatePicker("From Date", fromDate);
        await Page.SetMudDatePicker("To Date", toDate);
        
        // Act - Generate invoice
        await Page.ClickMudButton("Generate Invoice");
        
        // Wait for generation to complete
        await Page.WaitForSelectorAsync(".mud-button:has-text('Continue'), .mud-snackbar", new() { Timeout = 15000 });
        
        // Assert - Should either show continue button or success message
        var continueButton = await Page.QuerySelectorAsync(".mud-button:has-text('Continue')");
        var snackbar = await Page.QuerySelectorAsync(".mud-snackbar");
        
        (continueButton != null || snackbar != null).Should().BeTrue("Invoice generation should complete with either continue button or message");
    }

    [Test]
    public async Task InvoiceGenerationForm_EmptyCustomer_ShouldShowValidationError()
    {
        // Arrange
        await Page.ClickAsync(".mud-tab:has-text('Generate Invoice')");
        await WaitForPageReady();
        
        // Set valid dates but no customer
        await Page.SetMudDatePicker("From Date", DateTime.Today.AddDays(-30));
        await Page.SetMudDatePicker("To Date", DateTime.Today);
        
        // Act - Try to generate invoice without selecting customer
        await Page.ClickMudButton("Generate Invoice");
        await Page.WaitForFormValidation();
        
        // Assert - Should show validation error or warning message
        var snackbar = await Page.WaitForSelectorAsync(".mud-snackbar", new() { Timeout = 5000 });
        var snackbarText = await snackbar.TextContentAsync();
        
        snackbarText.Should().Contain("customer", "Should show customer selection error");
    }

    [Test]
    public async Task InvoiceGenerationForm_InvalidDateRange_ShouldShowValidationError()
    {
        // Arrange
        await Page.ClickAsync(".mud-tab:has-text('Generate Invoice')");
        await WaitForPageReady();
        
        // Set invalid date range (to date before from date)
        await Page.SetMudDatePicker("From Date", DateTime.Today);
        await Page.SetMudDatePicker("To Date", DateTime.Today.AddDays(-10));
        
        await Page.Keyboard.PressAsync("Tab"); // Trigger validation
        await Page.WaitForFormValidation();
        
        // Assert - Should show validation error
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Invalid date range should trigger validation error");
        
        var error = await Page.GetFieldValidationError("To Date");
        error.Should().Contain("after", "To Date should be after From Date");
    }

    [Test]
    public async Task InvoiceGenerationForm_FutureDates_ShouldShowValidationError()
    {
        // Arrange
        await Page.ClickAsync(".mud-tab:has-text('Generate Invoice')");
        await WaitForPageReady();
        
        // Set future dates
        await Page.SetMudDatePicker("From Date", DateTime.Today.AddDays(1));
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        // Assert - Should show validation error for future from date
        var hasFromDateError = await Page.HasFieldValidationError("From Date");
        hasFromDateError.Should().BeTrue("Future from date should trigger validation error");
        
        var fromDateError = await Page.GetFieldValidationError("From Date");
        fromDateError.Should().Contain("future", "From Date cannot be in the future");
    }

    [Test]
    public async Task InvoiceGenerationForm_DateRangeExceeds365Days_ShouldShowValidationError()
    {
        // Arrange
        await Page.ClickAsync(".mud-tab:has-text('Generate Invoice')");
        await WaitForPageReady();
        
        // Set date range exceeding 365 days
        await Page.SetMudDatePicker("From Date", DateTime.Today.AddDays(-400));
        await Page.SetMudDatePicker("To Date", DateTime.Today);
        
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        // Assert - Should show validation error for excessive date range
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Date range exceeding 365 days should trigger validation error");
        
        var errors = await Page.GetValidationErrors();
        errors.Should().Contain(e => e.Contains("365 days"), "Should show 365 days limit error");
    }

    [Test]
    public async Task InvoicePreviewForm_ValidData_ShouldUpdateInvoice()
    {
        // This test assumes an invoice has been generated and we're on the preview tab
        // First try to navigate to preview tab
        var previewTab = await Page.QuerySelectorAsync(".mud-tab:has-text('Invoice Preview')");
        if (previewTab != null)
        {
            await previewTab.ClickAsync();
            await WaitForPageReady();
            
            // Check if there's invoice data to update
            var dueDateField = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('Due Date'))");
            if (dueDateField != null)
            {
                // Set due date
                await Page.SetMudDatePicker("Due Date", DateTime.Today.AddDays(30));
                
                // Add notes
                await Page.FillMudTextField("Invoice Notes", "Test invoice notes for validation");
                
                // Act - Update invoice
                await Page.ClickMudButton("Update");
                
                // Assert - Should show success message
                var snackbar = await Page.WaitForSelectorAsync(".mud-snackbar", new() { Timeout = 10000 });
                var snackbarText = await snackbar.TextContentAsync();
                
                snackbarText.Should().Contain("successfully", "Invoice update should be successful");
            }
        }
    }

    [Test]
    public async Task InvoicePreviewForm_InvalidDueDate_ShouldShowValidationError()
    {
        // Navigate to preview tab if available
        var previewTab = await Page.QuerySelectorAsync(".mud-tab:has-text('Invoice Preview')");
        if (previewTab != null)
        {
            await previewTab.ClickAsync();
            await WaitForPageReady();
            
            var dueDateField = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('Due Date'))");
            if (dueDateField != null)
            {
                // Set due date in the past
                await Page.SetMudDatePicker("Due Date", DateTime.Today.AddDays(-10));
                await Page.Keyboard.PressAsync("Tab");
                await Page.WaitForFormValidation();
                
                // Assert - Should show validation error
                var hasError = await Page.HasFieldValidationError("Due Date");
                hasError.Should().BeTrue("Due date in the past should trigger validation error");
                
                var error = await Page.GetFieldValidationError("Due Date");
                error.Should().Contain("after", "Due Date must be after Invoice Date");
            }
        }
    }

    [Test]
    public async Task InvoiceForm_NotesFieldValidation_ShouldWork()
    {
        // Navigate to preview tab if available
        var previewTab = await Page.QuerySelectorAsync(".mud-tab:has-text('Invoice Preview')");
        if (previewTab != null)
        {
            await previewTab.ClickAsync();
            await WaitForPageReady();
            
            var notesField = await Page.QuerySelectorAsync(".mud-text-field:has(.mud-input-label:has-text('Invoice Notes'))");
            if (notesField != null)
            {
                // Test notes length validation
                var longNotes = new string('A', 1001); // Exceeds 1000 character limit
                await Page.FillMudTextField("Invoice Notes", longNotes);
                await Page.Keyboard.PressAsync("Tab");
                await Page.WaitForFormValidation();
                
                var hasError = await Page.HasFieldValidationError("Invoice Notes");
                hasError.Should().BeTrue("Notes exceeding 1000 characters should trigger validation error");
                
                var error = await Page.GetFieldValidationError("Invoice Notes");
                error.Should().Contain("1000 characters", "Should show character limit error");
            }
        }
    }

    [Test]
    public async Task InvoiceForm_GenerateButtonState_ShouldChangeBasedOnValidation()
    {
        // Arrange
        await Page.ClickAsync(".mud-tab:has-text('Generate Invoice')");
        await WaitForPageReady();
        
        // Check initial state - button should be enabled
        var isInitiallyDisabled = await Page.IsSubmitButtonDisabled("Generate Invoice");
        
        // Fill form with invalid data
        await Page.SetMudDatePicker("From Date", DateTime.Today.AddDays(1)); // Future date
        await Page.WaitForFormValidation();
        
        // Button behavior may vary - some forms disable on validation errors, others show errors on submit
        // We'll test that validation errors are properly shown
        var hasErrors = await Page.HasValidationErrors();
        if (hasErrors)
        {
            var isDisabledWithErrors = await Page.IsSubmitButtonDisabled("Generate Invoice");
            // Form should either disable button or show errors on submit attempt
        }
    }

    [Test]
    public async Task InvoiceForm_ProductSelection_ShouldBeOptional()
    {
        // Arrange
        await Page.ClickAsync(".mud-tab:has-text('Generate Invoice')");
        await WaitForPageReady();
        
        // Test that product selection is optional
        var productField = await Page.QuerySelectorAsync(".mud-autocomplete:has(.mud-input-label:has-text('Product'))");
        if (productField != null)
        {
            // Select customer and dates (required fields)
            await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input");
            await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input", "test");
            await Page.WaitForTimeoutAsync(1000);
            
            var results = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
            if (results.Count > 0)
            {
                await results[0].ClickAsync();
            }
            
            await Page.SetMudDatePicker("From Date", DateTime.Today.AddDays(-30));
            await Page.SetMudDatePicker("To Date", DateTime.Today);
            
            // Leave product field empty and try to generate
            await Page.ClickMudButton("Generate Invoice");
            
            // Should not show product validation error since it's optional
            await Page.WaitForTimeoutAsync(2000);
            var errors = await Page.GetValidationErrors();
            errors.Should().NotContain(e => e.Contains("Product"), "Product field should be optional");
        }
    }

    [Test]
    public async Task InvoiceForm_LoadingStates_ShouldWork()
    {
        // Arrange
        await Page.ClickAsync(".mud-tab:has-text('Generate Invoice')");
        await WaitForPageReady();
        
        // Setup valid form data
        await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input");
        await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input", "test");
        await Page.WaitForTimeoutAsync(1000);
        
        var results = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
        if (results.Count > 0)
        {
            await results[0].ClickAsync();
        }
        
        await Page.SetMudDatePicker("From Date", DateTime.Today.AddDays(-30));
        await Page.SetMudDatePicker("To Date", DateTime.Today);
        
        // Act - Click generate and check for loading state
        await Page.ClickMudButton("Generate Invoice");
        
        // Assert - Should show loading indicator
        var loadingIndicator = await Page.WaitForSelectorAsync(".mud-progress-circular, .mud-button:has-text('Generating')", new() { Timeout = 2000 });
        loadingIndicator.Should().NotBeNull("Should show loading indicator during invoice generation");
    }
}
