using ComposedHealthBase.Server.Entities;

namespace Server.Modules.Auth.Entities
{
    public class LocalStorageKey : BaseEntity<LocalStorageKey>, IEntity, IAuditEntity
    {
        public required string ObjectTypeName { get; set; }
        public required Guid ObjectGuid { get; set; }
        public required string PrivateKey { get; set; }
        public required string PublicKey { get; set; }
        public DateTime KeyGeneratedDate { get; set; }
        public DateTime? KeyExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
