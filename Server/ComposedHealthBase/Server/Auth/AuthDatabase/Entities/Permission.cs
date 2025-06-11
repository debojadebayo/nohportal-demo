using System;

namespace Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities
{
    public class Permission
    {
        public long Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
