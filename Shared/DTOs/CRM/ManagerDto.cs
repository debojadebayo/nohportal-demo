using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using System;

namespace Shared.DTOs.CRM
{
    public class ManagerDto : BaseDto<ManagerDto>, IDto, ILazyLookup
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string DisplayName => Name;
    }
}