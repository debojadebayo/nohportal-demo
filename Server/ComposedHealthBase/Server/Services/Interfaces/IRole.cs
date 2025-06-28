using System;

namespace ComposedHealthBase.Server.Services.Interfaces
{
    public interface IRole
    {
        Guid Id { get; set; }
        string Name { get; set; }
        ICollection<IPermission> Permissions { get; set; }
    }
}
