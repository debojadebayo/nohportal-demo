using Server.Modules.Auth.Entities;
using Shared.DTOs.Auth;
using ComposedHealthBase.Server.Mappers;
using System.Collections.Generic;
using System.Linq;

namespace Server.Modules.Auth.Infrastructure.Mappers
{
    public class RoleMapper : IMapper<Role, RoleDto>
    {
        public RoleDto Map(Role entity)
        {
            // Permissions may be null if not loaded; handle accordingly
            var permissions = entity.Permissions ?? new List<Permission>();
            return new RoleDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = permissions.Select(MapPermissionToDto).ToList()
            };
        }

        public Role Map(RoleDto dto)
        {
            return new Role
            {
                Id = dto.Id,
                Name = dto.Name,
                Permissions = dto.Permissions?.Select(p => new Permission
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList() ?? new List<Permission>()
            };
        }

        public IEnumerable<RoleDto> Map(IEnumerable<Role> entities)
        {
            return entities.Select(Map).ToList();
        }

        public IEnumerable<Role> Map(IEnumerable<RoleDto> dtos)
        {
            return dtos.Select(Map).ToList();
        }

        public void Map(RoleDto dto, Role entity)
        {
            entity.Name = dto.Name;
            entity.Permissions = dto.Permissions?.Select(p => new Permission
            {
                Id = p.Id,
                Name = p.Name
            }).ToList() ?? new List<Permission>();
        }

        public void Map(Role entity, RoleDto dto)
        {
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Permissions = (entity.Permissions ?? new List<Permission>()).Select(MapPermissionToDto).ToList();
        }

        public static RoleDto MapToDto(Role role, IEnumerable<Permission> permissions)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = permissions.Select(MapPermissionToDto).ToList()
            };
        }

        public static PermissionDto MapPermissionToDto(Permission permission)
        {
            return new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name
            };
        }
    }
}
