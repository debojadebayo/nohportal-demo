using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Mappers
{
    public static class ManagerMapper
    {
        public static ManagerDto ToDto(Manager entity)
        {
            return new ManagerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                Department = entity.Department
            };
        }

        public static Manager ToEntity(ManagerDto dto)
        {
            return new Manager
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Department = dto.Department
            };
        }
    }
}