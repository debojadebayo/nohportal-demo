using Microsoft.Playwright;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Client.Tests.Infrastructure;

public class KeycloakAuthHelper
{
    private readonly IConfiguration _configuration;
    private readonly string _keycloakUrl;
    private readonly string _realm;
    private readonly string _clientId;

    public KeycloakAuthHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _keycloakUrl = configuration["Keycloak:Url"] ?? "http://localhost:8180";
        _realm = configuration["Keycloak:Realm"] ?? "NationOH";
        _clientId = configuration["Keycloak:ClientId"] ?? "nationoh_client";
    }

    /// <summary>
    /// Build the Keycloak authorization URL for OIDC flow
    /// </summary>
    public string BuildAuthUrl(string baseUrl, string? state = null)
    {
        var redirectUri = Uri.EscapeDataString($"{baseUrl}/authentication/login-callback");
        var stateParam = !string.IsNullOrEmpty(state) ? $"&state={Uri.EscapeDataString(state)}" : "";
        
        return $"{_keycloakUrl}/realms/{_realm}/protocol/openid-connect/auth?" +
               $"client_id={_clientId}&" +
               $"redirect_uri={redirectUri}&" +
               $"response_type=code&" +
               $"scope=openid%20nationoh_webapi-scope" +
               stateParam;
    }

    /// <summary>
    /// Authenticate with Keycloak using direct URL navigation (supports organization-based two-step authentication)
    /// </summary>
    public async Task AuthenticateAsync(IPage page, string username, string password, string baseUrl)
    {
        try
        {
            // Navigate directly to Keycloak auth URL
            var authUrl = BuildAuthUrl(baseUrl);
            await page.GotoAsync(authUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

            // Wait for Keycloak login form
            await page.WaitForSelectorAsync("input[name='username'], input[id='username'], #username", new PageWaitForSelectorOptions { Timeout = 10000 });

            // Check for organization-based authentication (two-step process)
            var usernameVisible = await page.IsVisibleAsync("input[name='username'], input[id='username'], #username");
            var passwordVisible = await page.IsVisibleAsync("input[name='password'], input[id='password'], #password");

            if (usernameVisible && !passwordVisible)
            {
                // Organization-based authentication: Step 1 - Username only
                await page.FillAsync("input[name='username'], input[id='username'], #username", username);
                await page.ClickAsync("button[type='submit']");
                
                // Wait for navigation to password page
                await page.WaitForLoadStateAsync(Microsoft.Playwright.LoadState.NetworkIdle);
                await page.WaitForTimeoutAsync(3000);
                
                // Step 2 - Password input should now be visible
                await page.WaitForSelectorAsync("input[name='password'], input[id='password'], #password", new PageWaitForSelectorOptions { Timeout = 15000 });
                await page.FillAsync("input[name='password'], input[id='password'], #password", password);
                await page.ClickAsync("button[type='submit']");
            }
            else if (usernameVisible && passwordVisible)
            {
                // Traditional single-step authentication
                await page.FillAsync("input[name='username'], input[id='username'], #username", username);
                await page.FillAsync("input[name='password'], input[id='password'], #password", password);
                await page.ClickAsync("button[type='submit']");
            }
            else
            {
                throw new Exception($"Could not find expected login inputs. Username visible: {usernameVisible}, Password visible: {passwordVisible}");
            }

            // Wait for redirect back to application
            Console.WriteLine($"Waiting for redirect away from Keycloak...");
            
            // First wait to get away from login-callback page
            try
            {
                await page.WaitForURLAsync(url => 
                    !url.Contains(_keycloakUrl) && 
                    !url.Contains("/authentication/login-callback"), 
                    new PageWaitForURLOptions { Timeout = 30000 });
                Console.WriteLine($"Successfully redirected to: {page.Url}");
            }
            catch (TimeoutException)
            {
                // If we're still on callback page, wait a bit more and check if app is ready
                Console.WriteLine($"Still on callback page, checking if app is functional at: {page.Url}");
                await page.WaitForTimeoutAsync(3000);
                
                // Check if we can find any main app elements (like the nav menu)
                var appReady = await page.Locator("nav, .mud-nav-link, .mud-appbar").First.IsVisibleAsync();
                if (!appReady)
                {
                    throw new Exception($"Authentication callback did not complete properly. Still at: {page.Url}");
                }
                Console.WriteLine("App appears to be ready despite being on callback URL");
            }
            
            // Additional wait for app to initialize
            await page.WaitForTimeoutAsync(2000);
            
            // If we're still on callback URL, navigate to the base URL
            if (page.Url.Contains("/authentication/login-callback"))
            {
                Console.WriteLine("Still on callback URL, navigating to base URL...");
                await page.GotoAsync(baseUrl);
                await page.WaitForTimeoutAsync(2000);
                Console.WriteLine($"Navigated to base URL: {page.Url}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Authentication error at URL: {page.Url}");
            throw new Exception($"Keycloak authentication failed: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Check if currently on Keycloak login page
    /// </summary>
    public bool IsOnKeycloakLoginPage(IPage page)
    {
        return page.Url.Contains(_keycloakUrl) && 
               (page.Url.Contains("/auth/") || page.Url.Contains("/realms/"));
    }

    /// <summary>
    /// Wait for Keycloak login page to be ready
    /// </summary>
    public async Task WaitForKeycloakReady(IPage page)
    {
        await page.WaitForSelectorAsync("#username, input[name='username']", new PageWaitForSelectorOptions { Timeout = 10000 });
        await page.WaitForSelectorAsync("#password, input[name='password']", new PageWaitForSelectorOptions { Timeout = 5000 });
        await page.WaitForTimeoutAsync(500); // Small delay for form to be fully interactive
    }

    /// <summary>
    /// Logout from Keycloak
    /// </summary>
    public async Task LogoutAsync(IPage page, string baseUrl)
    {
        try
        {
            var logoutUrl = $"{_keycloakUrl}/realms/{_realm}/protocol/openid-connect/logout?" +
                           $"redirect_uri={Uri.EscapeDataString(baseUrl)}";
            
            await page.GotoAsync(logoutUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
            await page.WaitForTimeoutAsync(1000);
        }
        catch (Exception ex)
        {
            throw new Exception($"Keycloak logout failed: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Get the admin credentials from configuration
    /// </summary>
    public (string username, string password) GetAdminCredentials()
    {
        var username = _configuration["Keycloak:AdminUsername"] ?? 
                      _configuration["Authentication:AdminUserEmail"] ?? 
                      "admin";
        var password = _configuration["Keycloak:AdminPassword"] ?? 
                      _configuration["Authentication:AdminUserPassword"] ?? 
                      "123";
        
        return (username, password);
    }

    /// <summary>
    /// Get test user credentials from configuration
    /// </summary>
    public (string username, string password) GetTestUserCredentials()
    {
        var username = _configuration["Authentication:TestUserEmail"] ?? "admin";
        var password = _configuration["Authentication:TestUserPassword"] ?? "123";
        
        return (username, password);
    }

    /// <summary>
    /// Get the Keycloak base URL
    /// </summary>
    public string GetKeycloakUrl()
    {
        return _keycloakUrl;
    }
}
