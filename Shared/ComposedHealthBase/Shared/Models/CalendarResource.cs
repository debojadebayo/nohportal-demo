using ComposedHealthBase.Shared.Interfaces;
using Heron.MudCalendar;

namespace ComposedHealthBase.Shared.Models
{
    public class CalendarResource<TCalendarItem> : ICalendarResource<TCalendarItem>
        where TCalendarItem : BaseCalendarItem
    {
        public string AvatarImage { get; set; } = string.Empty;
        public string AvatarTitle { get; set; } = string.Empty;
        public string AvatarDescription { get; set; } = string.Empty;
        public IEnumerable<TCalendarItem> CalendarItems { get; set; } = new List<TCalendarItem>();
    }
}