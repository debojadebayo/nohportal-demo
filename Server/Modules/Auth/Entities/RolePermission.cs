using System;

namespace Server.Modules.Auth.Entities
{
    public class RolePermission
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}
