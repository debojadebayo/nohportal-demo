using ComposedHealthBase.Shared.Models;
using System;

namespace Client.Pages.Components.Calendar.Events
{
    public class ItemClickedEventArgs<TCalendarItem>
        where TCalendarItem : BaseCalendarItem
    {
        public TCalendarItem Item { get; set; }

        public ItemClickedEventArgs(TCalendarItem item)
        {
            Item = item;
        }
    }
}