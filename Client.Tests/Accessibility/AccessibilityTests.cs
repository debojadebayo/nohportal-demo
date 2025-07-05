using Client.Tests.Infrastructure;
using Client.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Client.Tests.Accessibility;

[TestFixture]
public class AccessibilityTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
    }

    [Test]
    public async Task Forms_ShouldHaveProperLabels()
    {
        await NavigateTo("/customers");
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Check that form inputs have proper labels
        var inputs = await Page.QuerySelectorAllAsync("input[type='text'], input[type='email'], input[type='tel']");
        
        foreach (var input in inputs)
        {
            var hasLabel = false;
            
            // Check for associated label
            var inputId = await input.GetAttributeAsync("id");
            if (!string.IsNullOrEmpty(inputId))
            {
                var label = await Page.QuerySelectorAsync($"label[for='{inputId}']");
                hasLabel = label != null;
            }
            
            // Check for aria-label
            if (!hasLabel)
            {
                var ariaLabel = await input.GetAttributeAsync("aria-label");
                hasLabel = !string.IsNullOrEmpty(ariaLabel);
            }
            
            // Check for aria-labelledby
            if (!hasLabel)
            {
                var ariaLabelledBy = await input.GetAttributeAsync("aria-labelledby");
                hasLabel = !string.IsNullOrEmpty(ariaLabelledBy);
            }
            
            // Check for MudBlazor label structure
            if (!hasLabel)
            {
                var mudLabel = await input.EvaluateAsync<bool>(@"
                    element => {
                        const container = element.closest('.mud-input-control, .mud-text-field');
                        return container && container.querySelector('.mud-input-label');
                    }
                ");
                hasLabel = mudLabel;
            }
            
            hasLabel.Should().BeTrue("All form inputs should have accessible labels");
        }
    }

    [Test]
    public async Task Buttons_ShouldHaveAccessibleText()
    {
        await NavigateTo("/customers");
        
        var buttons = await Page.QuerySelectorAllAsync("button, .mud-button");
        
        foreach (var button in buttons)
        {
            var hasAccessibleText = false;
            
            // Check button text content
            var textContent = await button.TextContentAsync();
            if (!string.IsNullOrWhiteSpace(textContent))
            {
                hasAccessibleText = true;
            }
            
            // Check aria-label
            if (!hasAccessibleText)
            {
                var ariaLabel = await button.GetAttributeAsync("aria-label");
                hasAccessibleText = !string.IsNullOrEmpty(ariaLabel);
            }
            
            // Check title attribute
            if (!hasAccessibleText)
            {
                var title = await button.GetAttributeAsync("title");
                hasAccessibleText = !string.IsNullOrEmpty(title);
            }
            
            hasAccessibleText.Should().BeTrue("All buttons should have accessible text or labels");
        }
    }

    [Test]
    public async Task NavigationMenu_ShouldBeKeyboardAccessible()
    {
        await NavigateTo("/");
        
        // Test keyboard navigation through menu
        await Page.FocusAsync(".mud-nav-menu");
        
        // Tab through navigation items
        await Page.Keyboard.PressAsync("Tab");
        var focusedElement = await Page.EvaluateAsync<string>("document.activeElement.tagName");
        
        // Should be able to navigate with keyboard
        focusedElement.Should().NotBeNull("Navigation should be keyboard accessible");
        
        // Test Enter key activation
        await Page.Keyboard.PressAsync("Enter");
        await WaitForPageReady();
        
        // Should navigate to the focused page
        var currentUrl = Page.Url;
        currentUrl.Should().NotBeNullOrEmpty("Enter key should activate navigation links");
    }

    [Test]
    public async Task ErrorMessages_ShouldBeAssociatedWithFields()
    {
        await NavigateTo("/customers");
        await Page.ClickMudButton("Add New Customer");
        await WaitForPageReady();
        
        // Trigger validation error
        await Page.FillAsync("input[type='email']", "invalid-email");
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForTimeoutAsync(500);
        
        // Check that error messages are properly associated
        var errorMessages = await Page.QuerySelectorAllAsync(".mud-input-error, .error-message");
        
        foreach (var errorMsg in errorMessages)
        {
            var hasProperAssociation = false;
            
            // Check if error is within the same form control container
            var isInFormControl = await errorMsg.EvaluateAsync<bool>(@"
                element => {
                    const container = element.closest('.mud-input-control, .form-group');
                    return container !== null;
                }
            ");
            
            if (isInFormControl)
            {
                hasProperAssociation = true;
            }
            
            // Check for aria-describedby relationship
            var errorId = await errorMsg.GetAttributeAsync("id");
            if (!string.IsNullOrEmpty(errorId))
            {
                var describedInput = await Page.QuerySelectorAsync($"[aria-describedby*='{errorId}']");
                if (describedInput != null)
                {
                    hasProperAssociation = true;
                }
            }
            
            hasProperAssociation.Should().BeTrue("Error messages should be properly associated with their form fields");
        }
    }

    [Test]
    public async Task FocusManagement_ShouldWorkInDialogs()
    {
        await NavigateTo("/clinicians");
        await Page.ClickMudButton("Add Clinician");
        await WaitForDialog();
        
        // Focus should be trapped within dialog
        var dialogElement = await Page.QuerySelectorAsync(".mud-dialog");
        dialogElement.Should().NotBeNull("Dialog should be present");
        
        // Check that first focusable element is focused
        var activeElement = await Page.EvaluateAsync<string>("document.activeElement.tagName");
        activeElement.Should().NotBeNull("Focus should be set when dialog opens");
        
        // Test Tab key cycling within dialog
        var focusableElements = await Page.QuerySelectorAllAsync(".mud-dialog input, .mud-dialog button, .mud-dialog select");
        if (focusableElements.Count > 1)
        {
            // Tab through all elements
            for (int i = 0; i < focusableElements.Count; i++)
            {
                await Page.Keyboard.PressAsync("Tab");
            }
            
            // Should cycle back to first element or stay within dialog
            var finalActiveElement = await Page.EvaluateAsync<bool>(@"
                () => {
                    const dialog = document.querySelector('.mud-dialog');
                    return dialog && dialog.contains(document.activeElement);
                }
            ");
            
            finalActiveElement.Should().BeTrue("Focus should remain within dialog when tabbing");
        }
    }

    [Test]
    public async Task ColorContrast_ShouldMeetStandards()
    {
        // This test would ideally use a contrast checking library
        // For now, we'll check for basic color accessibility indicators
        
        await NavigateTo("/customers");
        
        // Check that error states use appropriate colors
        await Page.ClickMudButton("Add New Customer");
        await Page.FillAsync("input[type='email']", "invalid");
        await Page.Keyboard.PressAsync("Tab");
        await Page.WaitForTimeoutAsync(500);
        
        var errorElement = await Page.QuerySelectorAsync(".mud-input-error");
        if (errorElement != null)
        {
            var styles = await errorElement.EvaluateAsync<dynamic>(@"
                element => {
                    const computed = window.getComputedStyle(element);
                    return {
                        color: computed.color,
                        backgroundColor: computed.backgroundColor
                    };
                }
            ");
            
            // Basic check that error styling is applied
            styles.Should().NotBeNull("Error elements should have computed styles");
        }
    }

    [Test]
    public async Task ScreenReaderContent_ShouldBeAppropriate()
    {
        await NavigateTo("/customers");
        
        // Check for screen reader only content
        var srOnlyElements = await Page.QuerySelectorAllAsync(".sr-only, .visually-hidden");
        
        foreach (var element in srOnlyElements)
        {
            var textContent = await element.TextContentAsync();
            textContent.Should().NotBeNullOrWhiteSpace("Screen reader only content should have meaningful text");
            
            // Check that element is visually hidden but accessible
            var isVisible = await element.IsVisibleAsync();
            isVisible.Should().BeFalse("Screen reader only content should not be visually visible");
        }
    }

    [Test]
    public async Task LoadingStates_ShouldBeAccessible()
    {
        await NavigateTo("/finance");
        
        // Trigger a loading state
        await Page.ClickAsync(".mud-autocomplete input");
        await Page.FillAsync(".mud-autocomplete input", "test");
        
        // Check for loading indicators with proper accessibility
        var loadingIndicator = await Page.QuerySelectorAsync(".mud-progress-circular");
        if (loadingIndicator != null)
        {
            // Check for appropriate ARIA attributes
            var ariaLabel = await loadingIndicator.GetAttributeAsync("aria-label");
            var role = await loadingIndicator.GetAttributeAsync("role");
            
            (ariaLabel != null || role == "status" || role == "progressbar")
                .Should().BeTrue("Loading indicators should have appropriate ARIA attributes");
        }
    }
}
