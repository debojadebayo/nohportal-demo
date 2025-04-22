namespace ComposedHealthBase.Shared.Interfaces
{
    public interface ICalendarResource
    {
        string AvatarImage { get; set; }
        string AvatarTitle { get; set; }
        string AvatarDescription { get; set; }
        IEnumerable<ICalendarItem> Schedules { get; set; }
    }
}
