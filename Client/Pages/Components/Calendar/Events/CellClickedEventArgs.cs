using ComposedHealthBase.Shared.Interfaces;
using ComposedHealthBase.Shared.Models;
using System;

namespace Client.Pages.Components.Calendar.Events
{
    public class CellClickedEventArgs<TResource, TCalendarItem>
        where TResource : ICalendarResource<TCalendarItem>
        where TCalendarItem : BaseCalendarItem
    {
        public DateTime DateTime { get; set; }
        public TResource Resource { get; set; }

        public CellClickedEventArgs(DateTime dateTime, TResource resource)
        {
            DateTime = dateTime;
            Resource = resource;
        }
    }
}