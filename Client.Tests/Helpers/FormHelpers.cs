using Microsoft.Playwright;

namespace Client.Tests.Helpers;

public static class FormHelpers
{
    /// <summary>
    /// Fill a MudBlazor text field
    /// </summary>
    public static async Task FillMudTextField(this IPage page, string label, string value)
    {
        var selector = $".mud-input-control:has(.mud-input-label:has-text('{label}')) input, " +
                      $".mud-text-field:has(.mud-input-label:has-text('{label}')) input";
        await page.FillAsync(selector, value);
    }

    /// <summary>
    /// Fill a MudBlazor text field by data-testid
    /// </summary>
    public static async Task FillMudTextFieldByTestId(this IPage page, string testId, string value)
    {
        await page.FillAsync($"[data-testid='{testId}'] input", value);
    }

    /// <summary>
    /// Select option from MudBlazor select component
    /// </summary>
    public static async Task SelectMudSelectOption(this IPage page, string label, string optionText)
    {
        // Click the select to open dropdown
        var selectSelector = $".mud-select:has(.mud-input-label:has-text('{label}'))";
        await page.ClickAsync(selectSelector);
        
        // Wait for dropdown to appear
        await page.WaitForSelectorAsync(".mud-popover .mud-list");
        
        // Click the desired option
        await page.ClickAsync($".mud-popover .mud-list-item:has-text('{optionText}')");
    }

    /// <summary>
    /// Select option from MudBlazor select by test id
    /// </summary>
    public static async Task SelectMudSelectOptionByTestId(this IPage page, string testId, string optionText)
    {
        await page.ClickAsync($"[data-testid='{testId}'] .mud-select");
        await page.WaitForSelectorAsync(".mud-popover .mud-list");
        await page.ClickAsync($".mud-popover .mud-list-item:has-text('{optionText}')");
    }

    /// <summary>
    /// Set MudBlazor date picker value
    /// </summary>
    public static async Task SetMudDatePicker(this IPage page, string label, DateTime date)
    {
        var selector = $".mud-picker:has(.mud-input-label:has-text('{label}')) input";
        var dateString = date.ToString("dd/MM/yyyy");
        await page.FillAsync(selector, dateString);
        await page.Keyboard.PressAsync("Tab"); // Trigger blur to validate
    }

    /// <summary>
    /// Click MudBlazor button by text
    /// </summary>
    public static async Task ClickMudButton(this IPage page, string buttonText)
    {
        await page.ClickAsync($".mud-button:has-text('{buttonText}')");
    }

    /// <summary>
    /// Click MudBlazor button by test id
    /// </summary>
    public static async Task ClickMudButtonByTestId(this IPage page, string testId)
    {
        await page.ClickAsync($"[data-testid='{testId}'].mud-button");
    }

    /// <summary>
    /// Wait for MudBlazor form validation to complete
    /// </summary>
    public static async Task WaitForFormValidation(this IPage page)
    {
        await page.WaitForTimeoutAsync(300); // Allow validation to process
    }

    /// <summary>
    /// Get all validation error messages from MudBlazor form
    /// </summary>
    public static async Task<string[]> GetValidationErrors(this IPage page)
    {
        await page.WaitForTimeoutAsync(500); // Wait for validation to process
        
        var errorElements = await page.QuerySelectorAllAsync(".mud-input-helper-text.mud-input-error");
        var errors = new List<string>();
        
        foreach (var element in errorElements)
        {
            var text = await element.TextContentAsync();
            if (!string.IsNullOrWhiteSpace(text))
            {
                errors.Add(text.Trim());
            }
        }
        
        return errors.ToArray();
    }

    /// <summary>
    /// Check if MudBlazor form has validation errors
    /// </summary>
    public static async Task<bool> HasValidationErrors(this IPage page)
    {
        var errors = await GetValidationErrors(page);
        return errors.Length > 0;
    }

    /// <summary>
    /// Get validation error for specific field
    /// </summary>
    public static async Task<string?> GetFieldValidationError(this IPage page, string fieldLabel)
    {
        var selector = $".mud-input-control:has(.mud-input-label:has-text('{fieldLabel}')) .mud-input-helper-text.mud-input-error";
        try
        {
            var element = await page.WaitForSelectorAsync(selector, new() { Timeout = 2000 });
            return await element.TextContentAsync();
        }
        catch (TimeoutException)
        {
            return null;
        }
    }

    /// <summary>
    /// Check if a specific field has validation error
    /// </summary>
    public static async Task<bool> HasFieldValidationError(this IPage page, string fieldLabel)
    {
        var error = await GetFieldValidationError(page, fieldLabel);
        return !string.IsNullOrWhiteSpace(error);
    }

    /// <summary>
    /// Wait for and get snackbar message
    /// </summary>
    public static async Task<string> GetSnackbarMessage(this IPage page)
    {
        var snackbar = await page.WaitForSelectorAsync(".mud-snackbar", new() { Timeout = 5000 });
        return await snackbar.TextContentAsync() ?? "";
    }

    /// <summary>
    /// Check if form submit button is disabled
    /// </summary>
    public static async Task<bool> IsSubmitButtonDisabled(this IPage page, string buttonText = "Save")
    {
        var button = await page.QuerySelectorAsync($".mud-button:has-text('{buttonText}')");
        if (button == null) return true;
        
        var disabled = await button.GetAttributeAsync("disabled");
        var classList = await button.GetAttributeAsync("class");
        
        return disabled != null || (classList?.Contains("mud-button-disable") ?? false);
    }

    /// <summary>
    /// Clear all form fields in a MudBlazor form
    /// </summary>
    public static async Task ClearForm(this IPage page)
    {
        var inputs = await page.QuerySelectorAllAsync(".mud-form input, .mud-form textarea");
        foreach (var input in inputs)
        {
            await input.FillAsync("");
        }
    }

    /// <summary>
    /// Fill a complex form with validation testing
    /// </summary>
    public static async Task FillFormAndValidate(this IPage page, Dictionary<string, string> fieldValues, bool expectErrors = false)
    {
        // Fill all fields
        foreach (var (label, value) in fieldValues)
        {
            await page.FillMudTextField(label, value);
            await page.WaitForFormValidation();
        }

        // Trigger final validation
        await page.ClickAsync("body"); // Click outside to trigger blur on last field
        await page.WaitForFormValidation();

        // Check validation state
        var hasErrors = await page.HasValidationErrors();
        if (expectErrors && !hasErrors)
        {
            throw new Exception("Expected validation errors but none were found");
        }
        else if (!expectErrors && hasErrors)
        {
            var errors = await page.GetValidationErrors();
            throw new Exception($"Unexpected validation errors: {string.Join(", ", errors)}");
        }
    }

    /// <summary>
    /// Submit form and wait for response
    /// </summary>
    public static async Task SubmitFormAndWait(this IPage page, string submitButtonText = "Save", int timeoutMs = 10000)
    {
        // Wait for any pending validation
        await page.WaitForFormValidation();
        
        // Click submit button
        await page.ClickMudButton(submitButtonText);
        
        // Wait for either success message or error
        try
        {
            await page.WaitForSelectorAsync(".mud-snackbar", new() { Timeout = timeoutMs });
        }
        catch (TimeoutException)
        {
            // No snackbar appeared, form might have client-side validation errors
            var hasErrors = await page.HasValidationErrors();
            if (hasErrors)
            {
                var errors = await page.GetValidationErrors();
                throw new Exception($"Form submission blocked by validation errors: {string.Join(", ", errors)}");
            }
            throw;
        }
    }

    /// <summary>
    /// Test field validation by entering invalid value and checking error
    /// </summary>
    public static async Task TestFieldValidation(this IPage page, string fieldLabel, string invalidValue, string expectedErrorMessage)
    {
        // Fill field with invalid value
        await page.FillMudTextField(fieldLabel, invalidValue);
        
        // Trigger validation by moving focus away
        await page.Keyboard.PressAsync("Tab");
        await page.WaitForFormValidation();
        
        // Check for expected error
        var actualError = await page.GetFieldValidationError(fieldLabel);
        if (string.IsNullOrWhiteSpace(actualError))
        {
            throw new Exception($"Expected validation error '{expectedErrorMessage}' for field '{fieldLabel}' with invalid value '{invalidValue}', but no error was found");
        }
        
        if (!actualError.Contains(expectedErrorMessage))
        {
            throw new Exception($"Expected validation error containing '{expectedErrorMessage}' for field '{fieldLabel}', but got '{actualError}'");
        }
    }
}
