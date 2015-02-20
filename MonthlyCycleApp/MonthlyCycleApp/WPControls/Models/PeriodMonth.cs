using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Windows;
using System.ComponentModel;


namespace WPControls.Models
{
    [DataContract]
    public class PeriodMonth 
    {
        [DataMember]
        public DateTime CycleStartDay
        {
            get;
            set;
        }
        
         [DataMember]
        public int CycleDuration { get; set; }
         [DataMember]
        public int PeriodDuration { get; set; }
       

        public PeriodMonth() { }

        public PeriodMonth(DateTime cycleStartDay, int cycleDuration, int periodDuration)
        {
            this.PeriodDuration = periodDuration;
            this.CycleDuration = cycleDuration;
            this.CycleStartDay = cycleStartDay;
        }

        private DateTime cycleEndDay;
        public DateTime CycleEndDay
        {
            get
            {
                if (cycleEndDay == DateTime.MinValue)
                    cycleEndDay = CycleStartDay.AddDays(CycleDuration);
                return cycleEndDay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    cycleEndDay = value;
            }
        }

        private DateTime periodEndDay;
        public DateTime PeriodEndDay
        {
            get
            {
                if (periodEndDay == DateTime.MinValue)
                     periodEndDay = CycleStartDay.AddDays(PeriodDuration);
                return periodEndDay;
            }
            set
            {
                if (value != DateTime.MinValue)                  
                    periodEndDay = value;
            }
        }

        private DateTime _fertilityStartDay;
        public DateTime FertilityStartDay
        {
            get
            {
                return _fertilityStartDay != DateTime.MinValue ? _fertilityStartDay : _fertilityStartDay = CycleStartDay.AddDays(PeriodDuration / 2);
            }
            set
            {
                if (value != DateTime.MinValue)                  
                    _fertilityStartDay = value;
            }
        }

        public int FertilityDuration { get { return 5; } }



       
    }
}
