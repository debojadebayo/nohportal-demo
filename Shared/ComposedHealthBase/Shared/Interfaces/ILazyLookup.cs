namespace ComposedHealthBase.Shared.Interfaces
{
    public interface ILazyLookup
    {
        Guid Id { get; set; }
        string DisplayName { get; }
    }
}