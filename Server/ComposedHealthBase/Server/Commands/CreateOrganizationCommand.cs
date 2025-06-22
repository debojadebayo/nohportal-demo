using Microsoft.AspNetCore.Authorization;

using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;
namespace ComposedHealthBase.Server.Commands
{
    public interface ICreateOrganizationCommand
    {
        Task<Guid> Handle(string orgId, string orgName, ClaimsPrincipal user);
    }

    public class CreateOrganizationCommand : ICreateOrganizationCommand
    {
        private readonly IAuthorizationService _authorizationService;

        public CreateOrganizationCommand(
            IAuthorizationService authorizationService
        )
        {
            _authorizationService = authorizationService;
        }

        public async Task<Guid> Handle(string orgId, string orgName, ClaimsPrincipal user)
        {
            // Extract access token from ClaimsPrincipal
            var accessToken = user.FindFirst("access_token")?.Value
                ?? user.FindFirst("jwt")?.Value
                ?? user.FindFirst("Authorization")?.Value
                ?? null;

            if (string.IsNullOrEmpty(accessToken))
                throw new UnauthorizedAccessException("No access token found in user claims.");

            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://keycloak:8080");
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var orgBody = new
            {
                id = orgId,
                name = orgName,
                enabled = true
            };

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(orgBody),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync("/admin/realms/NationOH/organizations", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Keycloak API error: {response.StatusCode} - {error}");
            }

            // Try to get the created org id from the Location header or response body
            Guid orgId = Guid.Empty;
            if (response.Headers.Location != null)
            {
                // e.g. Location: .../organizations/{id}
                var segments = response.Headers.Location.Segments;
                if (segments.Length > 0 && Guid.TryParse(segments[^1], out var parsedId))
                    orgId = parsedId;
            }
            else
            {
                // fallback: try to parse response body for id
                var body = await response.Content.ReadAsStringAsync();
                using var doc = System.Text.Json.JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("id", out var idProp) && Guid.TryParse(idProp.GetString(), out var parsedId))
                    orgId = parsedId;
            }

            if (orgId == Guid.Empty)
                throw new Exception("Could not determine organization id from Keycloak response.");

            return orgId;
        }
    }
}