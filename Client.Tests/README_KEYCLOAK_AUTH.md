# Keycloak Authentication for Playwright Tests

This guide explains how to use Keycloak authentication in your Playwright tests for the NationOH application.

## Overview

The application uses Keycloak for OIDC authentication with the following setup:
- **Keycloak URL**: `http://localhost:8180`
- **Realm**: `NationOH`
- **Client ID**: `nationoh_client`
- **Default Admin User**: `admin` / `123`

## Test Configuration

Authentication settings are configured in `appsettings.json`:

```json
{
  "Authentication": {
    "TestUserEmail": "admin",
    "TestUserPassword": "123",
    "AdminUserEmail": "admin",
    "AdminUserPassword": "123"
  },
  "Keycloak": {
    "Url": "http://localhost:8180",
    "Realm": "NationOH",
    "ClientId": "nationoh_client",
    "AdminUsername": "admin",
    "AdminPassword": "123"
  }
}
```

## Usage in Tests

### Basic Authentication

```csharp
[Test]
public async Task MyTest_ShouldWork()
{
    // Login as admin user
    await LoginAsAdmin();
    
    // Navigate to protected page
    await NavigateTo("/customers");
    
    // Your test logic here...
}
```

### Different User Types

```csharp
// Login as admin
await LoginAsAdmin();

// Login as test user
await LoginAsTestUser();

// Login with specific credentials
await Login("username", "password");
```

### Authentication Helpers

The `KeycloakAuthHelper` class provides:

- `AuthenticateAsync()` - Direct authentication with Keycloak
- `IsOnKeycloakLoginPage()` - Check if on Keycloak login page
- `WaitForKeycloakReady()` - Wait for login form to be ready
- `LogoutAsync()` - Logout from Keycloak
- `GetAdminCredentials()` - Get admin credentials from config
- `GetTestUserCredentials()` - Get test user credentials from config

### Test Base Class Features

The `PlaywrightTestBase` class provides:

- **`LoginAsAdmin()`** - Login with admin credentials
- **`LoginAsTestUser()`** - Login with test user credentials  
- **`Login(username, password)`** - Login with specific credentials
- **`Logout()`** - Logout current user
- **`IsAuthenticated()`** - Check if currently authenticated
- **`WaitForMudBlazorReady()`** - Wait for MudBlazor components to load

## Advanced Usage

### Authentication State Persistence

For faster test execution, authentication state can be persisted:

```csharp
await LoginWithStoredState("admin", "123");
```

This saves the authentication state to a file and reuses it in subsequent test runs.

### Handling Authentication Redirects

The authentication flow handles:
1. Redirect to Keycloak login page
2. Credential entry and submission
3. Redirect back to application
4. MudBlazor component initialization

### Error Handling

The authentication helpers include robust error handling for:
- Network timeouts
- Invalid credentials
- Keycloak service unavailability
- Authentication state corruption

## Best Practices

1. **Always authenticate in `[SetUp]`** - Ensure tests start with known auth state
2. **Use `LoginAsAdmin()` for most tests** - Admin user has access to all features
3. **Test different user roles** - Use specific credentials for role-based tests
4. **Handle logout properly** - Use `Logout()` in cleanup when needed
5. **Wait for components** - Always call `WaitForMudBlazorReady()` after navigation

## Troubleshooting

### Common Issues

1. **Keycloak not running**: Ensure Keycloak is started on `localhost:8180`
2. **Wrong credentials**: Check `appsettings.json` configuration
3. **Timeout errors**: Increase timeout values in test configuration
4. **Navigation fails**: Ensure proper waiting for page loads

### Debug Tips

- Set `"Headless": false` in `appsettings.json` to see browser interaction
- Add `await Page.WaitForTimeoutAsync(5000)` to pause and inspect state
- Check browser developer tools for console errors
- Verify Keycloak realm configuration

### Test Environment Setup

Ensure the following services are running:
1. **Keycloak** on `http://localhost:8180`
2. **Application API** on `http://localhost:5003`
3. **Client Application** (if testing full integration)

## Example Test

```csharp
[TestFixture]
public class MyFeatureTests : PlaywrightTestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await LoginAsAdmin();
        await NavigateTo("/my-feature");
        await WaitForMudBlazorReady();
    }

    [Test]
    public async Task MyFeature_ShouldWork()
    {
        // Test is ready to interact with authenticated application
        await Page.ClickAsync(".mud-button:has-text('Test Button')");
        
        // Assert results...
    }

    [TearDown]
    public async Task TestTearDown()
    {
        // Logout is optional - auth state is reset between tests
        // await Logout();
    }
}
```
