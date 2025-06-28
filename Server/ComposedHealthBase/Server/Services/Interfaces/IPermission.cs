using System;

namespace ComposedHealthBase.Server.Services.Interfaces
{
    public interface IPermission
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
