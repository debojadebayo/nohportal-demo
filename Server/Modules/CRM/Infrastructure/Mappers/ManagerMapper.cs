using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Mappers
{
    public class ManagerMapper : IMapper<Manager, ManagerDto>
    {
        public ManagerDto Map(Manager entity)
        {
            return new ManagerDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Username = entity.Username,
                Telephone = entity.Telephone,
                Email = entity.Email,
                AvatarImage = entity.AvatarImage,
                AvatarTitle = entity.AvatarTitle,
                AvatarDescription = entity.AvatarDescription,
                Department = entity.Department,
                CustomerId = entity.CustomerId
            };
        }

        public Manager MapWithKeycloakId(ManagerDto dto, Guid keycloakId)
        {
            return new Manager
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username ?? $"{dto.FirstName}.{dto.LastName}".ToLower(),
                Telephone = dto.Telephone,
                Email = dto.Email,
                AvatarImage = dto.AvatarImage,
                AvatarTitle = dto.AvatarTitle,
                AvatarDescription = dto.AvatarDescription,
                KeycloakId = keycloakId,
                Department = dto.Department,
                CustomerId = dto.CustomerId,
                SearchTags = $"{dto.FirstName} {dto.LastName} {dto.Telephone} {dto.Email}".ToLower(),
            };
        }

        public IEnumerable<ManagerDto> Map(IEnumerable<Manager> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<Manager> Map(IEnumerable<ManagerDto> dtos)
        {
            return dtos.Select(Map);
        }

        public void Map(ManagerDto dto, Manager entity)
        {
            entity.Id = dto.Id;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Username = dto.Username ?? $"{dto.FirstName}.{dto.LastName}".ToLower();
            entity.Telephone = dto.Telephone;
            entity.Email = dto.Email;
            entity.AvatarImage = dto.AvatarImage;
            entity.AvatarTitle = dto.AvatarTitle;
            entity.AvatarDescription = dto.AvatarDescription;
            entity.Department = dto.Department;
            entity.CustomerId = dto.CustomerId;
            entity.SearchTags = $"{dto.FirstName} {dto.LastName} {dto.Telephone} {dto.Email}".ToLower();
        }

        public void Map(Manager entity, ManagerDto dto)
        {
            dto.Id = entity.Id;
            dto.FirstName = entity.FirstName;
            dto.LastName = entity.LastName;
            dto.Username = entity.Username;
            dto.Telephone = entity.Telephone;
            dto.Email = entity.Email;
            dto.AvatarImage = entity.AvatarImage;
            dto.AvatarTitle = entity.AvatarTitle;
            dto.AvatarDescription = entity.AvatarDescription;
            dto.Department = entity.Department;
            dto.CustomerId = entity.CustomerId;
        }

        public Manager Map(ManagerDto dto)
        {
            throw new NotImplementedException("Mapping from DTO to Entity without KeycloakId is not supported.");
        }
    }
}