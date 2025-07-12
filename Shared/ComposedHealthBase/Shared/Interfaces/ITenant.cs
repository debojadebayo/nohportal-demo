namespace ComposedHealthBase.Shared.Interfaces
{
    public interface ITenant
    {
        string Name { get; set; }
        string Domain { get; set; }
    }
}
