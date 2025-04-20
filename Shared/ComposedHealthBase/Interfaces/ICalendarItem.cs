namespace ComposedHealthBase.Shared.Interfaces
{
    public interface ICalendarItem
    {
        string Title { get; set; }
        string Description { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
    }
}
