namespace ComposedHealthBase.Shared.Interfaces
{
    public interface ICalendarResource<TCalendarItem>
    where TCalendarItem : Heron.MudCalendar.CalendarItem
    {
        string AvatarImage { get; set; }
        string AvatarTitle { get; set; }
        string AvatarDescription { get; set; }
        IEnumerable<TCalendarItem> CalendarItems { get; set; }
    }
}