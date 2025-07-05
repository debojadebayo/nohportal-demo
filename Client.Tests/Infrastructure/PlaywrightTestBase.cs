using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Client.Tests.Infrastructure;

public class PlaywrightTestBase : PageTest
{
    protected IConfiguration Configuration { get; private set; } = null!;
    protected string BaseUrl { get; private set; } = null!;
    protected TimeSpan DefaultTimeout { get; private set; }
    protected KeycloakAuthHelper KeycloakAuth { get; private set; } = null!;

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            IgnoreHTTPSErrors = true,
            AcceptDownloads = true,
            ViewportSize = new() { Width = 1920, Height = 1080 }
        };
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        BaseUrl = Configuration["TestSettings:BaseUrl"] ?? "https://localhost:7001";
        var timeoutMs = int.Parse(Configuration["TestSettings:TimeoutMs"] ?? "30000");
        DefaultTimeout = TimeSpan.FromMilliseconds(timeoutMs);
        
        // Initialize Keycloak helper
        KeycloakAuth = new KeycloakAuthHelper(Configuration);
    }

    [SetUp]
    public async Task SetUp()
    {
        // Set default timeouts
        Page.SetDefaultTimeout((float)DefaultTimeout.TotalMilliseconds);
        Page.SetDefaultNavigationTimeout((float)DefaultTimeout.TotalMilliseconds);
        
        // Navigate to base URL before each test with ignore HTTPS errors for development
        await Page.GotoAsync(BaseUrl, new PageGotoOptions 
        { 
            WaitUntil = WaitUntilState.NetworkIdle 
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        // Take screenshot on failure
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var screenshotPath = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "Screenshots",
                $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
            );
            
            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);
            await Page.ScreenshotAsync(new() { Path = screenshotPath, FullPage = true });
            
            TestContext.WriteLine($"Screenshot saved: {screenshotPath}");
        }
    }

    /// <summary>
    /// Wait for page to be loaded and ready for interaction
    /// </summary>
    protected async Task WaitForPageReady()
    {
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Page.WaitForTimeoutAsync(500); // Small delay to ensure components are rendered
    }

    /// <summary>
    /// Wait for MudBlazor components to be fully rendered
    /// </summary>
    protected async Task WaitForMudBlazorReady()
    {
        try
        {
            Console.WriteLine("Waiting for MudBlazor components to be ready...");
            Console.WriteLine($"Current URL: {Page.Url}");
            
            // First, wait for basic page structure - this should always be present
            await Page.WaitForSelectorAsync("body", new() { Timeout = 5000 });
            
            // Take a screenshot for debugging
            await Page.ScreenshotAsync(new() { Path = $"debug_page_before_mudblazor_wait_{DateTime.Now:yyyyMMdd_HHmmss}.png" });
            
            // Try multiple approaches to detect MudBlazor is ready
            bool mudBlazorReady = false;
            
            // Approach 1: Look for main layout components that should be present after login
            try
            {
                await Page.WaitForSelectorAsync(".mud-layout, .mud-appbar, .mud-drawer, .mud-main-content", new() { Timeout = 5000 });
                Console.WriteLine("Found MudBlazor layout components");
                mudBlazorReady = true;
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Layout components not found, trying alternative selectors...");
            }
            
            // Approach 2: Look for common MudBlazor CSS classes
            if (!mudBlazorReady)
            {
                try
                {
                    await Page.WaitForSelectorAsync(".mud-typography, .mud-button, .mud-paper, .mud-nav-menu", new() { Timeout = 5000 });
                    Console.WriteLine("Found MudBlazor typography/button components");
                    mudBlazorReady = true;
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Typography/button components not found, trying navigation components...");
                }
            }
            
            // Approach 3: Look for navigation components (should be present after auth)
            if (!mudBlazorReady)
            {
                try
                {
                    await Page.WaitForSelectorAsync("a[href='/'], a[href='/customers'], .mud-nav-link", new() { Timeout = 5000 });
                    Console.WriteLine("Found navigation components");
                    mudBlazorReady = true;
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Navigation components not found, trying basic element checks...");
                }
            }
            
            // Approach 4: Check if we have any MudBlazor CSS loaded
            if (!mudBlazorReady)
            {
                try
                {
                    // Check if MudBlazor CSS is loaded by looking for any element with a "mud-" class
                    var mudElements = await Page.QuerySelectorAllAsync("[class*='mud-']");
                    if (mudElements.Count > 0)
                    {
                        Console.WriteLine($"Found {mudElements.Count} elements with mud- classes");
                        mudBlazorReady = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking for mud- elements: {ex.Message}");
                }
            }
            
            // Final approach: Wait for the page to be in a stable state
            if (!mudBlazorReady)
            {
                Console.WriteLine("MudBlazor components not detected, waiting for page stability...");
                // Wait for network idle to ensure page is fully loaded
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 10000 });
                
                // Give a bit more time for components to render
                await Page.WaitForTimeoutAsync(2000);
                
                // Check one more time for basic content
                var pageContent = await Page.TextContentAsync("body");
                if (!string.IsNullOrEmpty(pageContent) && pageContent.Length > 100)
                {
                    Console.WriteLine("Page appears to have content, proceeding...");
                    mudBlazorReady = true;
                }
            }
            
            if (mudBlazorReady)
            {
                Console.WriteLine("MudBlazor components are ready");
                // Small additional wait to ensure full rendering
                await Page.WaitForTimeoutAsync(500);
            }
            else
            {
                Console.WriteLine("WARNING: Could not confirm MudBlazor components are ready, proceeding anyway");
                await Page.ScreenshotAsync(new() { Path = $"debug_mudblazor_not_ready_{DateTime.Now:yyyyMMdd_HHmmss}.png" });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in WaitForMudBlazorReady: {ex.Message}");
            await Page.ScreenshotAsync(new() { Path = $"error_mudblazor_wait_{DateTime.Now:yyyyMMdd_HHmmss}.png" });
            // Don't throw - proceed with test even if we can't confirm MudBlazor is ready
        }
    }

    /// <summary>
    /// Login as a test user
    /// </summary>
    protected async Task LoginAsTestUser()
    {
        var (username, password) = KeycloakAuth.GetTestUserCredentials();
        await KeycloakAuth.AuthenticateAsync(Page, username, password, BaseUrl);
        await WaitForMudBlazorReady();
    }

    /// <summary>
    /// Login as an admin user
    /// </summary>
    protected async Task LoginAsAdmin()
    {
        var (username, password) = KeycloakAuth.GetAdminCredentials();
        await KeycloakAuth.AuthenticateAsync(Page, username, password, BaseUrl);
        await WaitForMudBlazorReady();
    }

    /// <summary>
    /// Login with specific credentials through Keycloak
    /// </summary>
    protected async Task Login(string username, string password)
    {
        await KeycloakAuth.AuthenticateAsync(Page, username, password, BaseUrl);
        await WaitForMudBlazorReady();
    }

    /// <summary>
    /// Logout current user (handles Keycloak logout flow)
    /// </summary>
    protected async Task Logout()
    {
        try
        {
            // Try application logout first
            var logoutSelector = ".mud-button:has-text('Logout'), .mud-menu-item:has-text('Logout'), .mud-list-item:has-text('Logout'), .mud-button:has-text('Log out')";
            
            var logoutButton = await Page.QuerySelectorAsync(logoutSelector);
            if (logoutButton != null)
            {
                await logoutButton.ClickAsync();
                await Page.WaitForTimeoutAsync(2000);
            }
            
            // If still authenticated or on a Keycloak page, use direct logout
            if (KeycloakAuth.IsOnKeycloakLoginPage(Page) || !await IsAuthenticated())
            {
                await KeycloakAuth.LogoutAsync(Page, BaseUrl);
            }
        }
        catch (Exception)
        {
            // Fallback to direct Keycloak logout
            await KeycloakAuth.LogoutAsync(Page, BaseUrl);
        }
    }

    /// <summary>
    /// Wait for a MudBlazor dialog to appear
    /// </summary>
    protected async Task WaitForDialog()
    {
        await Page.WaitForSelectorAsync(".mud-dialog, .mud-dialog-container", new() { Timeout = 5000 });
        await Page.WaitForTimeoutAsync(300); // Allow dialog animation to complete
    }

    /// <summary>
    /// Close any open MudBlazor dialogs
    /// </summary>
    protected async Task CloseDialog()
    {
        var closeButton = ".mud-dialog .mud-button:has-text('Cancel'), .mud-dialog .mud-button:has-text('Close'), .mud-dialog .mud-icon-button";
        try
        {
            await Page.ClickAsync(closeButton, new() { Timeout = 2000 });
            await Page.WaitForTimeoutAsync(300);
        }
        catch (TimeoutException)
        {
            // Alternatively, press Escape key
            await Page.Keyboard.PressAsync("Escape");
        }
    }

    /// <summary>
    /// Wait for a MudBlazor snackbar message
    /// </summary>
    protected async Task<string> WaitForSnackbar()
    {
        var snackbar = await Page.WaitForSelectorAsync(".mud-snackbar", new() { Timeout = 5000 });
        return await snackbar.TextContentAsync() ?? "";
    }

    /// <summary>
    /// Navigate to a specific page and wait for it to load
    /// </summary>
    protected async Task NavigateTo(string relativePath)
    {
        await Page.GotoAsync($"{BaseUrl}{relativePath}");
        await WaitForPageReady();
        await WaitForMudBlazorReady();
    }

    /// <summary>
    /// Check if currently on Keycloak login page
    /// </summary>
    protected bool IsOnKeycloakLoginPage()
    {
        return KeycloakAuth.IsOnKeycloakLoginPage(Page);
    }

    /// <summary>
    /// Wait for Keycloak login page to be ready
    /// </summary>
    protected async Task WaitForKeycloakLogin()
    {
        await KeycloakAuth.WaitForKeycloakReady(Page);
    }

    /// <summary>
    /// Wait for authentication to complete
    /// </summary>
    protected async Task WaitForAuthenticationComplete()
    {
        await Page.WaitForURLAsync(url => 
            !url.Contains(KeycloakAuth.GetKeycloakUrl()) && 
            !url.Contains("/authentication/login"), 
            new PageWaitForURLOptions { Timeout = 15000 });
        await WaitForMudBlazorReady();
    }

    /// <summary>
    /// Login directly to Keycloak with username and password
    /// </summary>
    protected async Task LoginDirectToKeycloak(string username, string password)
    {
        // Go directly to Keycloak login URL with proper redirect
        var keycloakLoginUrl = "http://localhost:8180/realms/NationOH/protocol/openid-connect/auth?" +
                              "client_id=nationoh_client&" +
                              "redirect_uri=" + Uri.EscapeDataString($"{BaseUrl}/authentication/login-callback") + "&" +
                              "response_type=code&" +
                              "scope=openid%20nationoh_webapi-scope";
        
        await Page.GotoAsync(keycloakLoginUrl);
        await WaitForKeycloakLogin();

        // Fill credentials
        await Page.FillAsync("input[name='username'], input[id='username']", username);
        await Page.FillAsync("input[name='password'], input[id='password']", password);
        
        // Submit login
        await Page.ClickAsync("input[type='submit'], button[type='submit'], .btn-primary");
        
        // Wait for authentication to complete
        await WaitForAuthenticationComplete();
    }

    /// <summary>
    /// Alternative login method that works with the OIDC authentication flow
    /// </summary>
    protected async Task LoginWithOIDC(string username, string password)
    {
        try
        {
            // Start by navigating to the application root
            await Page.GotoAsync(BaseUrl);
            
            // If we're redirected to authentication, continue with login
            if (Page.Url.Contains("/authentication") || Page.Url.Contains("localhost:8180"))
            {
                // If already on Keycloak, proceed with login
                if (IsOnKeycloakLoginPage())
                {
                    await WaitForKeycloakLogin();
                    await FillKeycloakCredentials(username, password);
                    await SubmitKeycloakLogin();
                    await WaitForAuthenticationComplete();
                }
                else
                {
                    // Navigate to login and wait for Keycloak redirect
                    await Page.ClickAsync("a[href*='authentication/login'], .mud-button:has-text('Log in')");
                    await WaitForKeycloakLogin();
                    await FillKeycloakCredentials(username, password);
                    await SubmitKeycloakLogin();
                    await WaitForAuthenticationComplete();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to login with OIDC: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Fill Keycloak login credentials
    /// </summary>
    protected async Task FillKeycloakCredentials(string username, string password)
    {
        await Page.FillAsync("input[name='username'], input[id='username']", username);
        await Page.FillAsync("input[name='password'], input[id='password']", password);
        await Page.WaitForTimeoutAsync(500); // Small delay to ensure form is populated
    }

    /// <summary>
    /// Submit Keycloak login form
    /// </summary>
    protected async Task SubmitKeycloakLogin()
    {
        // Try different common submit button selectors
        var submitSelectors = new[]
        {
            "input[type='submit']",
            "button[type='submit']",
            ".btn-primary",
            "#kc-login",
            "button:has-text('Sign In')",
            "button:has-text('Login')"
        };

        foreach (var selector in submitSelectors)
        {
            try
            {
                var element = await Page.QuerySelectorAsync(selector);
                if (element != null)
                {
                    await element.ClickAsync();
                    return;
                }
            }
            catch (Exception)
            {
                // Continue to next selector
            }
        }

        throw new Exception("Could not find Keycloak login submit button");
    }

    /// <summary>
    /// Check if the user is currently authenticated
    /// </summary>
    protected async Task<bool> IsAuthenticated()
    {
        try
        {
            // Navigate to a protected page and see if we get redirected to login
            await Page.GotoAsync($"{BaseUrl}/");
            await Page.WaitForTimeoutAsync(2000);
            
            // If we're on the login page or Keycloak, we're not authenticated
            return !Page.Url.Contains("/authentication") && !Page.Url.Contains("localhost:8180");
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Ensure user is logged in before running tests
    /// </summary>
    protected async Task EnsureAuthenticated(string username, string password)
    {
        if (!await IsAuthenticated())
        {
            await LoginWithOIDC(username, password);
        }
    }

    /// <summary>
    /// Login using stored authentication state (for faster test execution)
    /// Note: This is a simplified version that doesn't try to replace the Page instance
    /// </summary>
    protected async Task LoginWithStoredState(string username, string password)
    {
        var authFile = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"auth-{username}.json");
        
        if (File.Exists(authFile))
        {
            try
            {
                // Try to restore storage state to current context
                await Page.Context.AddCookiesAsync(new List<Cookie>());
                // Note: Storage state restoration would need different approach in newer Playwright
                // For now, fall back to normal login
            }
            catch (Exception)
            {
                // If loading auth state fails, continue with normal login
            }
        }

        // Perform fresh login
        await LoginWithOIDC(username, password);
        
        // Save authentication state for future use
        try
        {
            await Page.Context.StorageStateAsync(new BrowserContextStorageStateOptions { Path = authFile });
        }
        catch (Exception)
        {
            // Non-critical error - continue without saving state
        }
    }

    /// <summary>
    /// Check if a specific page is authenticated
    /// </summary>
    private async Task<bool> IsPageAuthenticated(IPage page)
    {
        try
        {
            await page.WaitForTimeoutAsync(2000);
            return !page.Url.Contains("/authentication") && !page.Url.Contains("localhost:8180");
        }
        catch (Exception)
        {
            return false;
        }
    }
}
