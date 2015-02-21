using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPControls.Models;

namespace WPControls
{
    public class CalendarChangedEventArgs: EventArgs
    {
        // ReSharper disable UnusedMember.Local
        private CalendarChangedEventArgs() { }
        // ReSharper restore UnusedMember.Local

        internal CalendarChangedEventArgs(PeriodCalendar calendar)
        {
            Calendar = calendar;
        }

        /// <summary>
        /// Date that is currently selected on the calendar
        /// </summary>
        public PeriodCalendar Calendar { get; private set; }
    }
}
