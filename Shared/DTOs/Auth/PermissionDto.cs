using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.Auth
{
    public class PermissionDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}