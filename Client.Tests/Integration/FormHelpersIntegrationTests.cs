using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Client.Tests.Integration;

/// <summary>
/// Integration tests that validate FormHelpers extension methods and complete workflows
/// 
/// IMPORTANT: All test data has been updated to match the exact DTO property names and structure:
/// - CustomerDto: Uses Name, Email, Telephone, Address, Postcode, Website, Industry, etc.
/// - EmployeeDto: Uses FirstName, LastName, Email, Telephone, JobRole, Department, etc.
/// - ClinicianDto: Uses FirstName, LastName, Email, Telephone, LicenceNumber, etc.
/// - ReferralDto: Uses Title, ReferralDetails, CustomerId (Guid), EmployeeId (Guid), etc.
/// 
/// All IDs in DTOs are GUIDs that inherit from BaseDto{T} which auto-generates Guid.NewGuid()
/// Foreign key relationships use proper GUID references (CustomerId, EmployeeId, ClinicianId, etc.)
/// </summary>
[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class FormHelpersIntegrationTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
        await WaitForPageReady();
    }

    /// <summary>
    /// End-to-end test that validates the complete workflow from customer creation to invoice generation
    /// This test validates all FormHelpers methods in a real-world scenario using proper GUID identifiers
    /// </summary>
    [Test]
    public async Task CompleteWorkflow_FromCustomerCreationToInvoice_ShouldWorkCorrectly()
    {
        // Arrange - Generate proper GUIDs for all entities
        var customerId = Guid.NewGuid();
        var employeeId = Guid.NewGuid();
        var clinicianId = Guid.NewGuid();
        var referralId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var scheduleId = Guid.NewGuid();
        
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var testCustomerName = $"Test Customer {timestamp}";
        var testEmployeeName = $"Test Employee {timestamp}";
        var testClinicianName = $"Dr. Test {timestamp}";
        var testReferralReference = $"REF{timestamp}";
        var appointmentDate = DateTime.Today.AddDays(1);

        await EnsureLoggedIn();
        
        // Act & Assert - Step 1: Create a new customer
        TestContext.WriteLine("Step 1: Creating new customer");
        await NavigateToCustomers();
        await CreateNewCustomer(testCustomerName);
        
        // Step 2: Create employee under the customer
        TestContext.WriteLine("Step 2: Creating employee under customer");
        var employeeName = await CreateEmployeeForCustomer(testCustomerName);
        
        // Step 3: Create clinician
        TestContext.WriteLine("Step 3: Creating clinician");
        await NavigateToStaff();
        var clinicianName = await CreateNewClinician();
        
        // Step 4: Create referral in Customers section
        TestContext.WriteLine("Step 4: Creating referral");
        await NavigateToCustomers();
        var referralReference = await CreateReferralForCustomer(testCustomerName, employeeName, testReferralReference);
        
        // Step 5: Navigate to Diary and create appointment
        TestContext.WriteLine("Step 5: Creating appointment in Diary");
        await NavigateToDiary();
        await CreateAppointmentFromReferral(referralReference, clinicianName, appointmentDate);
        
        // Step 6: Verify appointment in Case Management
        TestContext.WriteLine("Step 6: Verifying appointment in Case Management");
        await NavigateToCaseManagement();
        await VerifyAppointmentInGrid(referralReference, testCustomerName, employeeName, clinicianName);
        await EnterClinicalDetails(referralReference);
        
        // Step 7: Generate invoice and verify billing
        TestContext.WriteLine("Step 7: Generating invoice and verifying billing");
        await NavigateToFinance();
        await GenerateInvoiceForCustomer(testCustomerName, appointmentDate);
        await VerifyInvoiceBilling(testCustomerName, referralReference, employeeName);
        
        TestContext.WriteLine("✅ Complete workflow test passed successfully!");
    }

    [Test]
    public async Task FormHelpers_TextFieldOperations_ShouldWorkCorrectly()
    {
        await NavigateToCustomers();
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        await Page.FillMudTextField("Name", "Test Company");
        var companyNameValue = await Page.InputValueAsync(".mud-input-control:has(.mud-input-label:has-text('Name')) input");
        companyNameValue.Should().Be("Test Company");
        
        await Page.FillMudTextField("Name", "");
        var clearedValue = await Page.InputValueAsync(".mud-input-control:has(.mud-input-label:has-text('Name')) input");
        clearedValue.Should().BeEmpty();
        
        await Page.FillMudTextField("Email", "test@example.com");
        var emailValue = await Page.InputValueAsync(".mud-input-control:has(.mud-input-label:has-text('Email')) input");
        emailValue.Should().Be("test@example.com");
    }

    [Test]
    public async Task FormHelpers_SelectComponents_ShouldWorkCorrectly()
    {
        await NavigateToCustomers();
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        await Page.SelectMudSelectOption("Customer Type", "Corporate");
        await Page.WaitForTimeoutAsync(500);
        
        var selectedText = await Page.TextContentAsync(".mud-select:has(.mud-input-label:has-text('Customer Type')) .mud-select-input");
        selectedText.Should().Contain("Corporate");
        
        await Page.SelectMudSelectOption("Industry", "Healthcare");
        await Page.WaitForTimeoutAsync(500);
        
        var industrySelection = await Page.TextContentAsync(".mud-select:has(.mud-input-label:has-text('Industry')) .mud-select-input");
        industrySelection.Should().Contain("Healthcare");
    }

    [Test]
    public async Task FormHelpers_DatePickerOperations_ShouldWorkCorrectly()
    {
        // Arrange
        await NavigateToCustomers();
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        var testDate = new DateTime(2024, 6, 15);
        
        // Act & Assert
        await Page.SetMudDatePicker("Registration Date", testDate);
        
        // Verify date was set correctly
        var dateValue = await Page.InputValueAsync(".mud-picker:has(.mud-input-label:has-text('Registration Date')) input");
        dateValue.Should().Be("15/06/2024");
    }

    [Test]
    public async Task FormHelpers_ValidationMethods_ShouldDetectErrorsCorrectly()
    {
        // Arrange
        await NavigateToCustomers();
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act & Assert - Test field validation with invalid email
        await Page.TestFieldValidation("Email", "invalid-email", "valid email");
        
        // Test that validation errors are detected
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Should detect validation errors for invalid email");
        
        // Test getting specific field error
        var emailError = await Page.GetFieldValidationError("Email");
        emailError.Should().NotBeNullOrWhiteSpace("Should return email validation error");
        emailError.Should().Contain("valid email", "Error message should contain expected text");
        
        // Test field-specific error detection
        var hasEmailError = await Page.HasFieldValidationError("Email");
        hasEmailError.Should().BeTrue("Should detect email field has validation error");
        
        // Test submit button disabled state with errors
        var isDisabled = await Page.IsSubmitButtonDisabled("Save");
        isDisabled.Should().BeTrue("Submit button should be disabled when form has errors");
    }

    [Test]
    public async Task FormHelpers_FormOperations_ShouldHandleComplexScenarios()
    {
        // Arrange
        await NavigateToCustomers();
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act & Assert - Test complex form filling using proper DTO field names
        var validFormData = new Dictionary<string, string>
        {
            ["Name"] = "Valid Company Ltd", // CustomerDto.Name property
            ["Email"] = "contact@validcompany.com", // CustomerDto.Email property
            ["Telephone"] = "0123456789", // CustomerDto.Telephone property
            ["Address"] = "123 Valid Street, Valid City", // CustomerDto.Address property
            ["Postcode"] = "VA1 1ID", // CustomerDto.Postcode property
            ["Website"] = "https://validcompany.com", // CustomerDto.Website property
            ["Industry"] = "Technology" // CustomerDto.Industry property
        };
        
        await Page.FillFormAndValidate(validFormData, expectErrors: false);
        
        // Test that form is now valid
        var hasErrorsAfterValid = await Page.HasValidationErrors();
        hasErrorsAfterValid.Should().BeFalse("Form should be valid after filling with correct data");
        
        // Test clear form functionality
        await Page.ClearForm();
        await Page.WaitForFormValidation();
        
        // Verify all fields are cleared
        var companyNameValue = await Page.InputValueAsync(".mud-input-control:has(.mud-input-label:has-text('Name')) input");
        companyNameValue.Should().BeEmpty("Company name should be cleared");
    }

    [Test]
    public async Task FormHelpers_ButtonInteractions_ShouldWorkCorrectly()
    {
        // Arrange
        await NavigateToCustomers();
        
        // Act & Assert - Test button clicking by text
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Check if the customer form is now visible (inline form, not dialog)
        var formVisible = await Page.IsVisibleAsync(".mud-form, form, .customer-form");
        formVisible.Should().BeTrue("Customer form should be visible after clicking Add New Customer");
        
        // Test if Name field is present (indicating form is loaded)
        var nameFieldVisible = await Page.IsVisibleAsync(".mud-input-control:has(.mud-input-label:has-text('Name'))");
        nameFieldVisible.Should().BeTrue("Name field should be visible in customer form");
    }

    [Test]
    public async Task FormHelpers_SnackbarOperations_ShouldCaptureMessages()
    {
        // Arrange
        await NavigateToCustomers();
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Fill valid form data using proper DTO field names
        var formData = new Dictionary<string, string>
        {
            ["Name"] = $"Snackbar Test Company {DateTime.Now.Ticks}", // CustomerDto.Name property
            ["Email"] = $"test.{DateTime.Now.Ticks}@example.com", // CustomerDto.Email property
            ["Telephone"] = "0123456789", // CustomerDto.Telephone property
            ["Industry"] = "Technology" // CustomerDto.Industry property
        };
        
        await Page.FillFormAndValidate(formData);
        await Page.SelectMudSelectOption("Customer Type", "Corporate");
        
        // Act & Assert
        await Page.SubmitFormAndWait("Save");
        
        var snackbarMessage = await Page.GetSnackbarMessage();
        snackbarMessage.Should().NotBeNullOrEmpty("Should receive snackbar message after form submission");
        snackbarMessage.Should().Contain("success", "Snackbar should contain success message");
    }

    [Test]
    public async Task FormHelpers_FormSubmissionFlow_ShouldHandleValidationAndSuccess()
    {
        // Arrange
        await NavigateToCustomers();
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act & Assert - Test submission with validation errors using proper DTO field names
        var invalidFormData = new Dictionary<string, string>
        {
            ["Name"] = "", // Required field left empty (CustomerDto.Name property)
            ["Email"] = "invalid-email-format" // CustomerDto.Email property with invalid format
        };
        
        // Try to fill form with invalid data - should detect validation errors
        var validationFailed = false;
        try
        {
            await Page.FillFormAndValidate(invalidFormData, expectErrors: false);
        }
        catch (Exception ex)
        {
            validationFailed = true;
            ex.Message.Should().Contain("Unexpected validation errors");
        }
        
        validationFailed.Should().BeTrue("Should fail validation with invalid form data");
        
        // Test successful submission using proper DTO field names
        var validFormData = new Dictionary<string, string>
        {
            ["Name"] = $"Successful Test Company {DateTime.Now.Ticks}", // CustomerDto.Name property
            ["Email"] = $"success.{DateTime.Now.Ticks}@example.com", // CustomerDto.Email property
            ["Telephone"] = "0123456789", // CustomerDto.Telephone property
            ["Industry"] = "Technology" // CustomerDto.Industry property
        };
        
        await Page.FillFormAndValidate(validFormData);
        await Page.SelectMudSelectOption("Customer Type", "Corporate");
        await Page.SubmitFormAndWait("Save");
        
        var message = await Page.GetSnackbarMessage();
        message.Should().Contain("success", "Should receive success message after valid submission");
    }

    [Test]
    public async Task FormHelpers_ErrorHandling_ShouldProvideDetailedErrorInformation()
    {
        // Arrange
        await NavigateToCustomers();
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act & Assert - Test getting all validation errors using proper DTO field names
        await Page.FillMudTextField("Email", "invalid"); // CustomerDto.Email property
        await Page.FillMudTextField("Telephone", "abc"); // CustomerDto.Telephone property (invalid format)
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        var allErrors = await Page.GetValidationErrors();
        allErrors.Should().NotBeEmpty("Should return validation errors");
        allErrors.Should().HaveCountGreaterThan(0, "Should have at least one validation error");
        
        // Test specific field error retrieval
        var emailError = await Page.GetFieldValidationError("Email");
        emailError.Should().NotBeNullOrWhiteSpace("Should return specific email validation error");
        
        // Test non-existent field error
        var nonExistentError = await Page.GetFieldValidationError("NonExistentField");
        nonExistentError.Should().BeNull("Should return null for non-existent field");
    }

    #region Helper Methods

    private async Task EnsureLoggedIn()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        var loginVisible = await Page.IsVisibleAsync("button:has-text('Log in'), .mud-button:has-text('Log in')");
        if (loginVisible)
        {
            await Page.ClickMudButton("Log in");
            await Page.WaitForTimeoutAsync(5000);
        }
    }

    private async Task NavigateToCustomers()
    {
        await NavigateTo("/customers");
        await Page.WaitForSelectorAsync("h1:has-text('Customers'), .mud-typography-h4:has-text('Customers')");
    }

    private async Task NavigateToStaff()
    {
        await NavigateTo("/staff");
        await Page.WaitForSelectorAsync("h1:has-text('Staff'), .mud-typography-h4:has-text('Staff')");
    }

    private async Task NavigateToDiary()
    {
        await NavigateTo("/diary");
        await Page.WaitForSelectorAsync("h1:has-text('Diary'), .mud-typography-h4:has-text('Diary')");
    }

    private async Task NavigateToCaseManagement()
    {
        await NavigateTo("/case-management");
        await Page.WaitForSelectorAsync("h1:has-text('Case Management'), .mud-typography-h4:has-text('Case Management')");
    }

    private async Task NavigateToFinance()
    {
        await NavigateTo("/finance");
        await Page.WaitForSelectorAsync("h1:has-text('Finance'), .mud-typography-h4:has-text('Finance')");
    }

    private async Task CreateNewCustomer(string customerName)
    {
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Updated to match CustomerDto properties exactly
        var customerData = new Dictionary<string, string>
        {
            ["Name"] = customerName, // CustomerDto.Name property
            ["Email"] = $"test.{DateTime.Now.Ticks}@example.com", // CustomerDto.Email property
            ["Telephone"] = "0123456789", // CustomerDto.Telephone property
            ["Address"] = "123 Test Street, Test City", // CustomerDto.Address property
            ["Postcode"] = "TE5T 1NG", // CustomerDto.Postcode property
            ["Website"] = "https://testcompany.example.com", // CustomerDto.Website property
            ["Industry"] = "Technology", // CustomerDto.Industry property
            ["NumberOfEmployees"] = "50" // CustomerDto.NumberOfEmployees property
        };
        
        await Page.FillFormAndValidate(customerData);
        await Page.SelectMudSelectOption("Customer Type", "Corporate");
        await Page.SubmitFormAndWait("Save");
        
        var message = await Page.GetSnackbarMessage();
        message.Should().Contain("Customer created successfully");
        await Page.WaitForTimeoutAsync(2000);
    }

    private async Task<string> CreateEmployeeForCustomer(string customerName)
    {
        await Page.FillMudTextField("Search", customerName);
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.ClickAsync($"tr:has-text('{customerName}')");
        await Page.WaitForSelectorAsync(".customer-details, .mud-tabs");
        
        await Page.ClickAsync(".mud-tab:has-text('Employees')");
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.ClickMudButton("Add Employee");
        await Page.WaitForSelectorAsync(".employee-form, .mud-dialog-container");
        
        // Updated to match EmployeeDto properties exactly
        var employeeData = new Dictionary<string, string>
        {
            ["FirstName"] = "Jane", // EmployeeDto.FirstName property
            ["LastName"] = "Doe", // EmployeeDto.LastName property
            ["Email"] = $"jane.doe.{DateTime.Now.Ticks}@example.com", // EmployeeDto.Email property
            ["Telephone"] = "0987654321", // EmployeeDto.Telephone property
            ["JobRole"] = "Manager", // EmployeeDto.JobRole property
            ["Department"] = "Human Resources", // EmployeeDto.Department property
            ["Address1"] = "456 Employee Street", // EmployeeDto.Address1 property
            ["Postcode"] = "EMP 123", // EmployeeDto.Postcode property
            ["LineManager"] = "John Smith" // EmployeeDto.LineManager property
        };
        
        await Page.FillFormAndValidate(employeeData);
        await Page.SetMudDatePicker("Start Date", DateTime.Today);
        await Page.SubmitFormAndWait("Save");
        
        var message = await Page.GetSnackbarMessage();
        message.Should().Contain("Employee created successfully");
        
        return $"{employeeData["First Name"]} {employeeData["Last Name"]}";
    }

    private async Task<string> CreateNewClinician()
    {
        await Page.ClickMudButton("Add Clinician");
        await Page.WaitForSelectorAsync(".clinician-form, .mud-dialog-container");
        
        // Updated to match ClinicianDto properties exactly
        var clinicianData = new Dictionary<string, string>
        {
            ["FirstName"] = "Dr. Sarah", // ClinicianDto.FirstName property
            ["LastName"] = "Johnson", // ClinicianDto.LastName property
            ["Email"] = $"dr.johnson.{DateTime.Now.Ticks}@example.com", // ClinicianDto.Email property
            ["Telephone"] = "0555123456", // ClinicianDto.Telephone property
            ["LicenceNumber"] = $"GMC{DateTime.Now.Ticks % 100000}", // ClinicianDto.LicenceNumber property
            ["AvatarTitle"] = "Occupational Health Physician", // ClinicianDto.AvatarTitle property
            ["AvatarDescription"] = "Experienced OH doctor specializing in workplace health" // ClinicianDto.AvatarDescription property
        };
        
        await Page.FillFormAndValidate(clinicianData);
        await Page.SelectMudSelectOption("Qualification", "Doctor");
        await Page.SubmitFormAndWait("Save");
        
        var message = await Page.GetSnackbarMessage();
        message.Should().Contain("Clinician created successfully");
        
        return $"{clinicianData["First Name"]} {clinicianData["Last Name"]}";
    }

    private async Task<string> CreateReferralForCustomer(string customerName, string employeeName, string referralReference)
    {
        await Page.FillMudTextField("Search", customerName);
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.ClickAsync($"tr:has-text('{customerName}')");
        await Page.WaitForSelectorAsync(".customer-details, .mud-tabs");
        
        await Page.ClickAsync(".mud-tab:has-text('Referrals')");
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.ClickMudButton("Add Referral");
        await Page.WaitForSelectorAsync(".referral-form, .mud-dialog-container");
        
        // Updated to match ReferralDto properties exactly
        var referralData = new Dictionary<string, string>
        {
            ["Title"] = $"Health Assessment {referralReference}", // ReferralDto.Title property
            ["ReferralDetails"] = "Annual occupational health assessment required for employee", // ReferralDto.ReferralDetails property
        };
        
        await Page.FillFormAndValidate(referralData);
        await Page.SelectMudSelectOption("Employee", employeeName);
        await Page.SelectMudSelectOption("Service Type", "Health Assessment");
        await Page.SetMudDatePicker("Referral Date", DateTime.Today);
        await Page.SubmitFormAndWait("Save");
        
        var message = await Page.GetSnackbarMessage();
        message.Should().Contain("Referral created successfully");
        
        return referralReference;
    }

    private async Task CreateAppointmentFromReferral(string referralId, string clinicianName, DateTime appointmentDate)
    {
        await Page.ClickMudButton("Add Appointment");
        await Page.WaitForSelectorAsync(".appointment-form, .mud-dialog-container");
        
        await Page.SelectMudSelectOption("Referral", referralId);
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.SetMudDatePicker("Appointment Date", appointmentDate);
        await Page.SelectMudSelectOption("Time", "09:00");
        await Page.SelectMudSelectOption("Duration", "60 minutes");
        await Page.SelectMudSelectOption("Clinician", clinicianName);
        await Page.SelectMudSelectOption("Location", "Main Clinic");
        
        var appointmentData = new Dictionary<string, string>
        {
            ["Notes"] = "Scheduled health assessment appointment"
        };
        
        await Page.FillFormAndValidate(appointmentData);
        await Page.SubmitFormAndWait("Save");
        
        var message = await Page.GetSnackbarMessage();
        message.Should().Contain("Appointment created successfully");
    }

    private async Task VerifyAppointmentInGrid(string referralId, string customerName, string employeeName, string clinicianName)
    {
        await Page.FillMudTextField("Search", referralId);
        await Page.WaitForTimeoutAsync(1000);
        
        var appointmentRow = await Page.WaitForSelectorAsync($"tr:has-text('{referralId}')");
        appointmentRow.Should().NotBeNull("Appointment should appear in case management grid");
        
        var rowText = await appointmentRow!.TextContentAsync();
        rowText.Should().Contain(customerName);
        rowText.Should().Contain(employeeName);
        rowText.Should().Contain(clinicianName);
    }

    private async Task EnterClinicalDetails(string referralId)
    {
        await Page.ClickAsync($"tr:has-text('{referralId}')");
        await Page.WaitForSelectorAsync(".case-details, .clinical-form");
        
        var clinicalTabExists = await Page.IsVisibleAsync(".mud-tab:has-text('Clinical Details')");
        if (clinicalTabExists)
        {
            await Page.ClickAsync(".mud-tab:has-text('Clinical Details')");
            await Page.WaitForTimeoutAsync(1000);
        }
        
        var clinicalData = new Dictionary<string, string>
        {
            ["Blood Pressure"] = "120/80",
            ["Heart Rate"] = "72",
            ["Temperature"] = "37.0",
            ["Weight"] = "70",
            ["Height"] = "175",
            ["Clinical Notes"] = "Patient appears healthy, all vitals within normal range",
            ["Recommendations"] = "Continue regular exercise, maintain healthy diet"
        };
        
        await Page.FillFormAndValidate(clinicalData);
        await Page.SelectMudSelectOption("Assessment Outcome", "Fit for Work");
        await Page.SetMudDatePicker("Completion Date", DateTime.Today);
        await Page.SubmitFormAndWait("Save Clinical Details");
        
        var message = await Page.GetSnackbarMessage();
        message.Should().Contain("Clinical details saved successfully");
    }

    private async Task GenerateInvoiceForCustomer(string customerName, DateTime appointmentDate)
    {
        await Page.ClickMudButton("Generate Invoice");
        await Page.WaitForSelectorAsync(".invoice-form, .mud-dialog-container");
        
        await Page.SelectMudSelectOption("Customer", customerName);
        await Page.SetMudDatePicker("From Date", appointmentDate);
        await Page.SetMudDatePicker("To Date", appointmentDate);
        
        var invoiceData = new Dictionary<string, string>
        {
            ["Invoice Description"] = $"Health Assessment Services for {DateTime.Now:MMMM yyyy}"
        };
        
        await Page.FillFormAndValidate(invoiceData);
        await Page.SubmitFormAndWait("Generate");
        
        var message = await Page.GetSnackbarMessage();
        message.Should().Contain("Invoice generated successfully");
        await Page.WaitForTimeoutAsync(2000);
    }

    private async Task VerifyInvoiceBilling(string customerName, string referralId, string employeeName)
    {
        await Page.ClickAsync(".mud-tab:has-text('Invoices'), a:has-text('View Invoices')");
        await Page.WaitForTimeoutAsync(1000);
        
        await Page.FillMudTextField("Search", customerName);
        await Page.WaitForTimeoutAsync(1000);
        
        var invoiceRow = await Page.WaitForSelectorAsync($"tr:has-text('{customerName}')");
        invoiceRow.Should().NotBeNull("Invoice should appear in invoices list");
        
        await invoiceRow!.ClickAsync();
        await Page.WaitForSelectorAsync(".invoice-details");
        
        var invoiceContent = await Page.TextContentAsync(".invoice-details");
        invoiceContent.Should().Contain(referralId, "Invoice should reference our referral");
        invoiceContent.Should().Contain("Health Assessment", "Invoice should contain the service type");
        invoiceContent.Should().Contain(employeeName, "Invoice should contain employee name");
        
        var amountElement = await Page.QuerySelectorAsync(".invoice-total, .total-amount");
        amountElement.Should().NotBeNull("Invoice should have a total amount");
        
        var amountText = await amountElement!.TextContentAsync();
        amountText.Should().NotBeNullOrEmpty("Invoice amount should not be empty");
        amountText.Should().NotContain("£0.00", "Invoice amount should not be zero");
        
        TestContext.WriteLine($"Invoice generated successfully with amount: {amountText}");
    }

    #endregion
}
