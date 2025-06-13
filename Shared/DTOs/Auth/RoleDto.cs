using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.Auth
{
    public class RoleDto : IDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}
