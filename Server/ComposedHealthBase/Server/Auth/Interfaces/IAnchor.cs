namespace ComposedHealthBase.Server.Auth
{
    public interface IAnchor
    {
        Guid Id { get; }
        Guid CreatedById { get; }
    }
}