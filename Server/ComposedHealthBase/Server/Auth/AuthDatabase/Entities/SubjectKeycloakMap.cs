using System;

namespace Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities
{
    public class SubjectKeycloakMap
    {
        public long Id { get; set; }
        public required long SubjectId { get; set; }
        public required Guid KeycloakId { get; set; }
    }
}
