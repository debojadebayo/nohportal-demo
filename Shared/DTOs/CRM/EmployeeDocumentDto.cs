using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;
using Shared.Enums;

namespace Shared.DTOs.CRM
{
    public class EmployeeDocumentDto : BaseDocumentDto, ILazyLookup
    {
        public long EmployeeId { get; set; }
        public EmployeeDocumentTypeEnum EmployeeDocumentType { get; set; }
    }
}