using System;

namespace Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities
{
    public class RolePermission
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}
