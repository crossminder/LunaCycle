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
            this.OvulationDay = PeriodStartDay.AddDays(CycleDuration / 2);
            this.FertilityStartDay = OvulationDay.AddDays(-FertilityDuration);

        }

        private DateTime periodEndDay;
        public DateTime PeriodEndDay
        {
            get
            {
                DateTime start = PeriodStartDay;
                if (periodEndDay == DateTime.MinValue && start != null && start != DateTime.MinValue)
                    periodEndDay = start.AddDays(PeriodDuration - 1);
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
                DateTime start = PeriodStartDay;
                if (cycleEndDay == DateTime.MinValue && start != null && start != DateTime.MinValue)
                    cycleEndDay = start.AddDays(CycleDuration - 1);
                return cycleEndDay;
            }
            set
            {
                if (value != DateTime.MinValue)                  
                    cycleEndDay = value;
            }
        }


        private DateTime ovulationDay;
        public DateTime OvulationDay
        {
            get
            {
                DateTime start = PeriodStartDay;
                if (ovulationDay == DateTime.MinValue && start != null && start != DateTime.MinValue)
                    ovulationDay = start.AddDays(CycleDuration / 2);
                return ovulationDay;
            }
            set
            {
                if (DateTime.MinValue != value)
                    ovulationDay = value;
            }
        }


        private DateTime fertilityStartDay;
        public DateTime FertilityStartDay
        {
            get
            {
                DateTime ovulation = OvulationDay;
                if (fertilityStartDay == DateTime.MinValue && ovulation != null && ovulation != DateTime.MinValue)
                    fertilityStartDay = ovulation.AddDays(-FertilityDuration);
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
