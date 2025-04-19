using Heron.MudCalendar;

public class CustomCalendarItem<T> : CalendarItem
    where T : class
{
    public required T Data { get; set; }
}