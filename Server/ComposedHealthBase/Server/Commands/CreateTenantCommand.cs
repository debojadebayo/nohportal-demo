using Microsoft.AspNetCore.Authorization;

using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Authentication.ClientFactory;
using FS.Keycloak.RestApiClient.Authentication.Flow;
using FS.Keycloak.RestApiClient.ClientFactory;
using FS.Keycloak.RestApiClient.Model;
using Microsoft.Extensions.Options;
using ComposedHealthBase.Server.Config;
using ComposedHealthBase.Server.Services;

namespace ComposedHealthBase.Server.Commands
{
    public interface ICreateTenantCommand<T, TDto, TContext>
    {
        Task<TDto> Handle(TDto dto, ClaimsPrincipal user);
    }

    public class CreateTenantCommand<T, TDto, TContext> : ICreateTenantCommand<T, TDto, TContext>, ICommand
        where T : class, IEntity, IAuditEntity, ITenant
        where TDto : IDto, ITenant
        where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;
        private readonly IKeycloakService _keycloakService;

        public CreateTenantCommand(
            IDbContext<TContext> dbContext,
            IMapper<T, TDto> mapper,
            IAuthorizationService authorizationService,
            IKeycloakService keycloakService
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _keycloakService = keycloakService;
        }

        public async Task<TDto> Handle(TDto dto, ClaimsPrincipal user)
        {
            var orgBody = new OrganizationRepresentation
            {
                Name = dto.Name,
                Domains = new List<OrganizationDomainRepresentation> { new OrganizationDomainRepresentation { Name = dto.Domain } },
                Enabled = true
            };

            using var organizationsApi = _keycloakService.CreateOrganizationsApi();
            await organizationsApi.PostOrganizationsAsync(_keycloakService.GetRealmName(), orgBody);

            var newId = (await organizationsApi.GetOrganizationsAsync(_keycloakService.GetRealmName(), true, true, null, 1, null, dto.Name)).FirstOrDefault()?.Id;

            var newEntity = _mapper.Map(dto);

            newEntity.Id = newId != null ? Guid.Parse(newId) : Guid.NewGuid();

            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesWithAuditAsync(user);
            dto.Id = newEntity.Id;
            return dto;
        }
    }
}