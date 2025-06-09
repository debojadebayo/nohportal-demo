namespace ComposedHealthBase.Server.Auth
{
    public interface IAnchor
    {
        long Id { get; }
        Guid CreatedByKeycloakId { get; }
    }
}