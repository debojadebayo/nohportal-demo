using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using MudBlazor;
using Shared.DTOs.CRM;
using System.Net.Http.Json;

namespace ComposedHealthBase.BaseClient.Services
{
    public interface IAuthHelperService
    {
        Task<string?> GetFullNameAsync();
        Task<string?> GetEmailAsync();
        Task<string?> GetUserNameAsync();
        Task<ClaimsPrincipal?> GetUserAsync();
    }

    public class AuthHelperService : IAuthHelperService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public AuthHelperService(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient, ISnackbar snackbar)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
            _snackbar = snackbar;
        }
        public async Task<ClaimsPrincipal?> GetUserAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated == true ? authState.User : null;
        }

        public async Task<string?> GetFullNameAsync()
        {
            var user = await GetUserAsync();
            return user?.FindFirst("name")?.Value;
        }

        public async Task<string?> GetEmailAsync()
        {
            var user = await GetUserAsync();
            return user?.FindFirst("email")?.Value;
        }

        public async Task<string?> GetUserNameAsync()
        {
            var user = await GetUserAsync();
            return user?.FindFirst("preferred_username")?.Value;
        }

        public async Task CreateTenant(CustomerDto item, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/security/createtenant", item, token);
                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Successfully added new customer", Severity.Success);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to add item: {ex.Message}", Severity.Error);
            }
        }
        public async Task CreateSubject(EmployeeDto item, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/security/createtenant", item, token);
                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Successfully added new employee", Severity.Success);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to add item: {ex.Message}", Severity.Error);
            }
        }
    }
}