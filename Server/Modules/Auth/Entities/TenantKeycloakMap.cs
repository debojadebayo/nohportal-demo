using System;

namespace Server.Modules.Auth.Entities
{
    public class TenantKeycloakMap
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public required Guid KeycloakId { get; set; }
    }
}
