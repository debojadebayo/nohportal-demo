using ComposedHealthBase.Shared.Models;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.CRM
{
    public class EmployeeDocumentDto : BaseDocumentDto, ILazyLookup
    {
        public long EmployeeId { get; set; }
    }
}