using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using System;

namespace Shared.DTOs.CRM
{
    public class ManagerDto : BaseDto<ManagerDto>, IDto, IAuditDto, ILazyLookup
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarImage { get; set; }
        public string? AvatarTitle { get; set; }
        public string? AvatarDescription { get; set; }
        public string Department { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public string DisplayName => $"{FirstName} {LastName} - {Department} - Id: {Id}";
    }
}