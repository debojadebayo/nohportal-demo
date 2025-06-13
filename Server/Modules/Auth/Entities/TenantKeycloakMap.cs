using System;

namespace Server.Modules.Auth.Entities
{
    public class TenantKeycloakMap
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public required Guid KeycloakId { get; set; }
    }
}
