using System;

namespace Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public HashSet<Permission> Permissions { get; set; } = new();
    }
}
