using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;

namespace Shared.DTOs.CRM
{
    public class ContractDto : BaseDto<ContractDto>, ILazyLookup, IDto, IAuditDto
    {
        public required string Reference { get; set; }
        public string? Notes { get; set; }
        public Guid RepresentativeId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string DisplayName => $"{Reference} - {StartTime?.ToString("d") ?? "N/A"} to {EndTime?.ToString("d") ?? "N/A"}";
        public Guid CustomerId { get; set; }
    }
}
