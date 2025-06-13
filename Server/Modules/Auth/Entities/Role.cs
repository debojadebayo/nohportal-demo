using ComposedHealthBase.Server.Enumerations;

namespace Server.Modules.Auth.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}