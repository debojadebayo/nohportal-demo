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
    public interface ICreateSubjectCommand<T, TDto, TContext>
    {
        Task<TDto> Handle(TDto dto, ClaimsPrincipal user);
    }

    public class CreateSubjectCommand<T, TDto, TContext> : ICreateSubjectCommand<T, TDto, TContext>, ICommand
        where T : class, IEntity, IAuditEntity, ISubject
        where TDto : IDto, IAuditDto, ISubject
        where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;
        private readonly IKeycloakService _keycloakService;

        public CreateSubjectCommand(
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
            var userRep = new UserRepresentation
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.FirstName + dto.LastName,
                Email = dto.Email,
                RealmRoles = new List<string> { "subject" },
                Enabled = true,
            };

            using var usersApi = _keycloakService.CreateUsersApi();
            await usersApi.PostUsersAsync(_keycloakService.GetRealmName(), userRep);

            var newId = (await usersApi.GetUsersAsync(_keycloakService.GetRealmName(), true, dto.Email, null, null, true, null, dto.FirstName, null, null, dto.LastName, 1)).FirstOrDefault()?.Id;

            // Add user to organization if TenantId is provided
            if (!string.IsNullOrEmpty(newId))
            {
                using var organizationsApi = _keycloakService.CreateOrganizationsApi();

                await organizationsApi.PostOrganizationsMembersByOrgIdAsync(
                    _keycloakService.GetRealmName(),
                    dto.TenantId.ToString(),
                    newId.ToString()
                );

                // Assign user to the subject role
                using var roleMappingApi = _keycloakService.CreateRoleMappingApi();
                roleMappingApi.SetRoleMappingsAsync(
                    _keycloakService.GetRealmName(),
                    newId.ToString(),
                    new List<string> { "subject" }
                );
            }

            var newEntity = _mapper.Map(dto);

            newEntity.Id = newId != null ? Guid.Parse(newId) : Guid.NewGuid();

            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesWithAuditAsync(user);

            dto.Id = newEntity.Id;
            return dto;
        }
    }
}