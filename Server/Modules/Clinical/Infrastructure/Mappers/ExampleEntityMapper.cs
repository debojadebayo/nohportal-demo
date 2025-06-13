using ComposedHealthBase.Server.Mappers;
using Server.Modules.Clinical.Entities;

public class ExampleEntityMapper : IMapper<ExampleEntity, ExampleEntityDto>
{
    private readonly IMapper<ExampleEntity, ExampleEntityDto> _mapper;
    public ExampleEntityMapper(IMapper<ExampleEntity, ExampleEntityDto> mapper)
    {
        _mapper = mapper;
    }
    public ExampleEntityDto Map(ExampleEntity entity)
    {
        return new ExampleEntityDto
        {

        };
    }

    public ExampleEntity MapWithKeycloakId(ExampleEntityDto dto, Guid keycloakId)
    {
        return new ExampleEntity
        {

        };
    }

    public ExampleEntity Map(ExampleEntityDto dto)
    {
        throw new NotImplementedException("Map method without KeycloakId is not implemented. Use MapWithKeycloakId instead.");
    }

    public IEnumerable<ExampleEntityDto> Map(IEnumerable<ExampleEntity> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<ExampleEntity> Map(IEnumerable<ExampleEntityDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ExampleEntityDto dto, ExampleEntity entity)
    {

    }

    public void Map(ExampleEntity entity, ExampleEntityDto dto)
    {

    }

    public void Map(IEnumerable<ExampleEntityDto> dtos, IEnumerable<ExampleEntity> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<ExampleEntity> entities, IEnumerable<ExampleEntityDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}
