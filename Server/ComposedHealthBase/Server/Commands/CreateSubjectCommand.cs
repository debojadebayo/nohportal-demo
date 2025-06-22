using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Shared.Interfaces;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;
namespace ComposedHealthBase.Server.Commands
{
    public interface ICreateSubjectCommand<T, TDto, TContext>
    {
        Task<Guid> Handle(TDto dto, ClaimsPrincipal user);
    }

    public class CreateSubjectCommand<T, TDto, TContext> : ICreateSubjectCommand<T, TDto, TContext>, ICommand
        where T : class, IEntity, IAuditEntity, ISubject
        where TDto : IDto, ISubject
        where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;
        //private readonly AuthDbContext _authDbContext; // Add this line

        public CreateSubjectCommand(
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

            // Map DTO to Keycloak UserRepresentation
            var userBody = new
            {
                id = dto.Id.ToString(), // assumes dto has Id property
                username = dto.Username, // assumes dto has Username property
                firstName = dto.FirstName, // assumes dto has FirstName property
                lastName = dto.LastName,   // assumes dto has LastName property
                email = dto.Email,         // assumes dto has Email property
                enabled = true,
                emailVerified = true,
                // attributes = new Dictionary<string, string[]>(), // optional, add if needed
            };

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(userBody),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync("/admin/realms/NationOH/users", content);

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