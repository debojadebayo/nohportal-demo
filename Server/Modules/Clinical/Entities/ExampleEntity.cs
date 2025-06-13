// filepath: Modules/Clinical/Entities/ExampleEntity.cs
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Shared.DTOs;

namespace Server.Modules.Clinical.Entities;

public class ExampleEntity : BaseEntity<ExampleEntity>, IEntity, IAuditEntity
{
    public string? Name { get; set; }
}
public class ExampleEntityDto : IDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
}
