using ComposedHealthBase.Shared.Interfaces;
using Heron.MudCalendar;
namespace ComposedHealthBase.Shared.Models
{
    public class BaseCalendarItem : CalendarItem
    {
        public DateTime? StartTime
        {
            get
            {
                return Start;
            }
            set
            {
                if (value != null)
                {
                    Start = value.Value;
                }
                else
                {
                    Start = DateTime.UtcNow;
                    StartTime = Start;
                }
            }
        }
        public DateTime? EndTime
        {
            get
            {
                return End;
            }
            set
            {
                End = value;
            }
        }
    }
}

