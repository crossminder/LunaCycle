using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WPControls.Models
{
    public class PeriodMonth
    {
        public DateTime CycleStartDay { get; set; }
        public int CycleDuration { get; set; }
        public int PeriodDuration { get; set; }


        public PeriodMonth() { }

        public PeriodMonth(DateTime cycleStartDay, int cycleDuration, int periodDuration)
        {
            this.PeriodDuration = periodDuration;
            this.CycleDuration = cycleDuration;
            this.CycleStartDay = cycleStartDay;
            /*  this._cycleEndDay = CycleStartDay + CycleEndDay;
              this._periodEndDay = CycleStartDay + PeriodDuration;
              this._fertilityStartDay = CycleDuration / 2 + CycleStartDay; */
        }


        private DateTime _cycleEndDay;
        public DateTime CycleEndDay
        {
            get
            {
                return _cycleEndDay != DateTime.MinValue ? _cycleEndDay : _cycleEndDay = CycleStartDay.AddDays(CycleDuration);
            }
            set
            {
                if (value != DateTime.MinValue)
                    _cycleEndDay = value;
            }
        }

        private DateTime _periodEndDay;
        public DateTime PeriodEndDay
        {
            get
            {
                if (_periodEndDay == DateTime.MinValue)
                    _periodEndDay = CycleStartDay.AddDays(PeriodDuration);
                return _periodEndDay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    _periodEndDay = value;
            }
        }

        private DateTime _fertilityStartDay;
        public DateTime FertilityStartDay
        {
            get
            {
                return _fertilityStartDay != DateTime.MinValue ? _fertilityStartDay : _fertilityStartDay = CycleStartDay.AddDays(CycleDuration / 2);
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
