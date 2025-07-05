# Client.Tests - Playwright.NET Testing Suite

This project contains comprehensive end-to-end tests for the NationOH Client application using Playwright.NET and NUnit, with a focus on forms and validation testing.

## Overview

The test suite covers:
- **Form Validation**: Comprehensive testing of all form inputs and validation rules
- **UI Components**: MudBlazor component behavior and interaction
- **Navigation**: Page routing and navigation flows  
- **Authentication**: Login/logout and protected route behavior
- **Accessibility**: WCAG compliance and keyboard navigation
- **Integration**: End-to-end workflows across multiple pages

## Project Structure

```
Client.Tests/
├── appsettings.json                 # Test configuration
├── playwright.runsettings           # Playwright test settings
├── run-tests.sh                     # Test runner script
├── Client.Tests.csproj             # Project file
├── Forms/                          # Form validation tests
│   ├── CustomerFormTests.cs
│   ├── EmployeeFormTests.cs
│   ├── ClinicianFormTests.cs
│   ├── FinanceFormTests.cs
│   ├── CaseNotesFormTests.cs
│   ├── ScheduleFormTests.cs
│   └── ValidationIntegrationTests.cs
├── Components/                     # UI component tests
│   └── MudBlazorComponentTests.cs
├── Navigation/                     # Navigation and auth tests
│   └── NavigationTests.cs
├── Accessibility/                  # Accessibility tests
│   └── AccessibilityTests.cs
├── Helpers/                        # Test utilities
│   └── FormHelpers.cs
├── Infrastructure/                 # Base test classes
│   └── PlaywrightTestBase.cs
└── TestData/                      # Test data generation
    └── TestDataGenerator.cs
```

## Key Features

### Form Validation Testing
- **Comprehensive Coverage**: Tests all form fields with valid and invalid data
- **Real Validation Rules**: Uses actual FluentValidation rules from the Shared project
- **Error Message Verification**: Ensures proper error messages are displayed
- **Field-Level Validation**: Tests individual field validation behavior
- **Cross-Form Consistency**: Ensures validation behavior is consistent across forms

### Test Data Generation
- **Realistic Data**: Uses Bogus library to generate realistic test data
- **Valid/Invalid Scenarios**: Provides both valid and invalid data for testing
- **UK-Specific Validation**: Includes UK phone numbers, postcodes, etc.
- **Edge Cases**: Tests boundary conditions and edge cases

### Helper Methods
- **MudBlazor Integration**: Helper methods for interacting with MudBlazor components
- **Form Utilities**: Simplified form filling and validation checking
- **Wait Strategies**: Proper waiting for components to load and render
- **Error Handling**: Robust error detection and reporting

## Configuration

### appsettings.json
```json
{
  "TestSettings": {
    "BaseUrl": "https://localhost:7001",
    "TimeoutMs": 30000,
    "Headless": true,
    "BrowserType": "chromium"
  },
  "Authentication": {
    "TestUserEmail": "test@nationoh.com",
    "TestUserPassword": "TestPassword123!",
    "AdminUserEmail": "admin@nationoh.com", 
    "AdminUserPassword": "AdminPassword123!"
  }
}
```

### Environment Variables
- `PLAYWRIGHT_BROWSERS_PATH`: Browser installation path
- `HEADED`: Set to true for headed mode
- `BROWSER`: Browser type (chromium, firefox, webkit)

## Running Tests

### Prerequisites
1. Ensure the NationOH application is running locally
2. Install Playwright browsers: `pwsh bin/Debug/net9.0/playwright.ps1 install`

### Using the Test Runner Script
```bash
# Run all tests
./run-tests.sh

# Run tests in headed mode (visible browser)
./run-tests.sh --headed

# Run only form validation tests
./run-tests.sh --forms-only

# Run specific test pattern
./run-tests.sh --pattern "Customer*"

# Run with Firefox browser
./run-tests.sh --browser firefox

# Generate traces and videos for debugging
./run-tests.sh --generate-trace --record-video
```

### Using dotnet test directly
```bash
# Build and run all tests
dotnet test

# Run specific test class
dotnet test --filter "ClassName=CustomerFormTests"

# Run with specific configuration
dotnet test --configuration Debug --verbosity normal
```

## Test Categories

### 1. Form Validation Tests

#### CustomerFormTests
- Tests customer creation and editing forms
- Validates all required fields and business rules
- Tests UK-specific validation (phone, postcode, etc.)
- Verifies industry dropdown constraints
- Tests website URL validation

#### EmployeeFormTests  
- Tests employee form validation
- Age validation (minimum 18 years)
- UK address and phone number validation
- Line manager self-reference prevention
- Company association validation

#### ClinicianFormTests
- Tests clinician registration forms
- Professional license validation
- Regulator type selection
- Contact information validation
- Enum-based field validation

#### FinanceFormTests
- Invoice generation form testing
- Date range validation
- Customer selection requirements
- Financial calculation validation
- Multi-step form workflows

#### CaseNotesFormTests
- Clinical case notes validation
- Rich text editor functionality
- Follow-up requirements validation
- Character length limits
- Encryption indicator verification

#### ScheduleFormTests
- Appointment scheduling validation
- Time slot conflict detection
- Date and time validation
- Resource availability checking
- Referral integration

### 2. Component Tests

#### MudBlazorComponentTests
- Data grid functionality
- Dialog open/close behavior
- Tab switching
- Date picker interaction
- Select component behavior
- Lazy lookup components
- Progress indicators

### 3. Navigation Tests
- Page routing and loading
- Menu navigation
- Authentication flows
- Protected route handling
- Breadcrumb navigation

### 4. Accessibility Tests
- Form label associations
- Keyboard navigation
- Focus management
- Screen reader compatibility
- Color contrast validation
- ARIA attribute verification

## Best Practices

### Writing Tests
1. **Use Page Object Pattern**: Encapsulate page interactions in helper methods
2. **Wait Strategies**: Use proper waits for elements to load
3. **Descriptive Names**: Use clear, descriptive test names
4. **Independent Tests**: Each test should be independent and idempotent
5. **Cleanup**: Properly clean up test data and state

### Debugging Tests
1. **Screenshots**: Automatically captured on test failures
2. **Videos**: Optional recording of test execution
3. **Traces**: Playwright traces for detailed debugging
4. **Console Logs**: Browser console logs captured
5. **Network Logs**: HTTP request/response logging

### Performance
1. **Parallel Execution**: Tests can run in parallel
2. **Browser Reuse**: Browsers are reused across tests
3. **Selective Running**: Run only relevant test subsets
4. **Fast Feedback**: Quick validation test runs

## Continuous Integration

### GitHub Actions Example
```yaml
- name: Run Playwright Tests
  run: |
    cd Client.Tests
    dotnet test --configuration Release --logger trx
```

### Test Results
- **TRX Format**: Compatible with Azure DevOps and GitHub Actions
- **Screenshots**: Failure screenshots for debugging
- **Coverage Reports**: Test coverage metrics
- **Performance Metrics**: Test execution time tracking

## Troubleshooting

### Common Issues

1. **Browser Installation**
   ```bash
   pwsh bin/Debug/net9.0/playwright.ps1 install
   ```

2. **Timeout Issues**
   - Increase timeout in appsettings.json
   - Check application startup time
   - Verify network connectivity

3. **Element Not Found**
   - Check MudBlazor component rendering
   - Verify CSS selectors
   - Add proper wait conditions

4. **Authentication Failures**
   - Verify test user credentials
   - Check authentication endpoint
   - Validate token expiration

### Debug Mode
Run tests in headed mode to see browser interactions:
```bash
./run-tests.sh --headed --no-screenshots
```

## Contributing

When adding new tests:
1. Follow existing naming conventions
2. Add appropriate test data to TestDataGenerator
3. Use helper methods from FormHelpers
4. Include both positive and negative test cases
5. Add accessibility considerations
6. Update documentation as needed

## Dependencies

- **Microsoft.Playwright.NUnit**: Playwright integration for NUnit
- **FluentAssertions**: Fluent assertion library
- **Bogus**: Test data generation
- **NUnit**: Test framework
- **Microsoft.Extensions.Configuration**: Configuration management

## Related Documentation

- [Playwright Documentation](https://playwright.dev/dotnet/)
- [MudBlazor Documentation](https://mudblazor.com/)
- [NUnit Documentation](https://docs.nunit.org/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
