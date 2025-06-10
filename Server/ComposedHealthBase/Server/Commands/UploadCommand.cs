using Microsoft.AspNetCore.Authorization;

using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using System.Threading.Tasks;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;

namespace ComposedHealthBase.Server.Commands
{
    public interface IUploadCommand<T, TDto, TContext>
    {
        Task<long> Handle(TDto dto, ClaimsPrincipal user);
    }

    public class UploadCommand<T, TDto, TContext> : IUploadCommand<T, TDto, TContext>, ICommand
        where T : BaseEntity<T>
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;

        public UploadCommand(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<long> Handle(TDto dto, ClaimsPrincipal user)
        {
            var newEntity = _mapper.Map(dto);
            var authResult = await _authorizationService.AuthorizeAsync(user, newEntity, "resource-access");
            if (!authResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Authorization failed for resource-access policy.");
            }
            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesWithAuditAsync(user);
            return newEntity.Id;
        }
    }
}