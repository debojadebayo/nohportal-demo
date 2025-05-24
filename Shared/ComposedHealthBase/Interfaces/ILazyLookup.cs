namespace ComposedHealthBase.Shared.Interfaces
{
    public interface ILazyLookup
    {
        long Id { get; set; }
        string DisplayName { get; }
    }
}