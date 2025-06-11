using ComposedHealthBase.Server.Enumerations;

namespace Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities
{
    public class Role : Enumeration<Role>
    {
        public static readonly Role Administrator = new(1, "Administrator");

        public Role(long id, string name) : base(id, name)
        {
        }
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
