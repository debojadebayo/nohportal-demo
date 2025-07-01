using ComposedHealthBase.Server.Entities;

namespace Server.Modules.Auth.Entities
{
    public class Role : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}