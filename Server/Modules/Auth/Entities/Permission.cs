using ComposedHealthBase.Server.Entities;

namespace Server.Modules.Auth.Entities
{
    public class Permission : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
