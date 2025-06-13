using Server.Modules.Auth.Entities;
using Shared.DTOs.Auth;
using ComposedHealthBase.Server.Mappers;
using System.Collections.Generic;
using System.Linq;

namespace Server.Modules.Auth.Infrastructure.Mappers
{
    public class PermissionMapper : IMapper<Permission, PermissionDto>
    {
        public PermissionDto Map(Permission entity)
        {
            return new PermissionDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public Permission Map(PermissionDto dto)
        {
            return new Permission
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public IEnumerable<PermissionDto> Map(IEnumerable<Permission> entities)
        {
            return entities.Select(Map).ToList();
        }

        public IEnumerable<Permission> Map(IEnumerable<PermissionDto> dtos)
        {
            return dtos.Select(Map).ToList();
        }

        public void Map(PermissionDto dto, Permission entity)
        {
            entity.Name = dto.Name;
        }

        public void Map(Permission entity, PermissionDto dto)
        {
            dto.Id = entity.Id;
            dto.Name = entity.Name;
        }
    }
}
