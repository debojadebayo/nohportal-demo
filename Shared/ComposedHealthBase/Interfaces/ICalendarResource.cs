namespace ComposedHealthBase.Shared.Interfaces
{
    public interface ICalendarResource<TCalendarItem>
    where TCalendarItem : ICalendarItem
    {
        string AvatarImage { get; set; }
        string AvatarTitle { get; set; }
        string AvatarDescription { get; set; }
        IEnumerable<TCalendarItem> Schedules { get; set; }
    }
}
