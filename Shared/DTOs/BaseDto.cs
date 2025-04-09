namespace ComposedHealthBase.Shared.DTOs
{
    public class BaseDto<TDto> : IDto
    where TDto : class
    {
        public long Id { get; set; }
    }
}