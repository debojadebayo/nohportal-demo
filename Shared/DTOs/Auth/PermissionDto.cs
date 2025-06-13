using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.Auth
{
    public class PermissionDto : IDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}