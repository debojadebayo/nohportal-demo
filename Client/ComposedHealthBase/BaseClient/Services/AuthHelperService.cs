using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using MudBlazor;
using Shared.DTOs.CRM;
using System.Net.Http.Json;
using ComposedHealthBase.Shared.Interfaces;

namespace ComposedHealthBase.BaseClient.Services
{
    public interface IAuthHelperService
    {
        Task<string?> GetFullNameAsync();
        Task<string?> GetEmailAsync();
        Task<string?> GetUserNameAsync();
        Task<ClaimsPrincipal?> GetUserAsync();
        Task<TTenantDto?> CreateTenant<TTenantDto>(TTenantDto item, CancellationToken token) where TTenantDto : class, ITenant;
        Task<TSubjectDto?> CreateSubject<TSubjectDto>(TSubjectDto item, CancellationToken token) where TSubjectDto : class, ISubject;
        Task<bool> IsAuthorizedForRoleManagement();
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

        public async Task<TTenantDto?> CreateTenant<TTenantDto>(TTenantDto item, CancellationToken token)
            where TTenantDto : class, ITenant
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/security/{typeof(TTenantDto).Name.ToLowerInvariant().Replace("dto", "s")}/createtenant", item, token);
                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Successfully added new tenant", Severity.Success);
                    return await response.Content.ReadFromJsonAsync<TTenantDto>(cancellationToken: token);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to add item: {ex.Message}", Severity.Error);
            }
            return null;
        }
        public async Task<TSubjectDto?> CreateSubject<TSubjectDto>(TSubjectDto item, CancellationToken token)
            where TSubjectDto : class, ISubject
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/security/{typeof(TSubjectDto).Name.ToLowerInvariant().Replace("dto", "s")}/createsubject", item, token);
                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Successfully added new subject", Severity.Success);
                    return await response.Content.ReadFromJsonAsync<TSubjectDto>(cancellationToken: token);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Failed to add item: {ex.Message}", Severity.Error);
            }
            return null;
        }

        public async Task<bool> IsAuthorizedForRoleManagement()
        {
            var user = await GetUserAsync();
            if (user == null) return false;
            
            return user.IsInRole("administrator") || user.IsInRole("tenantadministrator");
        }
    }
}