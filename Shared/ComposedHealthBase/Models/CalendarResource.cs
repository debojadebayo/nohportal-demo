using ComposedHealthBase.Shared.Interfaces;

namespace ComposedHealthBase.Shared.Models
{
    public class CalendarResource<TCalendarItem>
        where TCalendarItem : class
    {
        public string AvatarImage { get; set; }
        public string AvatarTitle { get; set; }
        public string AvatarDescription { get; set; }
        public IEnumerable<TCalendarItem> Schedules { get; set; } = new List<TCalendarItem>();
    }
}