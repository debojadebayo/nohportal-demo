using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Navigation;

[TestFixture]
public class NavigationTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
    }

    [Test]
    public async Task Navigation_AllMainPages_ShouldLoadSuccessfully()
    {
        var pagesToTest = new[]
        {
            ("/", "Diary"),
            ("/customers", "Customers"),
            ("/employees", "Employees"),
            ("/clinicians", "Clinicians"),
            ("/document-library", "Documents"),
            ("/case-management", "Case Management"),
            ("/finance", "Finance"),
            ("/settings", "Settings")
        };

        foreach (var (route, expectedTitle) in pagesToTest)
        {
            await NavigateTo(route);
            
            // Check that page loaded without errors
            var pageTitle = await Page.TextContentAsync("h1, .mud-typography-h1");
            pageTitle.Should().Contain(expectedTitle, $"Page {route} should display correct title");
            
            // Check that MudBlazor components are rendered
            var mudComponents = await Page.QuerySelectorAllAsync(".mud-paper, .mud-button, .mud-typography");
            mudComponents.Should().NotBeEmpty($"Page {route} should contain MudBlazor components");
        }
    }

    [Test]
    public async Task Navigation_MenuLinks_ShouldWork()
    {
        // Test navigation menu links
        await Page.ClickAsync(".mud-nav-link:has-text('Customers')");
        await WaitForPageReady();
        
        Page.Url.Should().Contain("/customers", "Customer menu link should navigate to customers page");
        
        await Page.ClickAsync(".mud-nav-link:has-text('Employees')");
        await WaitForPageReady();
        
        Page.Url.Should().Contain("/employees", "Employee menu link should navigate to employees page");
        
        await Page.ClickAsync(".mud-nav-link:has-text('Finance')");
        await WaitForPageReady();
        
        Page.Url.Should().Contain("/finance", "Finance menu link should navigate to finance page");
    }

    [Test]
    public async Task Navigation_BreadcrumbsOrBackButtons_ShouldWork()
    {
        // Navigate to a detail page if available
        await NavigateTo("/customers");
        
        // Look for a way to navigate to detail view
        var customerLink = await Page.QuerySelectorAsync(".mud-data-grid .mud-button, .mud-table .mud-button");
        if (customerLink != null)
        {
            var originalUrl = Page.Url;
            await customerLink.ClickAsync();
            await WaitForPageReady();
            
            // Check if URL changed to detail view
            if (Page.Url != originalUrl)
            {
                // Look for back button or breadcrumb
                var backButton = await Page.QuerySelectorAsync(".mud-button:has-text('Back'), .mud-breadcrumbs .mud-button");
                if (backButton != null)
                {
                    await backButton.ClickAsync();
                    await WaitForPageReady();
                    
                    Page.Url.Should().Contain("/customers", "Back button should return to customers list");
                }
            }
        }
    }
}

[TestFixture]
public class AuthenticationTests : PlaywrightTestBase
{
    [Test]
    public async Task Login_ValidCredentials_ShouldSucceed()
    {
        // Start from logged out state
        await Page.GotoAsync($"{BaseUrl}/authentication/logout");
        await WaitForPageReady();
        
        // Login with valid credentials
        await LoginAsTestUser();
        
        // Should be redirected to main application
        Page.Url.Should().NotContain("/authentication", "Should be redirected away from authentication page after login");
        
        // Should see navigation menu
        var navMenu = await Page.QuerySelectorAsync(".mud-nav-menu, .mud-navigation");
        navMenu.Should().NotBeNull("Should see navigation menu after successful login");
    }

    [Test]
    public async Task Login_InvalidCredentials_ShouldShowError()
    {
        await Page.GotoAsync($"{BaseUrl}/authentication/logout");
        await WaitForPageReady();
        
        await Page.GotoAsync($"{BaseUrl}/authentication");
        await WaitForPageReady();
        
        // Try login with invalid credentials
        await Page.FillAsync("input[type='email'], input[name='email']", "invalid@example.com");
        await Page.FillAsync("input[type='password'], input[name='password']", "wrongpassword");
        await Page.ClickAsync("button[type='submit'], .mud-button:has-text('Login'), .mud-button:has-text('Sign In')");
        
        // Should show error message
        var errorMessage = await Page.WaitForSelectorAsync(".mud-alert, .error-message, .mud-snackbar", new() { Timeout = 10000 });
        errorMessage.Should().NotBeNull("Should show error message for invalid credentials");
    }

    [Test]
    public async Task Logout_ShouldRedirectToLogin()
    {
        await LoginAsTestUser();
        
        // Logout
        await Logout();
        
        // Should be redirected to authentication page
        Page.Url.Should().Contain("/authentication", "Should be redirected to authentication page after logout");
    }

    [Test]
    public async Task ProtectedPages_WithoutAuth_ShouldRedirectToLogin()
    {
        // Start from logged out state
        await Page.GotoAsync($"{BaseUrl}/authentication/logout");
        await WaitForPageReady();
        
        // Try to access protected page
        await Page.GotoAsync($"{BaseUrl}/customers");
        await WaitForPageReady();
        
        // Should be redirected to authentication
        Page.Url.Should().Contain("/authentication", "Protected pages should redirect to authentication when not logged in");
    }
}
