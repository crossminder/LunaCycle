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

            this.PeriodEndDay = PeriodStartDay.AddDays(PeriodDuration - 1);
            this.CycleEndDay = PeriodStartDay.AddDays(CycleDuration - 1);
            this.OvulationPeakDay = PeriodStartDay.AddDays(CycleDuration / 2);
            this.FertilityStartDay = OvulationPeakDay.AddDays(-2);

        }

        private DateTime periodEndDay;
        public DateTime PeriodEndDay
        {
            get
            {
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
                return ovulationPeakDay;
            }
            set
            {
                if (DateTime.MinValue != value)
                    ovulationPeakDay = value;
            }
        }


        private DateTime fertilityStartDay;
        public DateTime FertilityStartDay
        {
            get
            {
                return fertilityStartDay;
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
