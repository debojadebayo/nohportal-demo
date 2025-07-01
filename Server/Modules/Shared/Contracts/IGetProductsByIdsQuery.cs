using Shared.DTOs.CRM;

namespace Server.Modules.Shared.Contracts
{
    public interface IGetProductsByIdsQuery
    {
        Task<List<ProductDto>> Handle(List<Guid> productIds);
    }
}
