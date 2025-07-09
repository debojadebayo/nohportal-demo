using Microsoft.AspNetCore.Authorization;

using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using System.Security.Claims;
using ComposedHealthBase.Server.Interfaces;
namespace ComposedHealthBase.Server.Commands
{
    public interface ICreateCommand<T, TDto, TContext>
    {
        Task<Guid> Handle(TDto dto, ClaimsPrincipal user);
    }

    public class CreateCommand<T, TDto, TContext> : ICreateCommand<T, TDto, TContext>, ICommand
        where T : class, IEntity, IAuditEntity
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }
        private readonly IAuthorizationService _authorizationService;
        //private readonly AuthDbContext _authDbContext; // Add this line

        public CreateCommand(
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
            var newEntity = _mapper.Map(dto);

            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesWithAuditAsync(user);
            return newEntity.Id;
        }
    }
}