using Microsoft.AspNetCore.Authorization;

using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Shared.Interfaces;
namespace ComposedHealthBase.Server.Commands
{
    public interface ICreateTenantCommand<T, TDto, TContext>
    {
        Task<Guid> Handle(TDto dto, ClaimsPrincipal user);
    }

    public class CreateTenantCommand<T, TDto, TContext> : ICreateTenantCommand<T, TDto, TContext>, ICommand
        where T : class, IEntity, IAuditEntity, ITenant
        where TDto : IDto, ITenant
        where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;
        //private readonly AuthDbContext _authDbContext; // Add this line

        public CreateTenantCommand(
            IDbContext<TContext> dbContext,
            IMapper<T, TDto> mapper,
            IAuthorizationService authorizationService
            //AuthDbContext authDbContext // Add this parameter
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            //_authDbContext = authDbContext; // Assign here
        }

        public async Task<Guid> Handle(TDto dto, ClaimsPrincipal user)
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
                id = dto.Id.ToString(),
                name = dto.Name,
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

            var newEntity = _mapper.Map(dto);

            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesWithAuditAsync(user);
            return newEntity.Id;
        }
    }
}