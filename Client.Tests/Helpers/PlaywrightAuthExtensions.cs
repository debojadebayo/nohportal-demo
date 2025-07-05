using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Client.Tests.Helpers;

/// <summary>
/// Playwright extension methods for handling authentication and MudBlazor components
/// </summary>
public static class PlaywrightAuthExtensions
{
    /// <summary>
    /// Wait for authentication state to stabilize
    /// </summary>
    public static async Task WaitForAuthenticationAsync(this IPage page, int timeoutMs = 10000)
    {
        // Wait for either authenticated content or login redirect
        await page.WaitForFunctionAsync(@"
            () => {
                // Check if we're on login/auth page
                if (window.location.href.includes('/authentication') || 
                    window.location.href.includes('localhost:8180')) {
                    return true;
                }
                
                // Check if main app content is loaded (look for common selectors)
                const hasNavMenu = document.querySelector('.mud-nav-menu, .mud-drawer');
                const hasAppBar = document.querySelector('.mud-appbar');
                const hasMainContent = document.querySelector('.mud-main-content, .mud-container');
                
                return hasNavMenu || hasAppBar || hasMainContent;
            }
        ", new PageWaitForFunctionOptions { Timeout = timeoutMs });
    }

    /// <summary>
    /// Wait for MudBlazor application to be ready after authentication
    /// </summary>
    public static async Task WaitForMudBlazorAfterAuthAsync(this IPage page, int timeoutMs = 15000)
    {
        // First wait for authentication to complete
        await page.WaitForAuthenticationAsync(timeoutMs / 2);
        
        // If not on auth page, wait for MudBlazor to load
        if (!page.Url.Contains("/authentication") && !page.Url.Contains("localhost:8180"))
        {
            await page.WaitForSelectorAsync(".mud-typography, .mud-button, .mud-paper", new PageWaitForSelectorOptions { Timeout = timeoutMs / 2 });
            await page.WaitForTimeoutAsync(1000); // Additional buffer for components to render
        }
    }

    /// <summary>
    /// Check if page appears to be authenticated (not on login page)
    /// </summary>
    public static bool IsAuthenticated(this IPage page)
    {
        return !page.Url.Contains("/authentication") && 
               !page.Url.Contains("localhost:8180") &&
               !page.Url.Contains("/login");
    }

    /// <summary>
    /// Safe click that waits for element to be ready
    /// </summary>
    public static async Task SafeClickAsync(this IPage page, string selector, int timeoutMs = 5000)
    {
        await page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions { Timeout = timeoutMs });
        await page.ClickAsync(selector);
        await page.WaitForTimeoutAsync(300); // Small delay for any animations
    }

    /// <summary>
    /// Fill MudBlazor text field with proper waiting
    /// </summary>
    public static async Task FillMudTextFieldSafe(this IPage page, string label, string value, int timeoutMs = 5000)
    {
        var selector = $".mud-text-field:has(.mud-input-label:has-text('{label}')) input, .mud-text-field:has(label:has-text('{label}')) input";
        await page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions { Timeout = timeoutMs });
        await page.FillAsync(selector, value);
        await page.WaitForTimeoutAsync(200);
    }

    /// <summary>
    /// Click MudBlazor button with proper waiting
    /// </summary>
    public static async Task ClickMudButtonSafe(this IPage page, string buttonText, int timeoutMs = 5000)
    {
        var selector = $".mud-button:has-text('{buttonText}')";
        await page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions { Timeout = timeoutMs });
        await page.ClickAsync(selector);
        await page.WaitForTimeoutAsync(500); // Wait for any resulting actions
    }
}
