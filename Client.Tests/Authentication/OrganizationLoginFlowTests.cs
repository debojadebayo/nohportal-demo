using Client.Tests.Infrastructure;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Client.Tests.Authentication;

[TestFixture]
public class OrganizationLoginFlowTests : PlaywrightTestBase
{
    [Test]
    public async Task LoginFlow_WithOrganizationAuth_ShouldCompleteSuccessfully()
    {
        // Arrange
        var (username, password) = KeycloakAuth.GetAdminCredentials();
        
        // Ensure we start logged out
        await EnsureLoggedOut();
        
        // Act - Navigate to application home page
        await Page.GotoAsync(BaseUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
        
        // Should see the login button since we're not authenticated
        var loginButton = await Page.WaitForSelectorAsync("button:has-text('Log in'), .mud-button:has-text('Log in')", new() { Timeout = 10000 });
        loginButton.Should().NotBeNull("Login button should be visible when not authenticated");
        
        // Click the login button
        await loginButton.ClickAsync();
        
        // Wait for redirect to Keycloak - this might take us to organization selection first
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Page.WaitForTimeoutAsync(2000); // Allow for redirects
        
        // Check if we're on Keycloak (either organization selection or username input)
        var currentUrl = Page.Url;
        currentUrl.Should().Contain("localhost:8180", "Should be redirected to Keycloak");
        
        await HandleOrganizationAuthFlow(username, password);
        
        // Wait for redirect back to application
        await Page.WaitForURLAsync(url => 
            !url.Contains("localhost:8180") && 
            !url.Contains("/authentication/login"), 
            new PageWaitForURLOptions { Timeout = 30000 });
        
        // Wait for MudBlazor to initialize
        await WaitForMudBlazorReady();
        
        // Assert - Should be logged in and back at the application
        Page.Url.Should().StartWith(BaseUrl, "Should be back at the application");
        Page.Url.Should().NotContain("localhost:8180", "Should not be on Keycloak anymore");
        Page.Url.Should().NotContain("/authentication", "Should not be on authentication page");
        
        // Verify authentication state by looking for logout button
        var logoutButton = await Page.WaitForSelectorAsync("button:has-text('Log out'), .mud-button:has-text('Log out')", new() { Timeout = 10000 });
        logoutButton.Should().NotBeNull("Logout button should be visible when authenticated");
        
        // Verify navigation menu is available (only visible when authenticated)
        var navMenu = await Page.QuerySelectorAsync(".mud-nav-menu, .mud-drawer");
        navMenu.Should().NotBeNull("Navigation menu should be visible when authenticated");
        
        TestContext.WriteLine($"Successfully logged in. Final URL: {Page.Url}");
    }
    
    [Test]
    public async Task LoginFlow_DirectToKeycloakUrl_ShouldWorkWithOrganizations()
    {
        // Arrange
        var (username, password) = KeycloakAuth.GetAdminCredentials();
        
        // Ensure we start logged out
        await EnsureLoggedOut();
        
        // Act - Navigate directly to Keycloak auth URL
        var authUrl = KeycloakAuth.BuildAuthUrl(BaseUrl);
        await Page.GotoAsync(authUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
        
        await HandleOrganizationAuthFlow(username, password);
        
        // Wait for redirect back to application
        await Page.WaitForURLAsync(url => 
            !url.Contains("localhost:8180"), 
            new PageWaitForURLOptions { Timeout = 30000 });
        
        // Wait for app to initialize
        await WaitForMudBlazorReady();
        
        // Assert
        Page.Url.Should().StartWith(BaseUrl);
        var isAuthenticated = await IsAuthenticated();
        isAuthenticated.Should().BeTrue("Should be authenticated after direct Keycloak login");
        
        TestContext.WriteLine($"Successfully logged in via direct Keycloak URL. Final URL: {Page.Url}");
    }
    
    /// <summary>
    /// Handles the organization-based authentication flow in Keycloak
    /// This method deals with the two-step process: username first, then password
    /// </summary>
    private async Task HandleOrganizationAuthFlow(string username, string password)
    {
        try
        {
            // Wait for page to load
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await Page.WaitForTimeoutAsync(1000);
            
            // Take initial screenshot for debugging
            var initialScreenshotPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots", $"initial_login_page_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            Directory.CreateDirectory(Path.GetDirectoryName(initialScreenshotPath)!);
            await Page.ScreenshotAsync(new() { Path = initialScreenshotPath, FullPage = true });
            TestContext.WriteLine($"Initial login page screenshot: {initialScreenshotPath}");
            TestContext.WriteLine($"Initial URL: {Page.Url}");
            
            // Step 1: Check what's on the page
            var usernameInputExists = await Page.IsVisibleAsync("input[name='username'], input[id='username'], #username");
            var passwordInputExists = await Page.IsVisibleAsync("input[name='password'], input[id='password'], #password");
            
            TestContext.WriteLine($"Username input visible: {usernameInputExists}");
            TestContext.WriteLine($"Password input visible: {passwordInputExists}");
            
            if (usernameInputExists && !passwordInputExists)
            {
                TestContext.WriteLine("Organization auth step 1: Username input detected");
                
                // Fill username and continue
                await Page.FillAsync("input[name='username'], input[id='username'], #username", username);
                await Page.WaitForTimeoutAsync(500);
                
                // Look for submit button using safer methods
                var submitButtonFound = false;
                var buttonSelectors = new[]
                {
                    "input[type='submit']",
                    "button[type='submit']", 
                    "#kc-login",
                    "button:has-text('Continue')",
                    "button:has-text('Next')",
                    ".btn-primary"
                };
                
                foreach (var selector in buttonSelectors)
                {
                    if (await Page.IsVisibleAsync(selector))
                    {
                        await Page.ClickAsync(selector);
                        TestContext.WriteLine($"Clicked button with selector: {selector}");
                        submitButtonFound = true;
                        break;
                    }
                }
                
                if (!submitButtonFound)
                {
                    // Try pressing Enter if no button found
                    await Page.Keyboard.PressAsync("Enter");
                    TestContext.WriteLine("Pressed Enter after username input");
                }
                
                // Wait for navigation to password page
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                await Page.WaitForTimeoutAsync(3000);
                
                // Take screenshot after username submission
                var usernameSubmittedPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots", $"after_username_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                await Page.ScreenshotAsync(new() { Path = usernameSubmittedPath, FullPage = true });
                TestContext.WriteLine($"After username submission screenshot: {usernameSubmittedPath}");
                TestContext.WriteLine($"URL after username: {Page.Url}");
                
                // Step 2: Wait for password input page
                TestContext.WriteLine("Organization auth step 2: Waiting for password input");
                
                // Wait for password input to appear
                await Page.WaitForSelectorAsync("input[name='password'], input[id='password'], #password", new() { Timeout = 15000 });
                
                // Verify password input is visible
                var passwordVisible = await Page.IsVisibleAsync("input[name='password'], input[id='password'], #password");
                TestContext.WriteLine($"Password input visible after wait: {passwordVisible}");
                
                if (!passwordVisible)
                {
                    throw new Exception("Password input not visible after waiting");
                }
                
                await Page.FillAsync("input[name='password'], input[id='password'], #password", password);
                await Page.WaitForTimeoutAsync(500);
                
                // Submit the password form
                var passwordSubmitFound = false;
                var passwordButtonSelectors = new[]
                {
                    "input[type='submit']",
                    "button[type='submit']", 
                    "#kc-login",
                    "button:has-text('Sign In')",
                    "button:has-text('Login')",
                    ".btn-primary"
                };
                
                foreach (var selector in passwordButtonSelectors)
                {
                    if (await Page.IsVisibleAsync(selector))
                    {
                        await Page.ClickAsync(selector);
                        TestContext.WriteLine($"Clicked password submit button with selector: {selector}");
                        passwordSubmitFound = true;
                        break;
                    }
                }
                
                if (!passwordSubmitFound)
                {
                    await Page.Keyboard.PressAsync("Enter");
                    TestContext.WriteLine("Pressed Enter after password input");
                }
            }
            else if (usernameInputExists && passwordInputExists)
            {
                // Traditional single-step login
                TestContext.WriteLine("Traditional single-step login detected");
                await Page.FillAsync("input[name='username'], input[id='username'], #username", username);
                await Page.FillAsync("input[name='password'], input[id='password'], #password", password);
                await Page.WaitForTimeoutAsync(500);
                
                var loginButtonSelectors = new[]
                {
                    "input[type='submit']",
                    "button[type='submit']", 
                    "#kc-login",
                    "button:has-text('Sign In')",
                    "button:has-text('Login')",
                    ".btn-primary"
                };
                
                var loginButtonFound = false;
                foreach (var selector in loginButtonSelectors)
                {
                    if (await Page.IsVisibleAsync(selector))
                    {
                        await Page.ClickAsync(selector);
                        TestContext.WriteLine($"Clicked login button with selector: {selector}");
                        loginButtonFound = true;
                        break;
                    }
                }
                
                if (!loginButtonFound)
                {
                    await Page.Keyboard.PressAsync("Enter");
                    TestContext.WriteLine("Pressed Enter for traditional login");
                }
            }
            else
            {
                throw new Exception($"Could not find expected login inputs. Username visible: {usernameInputExists}, Password visible: {passwordInputExists}, URL: {Page.Url}");
            }
            
            // Wait for authentication to complete
            await Page.WaitForTimeoutAsync(2000);
            TestContext.WriteLine($"Authentication flow completed. Final URL: {Page.Url}");
        }
        catch (Exception ex)
        {
            // Take a screenshot for debugging
            var screenshotPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots", $"login_failure_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);
            await Page.ScreenshotAsync(new() { Path = screenshotPath, FullPage = true });
            TestContext.WriteLine($"Login failed. Screenshot saved: {screenshotPath}");
            TestContext.WriteLine($"Current URL: {Page.Url}");
            
            throw new Exception($"Organization authentication flow failed: {ex.Message}", ex);
        }
    }
    
    /// <summary>
    /// Ensures the user is logged out before starting a test
    /// </summary>
    private async Task EnsureLoggedOut()
    {
        try
        {
            // Try to logout if currently authenticated
            if (await IsAuthenticated())
            {
                await Logout();
                await Page.WaitForTimeoutAsync(2000);
            }
            
            // Navigate to home and verify we're logged out
            await Page.GotoAsync(BaseUrl);
            await Page.WaitForTimeoutAsync(2000);
            
            // If we're still authenticated, try direct Keycloak logout
            if (await IsAuthenticated())
            {
                await KeycloakAuth.LogoutAsync(Page, BaseUrl);
                await Page.WaitForTimeoutAsync(2000);
            }
        }
        catch (Exception ex)
        {
            TestContext.WriteLine($"Warning: Could not ensure logout state: {ex.Message}");
            // Continue with test - the login flow should handle any existing authentication
        }
    }
}
