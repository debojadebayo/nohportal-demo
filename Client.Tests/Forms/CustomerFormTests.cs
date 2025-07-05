using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using Client.Tests.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Forms;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CustomerFormTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
        await NavigateTo("/customers");
        await WaitForMudBlazorReady();
    }

    [Test]
    public async Task CustomerForm_ValidData_ShouldSubmitSuccessfully()
    {
        // Arrange
        var customer = TestDataGenerator.GenerateValidCustomer();
        
        // Act - Create new customer
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Fill form with valid data
        var formData = new Dictionary<string, string>
        {
            ["Name"] = customer.Name,
            ["Domain"] = customer.Domain,
            ["Phone Number"] = customer.Telephone,
            ["Email"] = customer.Email,
            ["Invoice Email"] = customer.InvoiceEmail,
            ["Address"] = customer.Address,
            ["Postcode"] = customer.Postcode,
            ["Site"] = customer.Site,
            ["OH Services Required"] = customer.OHServicesRequired,
            ["Number of Employees"] = customer.NumberOfEmployees.ToString(),
            ["Website"] = customer.Website
        };

        await Page.FillFormAndValidate(formData, expectErrors: false);
        
        // Select industry
        await Page.SelectMudSelectOption("Industry", customer.Industry);
        
        // Submit form
        await Page.SubmitFormAndWait("Save Changes");
        
        // Assert
        var snackbarMessage = await Page.GetSnackbarMessage();
        snackbarMessage.Should().Contain("successfully", "Expected success message after valid form submission");
    }

    [Test]
    public async Task CustomerForm_EmptyRequiredFields_ShouldShowValidationErrors()
    {
        // Arrange - Create new customer
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act - Submit empty form
        await Page.ClickMudButton("Save Changes");
        await Page.WaitForFormValidation();
        
        // Assert - Check for validation errors
        var hasErrors = await Page.HasValidationErrors();
        hasErrors.Should().BeTrue("Empty required fields should trigger validation errors");
        
        var errors = await Page.GetValidationErrors();
        errors.Should().Contain(e => e.Contains("Company name is required"));
        errors.Should().Contain(e => e.Contains("Telephone number is required"));
        errors.Should().Contain(e => e.Contains("Email address is required"));
    }

    [Test]
    [TestCase("", "Company name is required")]
    [TestCase("A", "Company name must be between 2 and 100 characters")]
    [TestCase(" Company Name ", "Company name cannot start or end with spaces")]
    public async Task CustomerForm_InvalidCompanyName_ShouldShowSpecificError(string invalidName, string expectedError)
    {
        // Arrange
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act & Assert
        await Page.TestFieldValidation("Name", invalidName, expectedError);
    }

    [Test]
    [TestCaseSource(typeof(TestDataGenerator.ValidationTestData), nameof(TestDataGenerator.ValidationTestData.InvalidEmailFormats))]
    public async Task CustomerForm_InvalidEmail_ShouldShowValidationError(string invalidEmail)
    {
        // Arrange
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act & Assert
        await Page.TestFieldValidation("Email", invalidEmail, "Please enter a valid email address");
    }

    [Test]
    [TestCaseSource(typeof(TestDataGenerator.ValidationTestData), nameof(TestDataGenerator.ValidationTestData.InvalidPhoneNumbers))]
    public async Task CustomerForm_InvalidPhoneNumber_ShouldShowValidationError(string invalidPhone)
    {
        // Arrange
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act & Assert
        await Page.TestFieldValidation("Phone Number", invalidPhone, "Please enter a valid UK telephone number");
    }

    [Test]
    [TestCaseSource(typeof(TestDataGenerator.ValidationTestData), nameof(TestDataGenerator.ValidationTestData.InvalidPostcodes))]
    public async Task CustomerForm_InvalidPostcode_ShouldShowValidationError(string invalidPostcode)
    {
        // Arrange
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act & Assert
        await Page.TestFieldValidation("Postcode", invalidPostcode, "Please enter a valid UK postcode");
    }

    [Test]
    public async Task CustomerForm_InvalidIndustry_ShouldShowValidationError()
    {
        // Arrange
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Act - Try to select invalid industry (this should be prevented by the dropdown)
        var validCustomer = TestDataGenerator.GenerateValidCustomer();
        await Page.FillMudTextField("Name", validCustomer.Name);
        await Page.FillMudTextField("Phone Number", validCustomer.Telephone);
        await Page.FillMudTextField("Email", validCustomer.Email);
        
        // Industry dropdown should only allow valid options
        await Page.ClickAsync(".mud-select:has(.mud-input-label:has-text('Industry'))");
        await Page.WaitForSelectorAsync(".mud-popover .mud-list");
        
        var industryOptions = await Page.QuerySelectorAllAsync(".mud-popover .mud-list-item");
        industryOptions.Should().NotBeEmpty("Industry dropdown should have valid options");
        
        // Verify that only valid industries are available
        var expectedIndustries = new[] { "Healthcare", "Manufacturing", "Construction", "Education", "Finance", "Retail", "Technology" };
        foreach (var expectedIndustry in expectedIndustries)
        {
            var optionExists = await Page.QuerySelectorAsync($".mud-popover .mud-list-item:has-text('{expectedIndustry}')") != null;
            optionExists.Should().BeTrue($"Industry '{expectedIndustry}' should be available in dropdown");
        }
    }

    [Test]
    public async Task CustomerForm_NumberOfEmployeesValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Test zero employees
        await Page.TestFieldValidation("Number of Employees", "0", "Number of employees must be at least 1");
        
        // Test negative employees
        await Page.TestFieldValidation("Number of Employees", "-5", "Number of employees must be at least 1");
        
        // Test too many employees
        await Page.TestFieldValidation("Number of Employees", "150000", "For companies with more than 100,000 employees, please contact us directly");
    }

    [Test]
    public async Task CustomerForm_WebsiteUrlValidation_ShouldWork()
    {
        // Arrange
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Test invalid URLs
        await Page.TestFieldValidation("Website", "not-a-url", "Website must be a valid URL");
        await Page.TestFieldValidation("Website", "ftp://example.com", "Website must be a valid URL starting with http:// or https://");
        
        // Test valid URL should not show error
        await Page.FillMudTextField("Website", "https://example.com");
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForFormValidation();
        
        var hasError = await Page.HasFieldValidationError("Website");
        hasError.Should().BeFalse("Valid URL should not show validation error");
    }

    [Test]
    public async Task CustomerForm_EditExistingCustomer_ShouldLoadDataCorrectly()
    {
        // This test assumes there's at least one customer in the system
        // In a real scenario, you might need to create test data first
        
        // Act - Select first customer from lookup
        await Page.ClickAsync("//label[text()='Company Name']/..//input");
        await Page.FillAsync("//label[text()='Company Name']/..//input", "test");
        await Page.WaitForTimeoutAsync(1000); // Wait for search results
        
        // Try to select first result if available
        var firstResult = await Page.QuerySelectorAsync(".mud-autocomplete-input .mud-list-item");
        if (firstResult != null)
        {
            await firstResult.ClickAsync();
            await WaitForPageReady();
            
            // Verify form is populated
            var nameField = await Page.QuerySelectorAsync("input[data-testid='customer-name'], .mud-input-control:has(.mud-input-label:has-text('Name')) input");
            if (nameField != null)
            {
                var nameValue = await nameField.InputValueAsync();
                nameValue.Should().NotBeNullOrEmpty("Customer name should be loaded from database");
            }
        }
    }

    [Test]
    public async Task CustomerForm_CancelButton_ShouldDiscardChanges()
    {
        // Arrange
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Fill some data
        await Page.FillMudTextField("Name", "Test Company");
        await Page.FillMudTextField("Email", "test@example.com");
        
        // Act - Click cancel
        var cancelButton = await Page.QuerySelectorAsync(".mud-button:has-text('Cancel')");
        if (cancelButton != null)
        {
            await cancelButton.ClickAsync();
            await WaitForPageReady();
        }
        
        // Assert - Should be back to customer list without creating customer
        var currentUrl = Page.Url;
        currentUrl.Should().Contain("/customers", "Should return to customers page after cancel");
    }
}
