using System;

namespace Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities
{
    public class Permission
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public HashSet<Role> Roles { get; set; } = new();
    }
}
