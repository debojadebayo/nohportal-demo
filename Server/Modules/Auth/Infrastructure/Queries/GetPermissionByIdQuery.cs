using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using Shared.DTOs.Auth;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using ComposedHealthBase.Server.Interfaces;

namespace Server.Modules.Auth.Infrastructure.Queries
{
    public interface IGetPermissionByIdQuery : IQuery
    {
        Task<PermissionDto?> Handle(Guid id);
    }

    public class GetPermissionByIdQuery : IGetPermissionByIdQuery
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper<Permission, PermissionDto> _mapper;

        public GetPermissionByIdQuery(AuthDbContext dbContext, IMapper<Permission, PermissionDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PermissionDto?> Handle(Guid id)
        {
            var permission = await _dbContext.Set<Permission>().FindAsync(id);
            return permission != null ? _mapper.Map(permission) : null;
        }
    }
}
