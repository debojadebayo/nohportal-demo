using Heron.MudCalendar;
using ComposedHealthBase.Shared.Interfaces;

public class CustomCalendarItem<T> : CalendarItem, ICalendarItem
    where T : ICalendarItem
{
    public T Data { get; set; }

    public string Title
    {
        get => Data.Title;
        set => Data.Title = value;
    }

    public string Description
    {
        get => Data.Description;
        set => Data.Description = value;
    }

    public DateTime Start
    {
        get => Data.Start;
        set => Data.Start = value;
    }

    public DateTime End
    {
        get => Data.End;
        set => Data.End = value;
    }
}