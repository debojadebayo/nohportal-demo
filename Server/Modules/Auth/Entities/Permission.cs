using System;

namespace Server.Modules.Auth.Entities
{
    public class Permission
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
