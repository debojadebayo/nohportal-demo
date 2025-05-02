using ComposedHealthBase.Shared.Interfaces;
using Heron.MudCalendar;
namespace ComposedHealthBase.Shared.Models;

public class BaseCalendarItem : CalendarItem
{
    public new DateTime? Start { get; set; }
    public new DateTime? End { get; set; }
}