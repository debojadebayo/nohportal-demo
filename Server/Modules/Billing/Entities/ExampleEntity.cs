// filepath: Modules/Billing/Entities/ExampleEntity.cs
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Shared.DTOs;

namespace Server.Modules.Billing.Entities;

public class ExampleEntity : BaseEntity<ExampleEntity>, IEntity, IAuditEntity
{
    public string? Name { get; set; }
}
public class ExampleEntityDto : IDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
}
