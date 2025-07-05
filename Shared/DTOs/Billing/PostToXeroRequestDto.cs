using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Billing
{
    public class PostToXeroRequestDto : IDto
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
    }
}
