using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;
using Shared.Enums;
using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.CRM
{
    public class CustomerDocumentDto : BaseDocumentDto, IDto
    {
        public CustomerDocumentTypeEnum CustomerDocumentType { get; set; }
    }
}