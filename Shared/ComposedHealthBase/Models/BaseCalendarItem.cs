using ComposedHealthBase.Shared.Interfaces;
using Heron.MudCalendar;
namespace ComposedHealthBase.Shared.Models;

public class BaseCalendarItem : CalendarItem, ICalendarItem
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AvatarImage { get; set; } = string.Empty;
    public string AvatarTitle { get; set; } = string.Empty;
    public string AvatarDescription { get; set; } = string.Empty;
    public DateTime Start { get; set; } = DateTime.UtcNow;
    public DateTime End { get; set; } = DateTime.UtcNow;
}