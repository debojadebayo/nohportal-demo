using Shared.DTOs.CRM;

namespace Server.Modules.Shared.Contracts
{
    public interface IGetCustomerByIdQuery
    {
        Task<CustomerDto?> Handle(Guid customerId);
    }
}
