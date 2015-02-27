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
        public DateTime PeriodStartDay
        {
            get;
            set;
        }
        
        /// <summary>
        /// Between 3-5 days
        /// </summary>
         [DataMember]
        public int PeriodDuration { get; set; }

        /// <summary>
        /// average 28
        /// </summary>
         [DataMember]
        public int CycleDuration { get; set; }
       

        public PeriodMonth() { }

        public PeriodMonth(DateTime periodStartDay, int cycleDuration, int periodDuration)
        {
            this.CycleDuration = cycleDuration;
            this.PeriodDuration = periodDuration;
            this.PeriodStartDay = periodStartDay;
        }

        private DateTime periodEndDay;
        public DateTime PeriodEndDay
        {
            get
            {
                if (periodEndDay == DateTime.MinValue)
                    periodEndDay = PeriodStartDay.AddDays(PeriodDuration - 1);
                return periodEndDay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    periodEndDay = value;
            }
        }

        private DateTime cycleEndDay;
        public DateTime CycleEndDay
        {
            get
            {
                if (cycleEndDay == DateTime.MinValue)
                     cycleEndDay = PeriodStartDay.AddDays(CycleDuration-1);
                return cycleEndDay;
            }
            set
            {
                if (value != DateTime.MinValue)                  
                    cycleEndDay = value;
            }
        }


        private DateTime ovulationPeakDay;
        public DateTime OvulationPeakDay
        {
            get
            {
                return ovulationPeakDay != DateTime.MinValue ? ovulationPeakDay : ovulationPeakDay = PeriodStartDay.AddDays(CycleDuration / 2);
            }
            set
            {
                if (value != DateTime.MinValue)
                    ovulationPeakDay = value;
            }
        }


        private DateTime fertilityStartDay;
        public DateTime FertilityStartDay
        {
            get
            {
                return fertilityStartDay != DateTime.MinValue ? fertilityStartDay : fertilityStartDay = OvulationPeakDay.AddDays(-2);
            }
            set
            {
                if (value != DateTime.MinValue)                  
                    fertilityStartDay = value;
            }
        }

        public int FertilityDuration { get { return 5; } }

       

       
    }
}
