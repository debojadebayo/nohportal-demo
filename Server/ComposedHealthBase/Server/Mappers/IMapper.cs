namespace ComposedHealthBase.Server.Mappers
{
    public interface IMapper<T, TDto>
    {
        TDto Map(T entity);
        T Map(TDto dto);
        IEnumerable<TDto> Map(IEnumerable<T> entities);
        IEnumerable<T> Map(IEnumerable<TDto> dtos);
        void Map(TDto dto, T entity);
        void Map(T entity, TDto dto);
        void Map(IEnumerable<TDto> dtos, IEnumerable<T> entities);
        void Map(IEnumerable<T> entities, IEnumerable<TDto> dtos);
    }
}