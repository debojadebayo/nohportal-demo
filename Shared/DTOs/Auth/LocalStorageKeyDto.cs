using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Auth
{
    public class LocalStorageKeyDto : BaseDto<LocalStorageKeyDto>, IDto, IAuditDto
    {
        public required string ObjectTypeName { get; set; }
        public required Guid ObjectGuid { get; set; }
        public required string PrivateKey { get; set; }
        public required string PublicKey { get; set; }
        public DateTime KeyGeneratedDate { get; set; }
        public DateTime? KeyExpiryDate { get; set; }
    }
}
