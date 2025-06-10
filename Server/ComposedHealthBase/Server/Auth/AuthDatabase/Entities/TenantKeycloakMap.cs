using System;

namespace Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities
{
    public class TenantKeycloakMap
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public required Guid KeycloakId { get; set; }
    }
}
