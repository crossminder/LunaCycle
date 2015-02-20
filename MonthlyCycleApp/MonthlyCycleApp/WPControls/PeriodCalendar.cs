using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WPControls;

namespace WPControls.Models
{
    public class PeriodCalendar
    {
        public int Year
        {
            get;
            set;
        }
        public List<PeriodMonth> PastPeriods
        {
            get;
            set;
        }

        private PeriodMonth _currentPeriod;
        public PeriodMonth CurrentPeriod
        {
            get
            {
                if (_currentPeriod != null) return _currentPeriod;


                _currentPeriod = new PeriodMonth();
                if (Year != 0 && PastPeriods != null && PastPeriods.Count > 0)
                    _currentPeriod = (from period in PastPeriods
                                      where period.CycleStartDay.Month == DateTime.Today.Month
                                      select period).FirstOrDefault();
                else
                {

                    int averagePastThreeMonthCycleDuration = (from month in PastPeriods.OrderBy(x => x.CycleStartDay.Month)
                                                              where month.CycleStartDay.Month <= DateTime.Today.Month - 3
                                                              select month.CycleDuration).Sum() / 3;
                    int averagePastThreeMonthPeriodDuration = (from month in PastPeriods.OrderBy(x => x.CycleStartDay.Month)
                                                               where month.CycleStartDay.Month <= DateTime.Today.Month - 3
                                                               select month.PeriodDuration).Sum() / 3;
                    DateTime lastMonthEndPeriodDay = PastPeriods.Last().PeriodEndDay;

                    _currentPeriod.PeriodDuration = averagePastThreeMonthPeriodDuration;
                    _currentPeriod.CycleDuration = averagePastThreeMonthCycleDuration;
                    //_currentPeriod.CycleStartDay = lastMonthEndPeriodDay + 1;
                    //_currentPeriod.CycleEndDay = _currentPeriod.CycleStartDay + averagePastThreeMonthCycleDuration;
                    //_currentPeriod.PeriodEndDay = _currentPeriod.CycleStartDay + averagePastThreeMonthPeriodDuration;
                }


                return _currentPeriod != null ? _currentPeriod : new PeriodMonth();
            }
            set
            {
                if (value != null)
                    _currentPeriod = value;
            }
        }

        public List<PeriodMonth> FuturePeriods
        {
            get;
            set;
        }
    }
}
