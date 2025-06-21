using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

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

        public AuthHelperService(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
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
    }
}