using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;
using ComposedHealthBase.Shared.DTOs;
using Shared.Enums;

namespace Shared.DTOs.CRM
{
    public class EmployeeDocumentDto : BaseDocumentDto, ILazyLookup, IDto
    {
        public Guid EmployeeId { get; set; }
        public EmployeeDocumentTypeEnum EmployeeDocumentType { get; set; }
    }
}