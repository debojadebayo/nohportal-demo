using Microsoft.AspNetCore.Authorization;

using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Shared.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;
namespace ComposedHealthBase.Server.Commands
{
    public interface IUpdateCommand<T, TDto, TContext>
    {
        Task<long> Handle(TDto dto, ClaimsPrincipal user);
    }

    public class UpdateCommand<T, TDto, TContext> : IUpdateCommand<T, TDto, TContext>, ICommand
        where T : BaseEntity<T>
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;

        public UpdateCommand(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<long> Handle(TDto dto, ClaimsPrincipal user)
        {
            var existingEntity = await _dbContext.Set<T>().FindAsync(dto.Id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with id {dto.Id} not found.");
            }
            var authResult = await _authorizationService.AuthorizeAsync(user, existingEntity, "resource-access");
            if (!authResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Authorization failed for resource-access policy.");
            }
            _mapper.Map(dto, existingEntity);
            _dbContext.Set<T>().Update(existingEntity);
            await _dbContext.SaveChangesWithAuditAsync(user);
            return existingEntity.Id;
        }
    }
}