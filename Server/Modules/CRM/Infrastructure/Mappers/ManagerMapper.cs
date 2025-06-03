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
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                Department = entity.Department,
                CustomerId = entity.CustomerId
            };
        }

        public Manager MapWithKeycloakId(ManagerDto dto, Guid keycloakId)
        {
            return new Manager
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Department = dto.Department,
                KeycloakId = keycloakId,
                CustomerId = dto.CustomerId
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
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.Department = dto.Department;
            entity.CustomerId = dto.CustomerId;
        }

        public void Map(Manager entity, ManagerDto dto)
        {
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Email = entity.Email;
            dto.Phone = entity.Phone;
            dto.Department = entity.Department;
            dto.CustomerId = entity.CustomerId;
        }

        public Manager Map(ManagerDto dto)
        {
            throw new NotImplementedException("Mapping from DTO to Entity without KeycloakId is not supported.");
        }
    }
}