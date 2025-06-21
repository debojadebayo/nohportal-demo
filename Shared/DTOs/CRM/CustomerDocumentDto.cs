using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;
using Shared.Enums;

namespace Shared.DTOs.CRM
{
    public class CustomerDocumentDto : BaseDocumentDto
    {
        public long CustomerId { get; set; }
        public CustomerDocumentTypeEnum CustomerDocumentType { get; set; }
    }
}