using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Authentication.ClientFactory;
using FS.Keycloak.RestApiClient.Authentication.Flow;
using FS.Keycloak.RestApiClient.ClientFactory;
using Microsoft.Extensions.Options;
using ComposedHealthBase.Server.Config;

namespace ComposedHealthBase.Server.Services
{
    public interface IKeycloakService
    {
        OrganizationsApi CreateOrganizationsApi();
        UsersApi CreateUsersApi();
        string GetRealmName();
        RolesApi CreateRolesApi();
        RoleMapperApi CreateRoleMapperApi();
    }

    public class KeycloakService : IKeycloakService
    {
        private readonly AppOptions _options;

        public KeycloakService(IOptions<AppOptions> options)
        {
            _options = options.Value;
        }

        public OrganizationsApi CreateOrganizationsApi()
        {
            var kc = _options.KeycloakAdminClient;
            var credentials = new ClientCredentialsFlow
            {
                KeycloakUrl = kc.KeycloakUrl,
                Realm = kc.Realm,
                ClientId = kc.ClientId,
                ClientSecret = kc.ClientSecret
            };

            var httpClient = AuthenticationHttpClientFactory.Create(credentials);
            return ApiClientFactory.Create<OrganizationsApi>(httpClient);
        }

        public UsersApi CreateUsersApi()
        {
            var kc = _options.KeycloakAdminClient;
            var credentials = new ClientCredentialsFlow
            {
                KeycloakUrl = kc.KeycloakUrl,
                Realm = kc.Realm,
                ClientId = kc.ClientId,
                ClientSecret = kc.ClientSecret
            };

            var httpClient = AuthenticationHttpClientFactory.Create(credentials);
            return ApiClientFactory.Create<UsersApi>(httpClient);
        }
        public RoleMapperApi CreateRoleMapperApi()
        {
            var kc = _options.KeycloakAdminClient;
            var credentials = new ClientCredentialsFlow
            {
                KeycloakUrl = kc.KeycloakUrl,
                Realm = kc.Realm,
                ClientId = kc.ClientId,
                ClientSecret = kc.ClientSecret
            };

            var httpClient = AuthenticationHttpClientFactory.Create(credentials);
            return ApiClientFactory.Create<RoleMapperApi>(httpClient);
        }

        public string GetRealmName()
        {
            return _options.KeycloakAdminClient.Realm;
        }

        public RolesApi CreateRolesApi()
        {
                        var kc = _options.KeycloakAdminClient;
            var credentials = new ClientCredentialsFlow
            {
                KeycloakUrl = kc.KeycloakUrl,
                Realm = kc.Realm,
                ClientId = kc.ClientId,
                ClientSecret = kc.ClientSecret
            };

            var httpClient = AuthenticationHttpClientFactory.Create(credentials);
            return ApiClientFactory.Create<RolesApi>(httpClient);
        }
    }
}
