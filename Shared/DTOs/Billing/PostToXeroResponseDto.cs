using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Billing
{
    public class PostToXeroResponseDto : IDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? XeroInvoiceId { get; set; }
    }
}
