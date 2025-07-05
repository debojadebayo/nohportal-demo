using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Components;

[TestFixture]
public class MudBlazorComponentTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
    }

    [Test]
    public async Task DataGrids_ShouldLoadAndDisplayData()
    {
        var pagesWithDataGrids = new[]
        {
            "/customers",
            "/employees", 
            "/clinicians"
        };

        foreach (var page in pagesWithDataGrids)
        {
            await NavigateTo(page);
            
            // Look for MudDataGrid
            var dataGrid = await Page.QuerySelectorAsync(".mud-data-grid, .mud-table");
            dataGrid.Should().NotBeNull($"Page {page} should have a data grid");
            
            // Check for loading state or data
            var loadingIndicator = await Page.QuerySelectorAsync(".mud-progress-circular");
            var dataRows = await Page.QuerySelectorAllAsync(".mud-data-grid tbody tr, .mud-table tbody tr");
            var noDataMessage = await Page.QuerySelectorAsync(".mud-data-grid .no-records, .mud-table .no-records");
            
            (loadingIndicator != null || dataRows.Count > 0 || noDataMessage != null)
                .Should().BeTrue($"Data grid on {page} should show loading, data, or no data message");
        }
    }

    [Test]
    public async Task Dialogs_ShouldOpenAndClose()
    {
        await NavigateTo("/clinicians");
        
        // Open dialog
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        var dialog = await Page.QuerySelectorAsync(".mud-dialog");
        dialog.Should().NotBeNull("Dialog should open when clicking Add Clinician");
        
        // Close dialog with cancel
        await Page.ClickMudButton("Cancel");
        await Page.WaitForTimeoutAsync(500);
        
        var dialogAfterCancel = await Page.QuerySelectorAsync(".mud-dialog");
        dialogAfterCancel.Should().BeNull("Dialog should close when clicking Cancel");
        
        // Test escape key
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        await Page.Keyboard.PressAsync("Escape");
        await Page.WaitForTimeoutAsync(500);
        
        var dialogAfterEscape = await Page.QuerySelectorAsync(".mud-dialog");
        dialogAfterEscape.Should().BeNull("Dialog should close when pressing Escape");
    }

    [Test]
    public async Task Tabs_ShouldSwitchContent()
    {
        await NavigateTo("/customers");
        
        // Look for customer with tabs
        var customerSearch = await Page.QuerySelectorAsync(".mud-autocomplete input");
        if (customerSearch != null)
        {
            await customerSearch.FillAsync("test");
            await Page.WaitForTimeoutAsync(1000);
            
            var results = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
            if (results.Count > 0)
            {
                await results[0].ClickAsync();
                await WaitForPageReady();
                
                // Check for tabs
                var tabs = await Page.QuerySelectorAllAsync(".mud-tab");
                if (tabs.Count > 1)
                {
                    // Click second tab
                    await tabs[1].ClickAsync();
                    await Page.WaitForTimeoutAsync(500);
                    
                    // Verify tab is active
                    var activeTab = await Page.QuerySelectorAsync(".mud-tab.mud-tab-active");
                    activeTab.Should().NotBeNull("Clicked tab should become active");
                }
            }
        }
    }

    [Test]
    public async Task Snackbars_ShouldAppearForUserActions()
    {
        await NavigateTo("/customers");
        
        // Try to trigger a snackbar by performing an action
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Fill minimum required fields and submit
        await Page.FillMudTextField("Name", "Test Company");
        await Page.FillMudTextField("Phone Number", "+44 20 1234 5678");
        await Page.FillMudTextField("Email", "test@example.com");
        await Page.FillMudTextField("Invoice Email", "invoice@example.com");
        await Page.FillMudTextField("Address", "123 Test Street, London");
        await Page.FillMudTextField("Postcode", "SW1A 1AA");
        await Page.FillMudTextField("Site", "London Office");
        await Page.FillMudTextField("OH Services Required", "Health surveillance");
        await Page.FillMudTextField("Number of Employees", "50");
        await Page.FillMudTextField("Website", "https://example.com");
        
        // Select industry
        await Page.SelectMudSelectOption("Industry", "Healthcare");
        
        await Page.ClickMudButton("Save Changes");
        
        // Should show snackbar
        var snackbar = await Page.WaitForSelectorAsync(".mud-snackbar", new() { Timeout = 10000 });
        snackbar.Should().NotBeNull("Snackbar should appear after form submission");
    }

    [Test]
    public async Task LazyLookupComponents_ShouldWork()
    {
        await NavigateTo("/");
        await Page.ClickMudButton("Add New Appointment");
        await WaitForDialog();
        
        // Test customer lookup
        var customerLookup = await Page.QuerySelectorAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer'))");
        if (customerLookup != null)
        {
            await Page.ClickAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input");
            await Page.FillAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input", "test");
            await Page.WaitForTimeoutAsync(2000); // Wait for search
            
            var searchResults = await Page.QuerySelectorAllAsync(".mud-autocomplete .mud-list-item");
            searchResults.Should().NotBeEmpty("LazyLookup should return search results");
            
            if (searchResults.Count > 0)
            {
                await searchResults[0].ClickAsync();
                
                // Verify selection
                var inputValue = await Page.InputValueAsync(".mud-autocomplete:has(.mud-input-label:has-text('Customer')) input");
                inputValue.Should().NotBeNullOrEmpty("LazyLookup should set selected value");
            }
        }
    }

    [Test]
    public async Task DatePickers_ShouldWork()
    {
        await NavigateTo("/finance");
        
        // Test date picker functionality
        var fromDatePicker = await Page.QuerySelectorAsync(".mud-picker:has(.mud-input-label:has-text('From Date'))");
        if (fromDatePicker != null)
        {
            await fromDatePicker.ClickAsync();
            
            // Check if date picker dialog opens
            var datePickerDialog = await Page.WaitForSelectorAsync(".mud-picker-popup, .mud-date-picker", new() { Timeout = 2000 });
            datePickerDialog.Should().NotBeNull("Date picker dialog should open");
            
            // Try to select a date
            var todayButton = await Page.QuerySelectorAsync(".mud-picker-popup .mud-button:has-text('Today')");
            if (todayButton != null)
            {
                await todayButton.ClickAsync();
            } else {
                // Click on a date in the calendar
                var dateCell = await Page.QuerySelectorAsync(".mud-picker-popup .mud-picker-calendar-day");
                if (dateCell != null)
                {
                    await dateCell.ClickAsync();
                }
            }
            
            // Verify date was set
            var inputValue = await Page.InputValueAsync(".mud-picker:has(.mud-input-label:has-text('From Date')) input");
            inputValue.Should().NotBeNullOrEmpty("Date picker should set a value");
        }
    }

    [Test]
    public async Task SelectComponents_ShouldWork()
    {
        await NavigateTo("/clinicians");
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Test select component
        var clinicianTypeSelect = await Page.QuerySelectorAsync(".mud-select:has(.mud-input-label:has-text('Clinician Type'))");
        if (clinicianTypeSelect != null)
        {
            await clinicianTypeSelect.ClickAsync();
            
            // Wait for dropdown
            var dropdown = await Page.WaitForSelectorAsync(".mud-popover .mud-list", new() { Timeout = 2000 });
            dropdown.Should().NotBeNull("Select dropdown should open");
            
            // Get options
            var options = await Page.QuerySelectorAllAsync(".mud-popover .mud-list-item");
            options.Should().NotBeEmpty("Select should have options");
            
            if (options.Count > 0)
            {
                await options[0].ClickAsync();
                
                // Verify selection
                var selectedText = await Page.TextContentAsync(".mud-select:has(.mud-input-label:has-text('Clinician Type')) .mud-input");
                selectedText.Should().NotBeNullOrWhiteSpace("Select should show selected value");
            }
        }
    }

    [Test]
    public async Task ProgressIndicators_ShouldAppearDuringOperations()
    {
        await NavigateTo("/finance");
        
        // Setup valid invoice generation
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
        
        // Click generate and look for progress indicator
        await Page.ClickMudButton("Generate Invoice");
        
        var progressIndicator = await Page.WaitForSelectorAsync(".mud-progress-circular, .mud-progress-linear, .mud-button:has-text('Generating')", new() { Timeout = 5000 });
        progressIndicator.Should().NotBeNull("Progress indicator should appear during long operations");
    }
}
