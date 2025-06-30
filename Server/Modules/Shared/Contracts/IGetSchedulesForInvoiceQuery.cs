using Shared.DTOs.Scheduling;

namespace Server.Modules.Shared.Contracts
{
    public interface IGetSchedulesForInvoiceQuery
    {
        Task<IEnumerable<ScheduleDto>> Handle(Guid customerId, DateTime fromDate, DateTime toDate, Guid? productId = null);
    }
}