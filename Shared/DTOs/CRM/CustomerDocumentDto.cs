using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.CRM
{
    public class CustomerDocumentDto : BaseDocumentDto
    {
        public long CustomerId { get; set; }
    }
}