using System;

namespace Server.Modules.Auth.Entities
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
