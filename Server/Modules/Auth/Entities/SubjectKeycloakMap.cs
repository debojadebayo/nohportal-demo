using System;

namespace Server.Modules.Auth.Entities
{
    public class SubjectKeycloakMap
    {
        public Guid Id { get; set; }
        public required Guid SubjectId { get; set; }
        public required Guid KeycloakId { get; set; }
    }
}
