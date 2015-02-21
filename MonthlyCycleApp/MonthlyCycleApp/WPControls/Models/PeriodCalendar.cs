using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WPControls;
using WPControls.Helpers;

namespace WPControls.Models
{
    [DataContract]
    public class PeriodCalendar:INotifyPropertyChanged
    {

        #region Serializable
        [DataMember]
        public List<PeriodMonth> PastPeriods
        {
            get
            {
                return _pastPeriods;
            }
            set
            {
                if (value != null)
                    _pastPeriods = value;
                else
                    _pastPeriods = new List<PeriodMonth>();
                OnPropertyChanged("PastPeriods");
            }
        }

        #endregion

        #region private
        private PeriodMonth currentPeriod;
        private List<PeriodMonth> _pastPeriods = new List<PeriodMonth>();
        private int averagePeriodDuration;
        private int averageCycleDuration;
        private List<PeriodMonth> futurePeriods;
        private PeriodMonth nextPeriod;
        private PeriodDay _today;
        #endregion

        #region const

        private const int _defaultAveragePeriod = 28;
        private const int defaultAverageCycle = 6;

        #endregion

        #region Properties
        public PeriodMonth CurrentPeriod
        {
            get
            {
                if (currentPeriod != null) return currentPeriod;

                currentPeriod = new PeriodMonth();
                if (PastPeriods != null && PastPeriods.Count > 0)
                    currentPeriod = (from period in PastPeriods
                                      where period.CycleStartDay.Month == DateTime.Today.Month
                                      select period).FirstOrDefault();


                if (currentPeriod.IsEmpty() && PastPeriods != null && PastPeriods.Count > 0)
                {
                    currentPeriod.PeriodDuration = AveragePeriodDuration;
                    currentPeriod.CycleDuration = AverageCycleDuration;
                }

                return currentPeriod != null ? currentPeriod : new PeriodMonth();
            }
            set
            {
                if (value != null)
                {
                    currentPeriod = value;
                    OnPropertyChanged("CurrentPeriod");
                }
            }
        }
   
        public int AveragePeriodDuration 
        {
            get 
            {
                if (averagePeriodDuration == 0)
                {
                    if (PastPeriods != null && PastPeriods.Count > 0)
                    {
                        var pastPeriodsList = (from month in PastPeriods.OrderBy(x => x.CycleStartDay.Month)
                                               where month.CycleStartDay.Month <= DateTime.Today.AddMonths(-3).Month
                                               select month).ToList();
                        averagePeriodDuration = pastPeriodsList.Sum(x => x.PeriodDuration) / pastPeriodsList.Count();
                    }
                    else
                        averagePeriodDuration = _defaultAveragePeriod;
                }

                return averagePeriodDuration;
            }
        }
       
        public int AverageCycleDuration
        {
            get
            {
                if (averagePeriodDuration == 0)
                {
                    if (PastPeriods != null && PastPeriods.Count > 0)
                    {
                        var pastPeriodsList = (from month in PastPeriods.OrderBy(x => x.CycleStartDay.Month)
                                               where month.CycleStartDay.Month <= DateTime.Today.AddMonths(-3).Month
                                               select month).ToList();
                        averageCycleDuration = pastPeriodsList.Sum(x => x.CycleDuration) / pastPeriodsList.Count();
                    }
                    else
                        averageCycleDuration = defaultAverageCycle;
                }
                return averageCycleDuration;
            }
        }

        public List<PeriodMonth> FuturePeriods
        {
            get
            {
                if (futurePeriods == null)
                    futurePeriods = new List<PeriodMonth>();
                if(futurePeriods.Count ==0)
                {
                    if (PastPeriods != null && PastPeriods.Count > 0)
                    {
                        DateTime lastMonthEndPeriodDay = PastPeriods.Last().PeriodEndDay;

                        PeriodMonth estimatedFuture1 = new PeriodMonth()
                        {
                            CycleDuration = AverageCycleDuration,
                            PeriodDuration = AveragePeriodDuration,
                            CycleStartDay = lastMonthEndPeriodDay.AddDays(1)
                        };

                        PeriodMonth estimatedFuture2 = new PeriodMonth()
                        {
                            CycleDuration = AverageCycleDuration,
                            PeriodDuration = AveragePeriodDuration,
                            CycleStartDay = estimatedFuture1.PeriodEndDay.AddDays(1)
                        };

                        PeriodMonth EstimatedFuture3 = new PeriodMonth()
                        {
                            CycleDuration = AverageCycleDuration,
                            PeriodDuration = AveragePeriodDuration,
                            CycleStartDay = estimatedFuture2.PeriodEndDay.AddDays(1)
                        };

                        futurePeriods.Add(estimatedFuture1);
                        futurePeriods.Add(estimatedFuture2);
                        futurePeriods.Add(EstimatedFuture3);
                    }
                }
                return futurePeriods;
            }
        }

        public List<PeriodMonth> periods;
        public List<PeriodMonth> Periods
        {
            get
            {
                if (periods == null)
                    periods = new List<PeriodMonth>();
                if (periods.Count == 0)
                {
                    if (PastPeriods != null && PastPeriods.Count > 0)
                        periods = (from item in PastPeriods
                                    select item).ToList();
                    if (FuturePeriods != null && FuturePeriods.Count > 0)
                        periods = periods.Concat(FuturePeriods).ToList();
                }
                return periods;
            }
        }

        public PeriodMonth NextPeriod
        {
            get
            {

               var period = new PeriodMonth();
                if (CurrentPeriod != null && DateTime.Today < CurrentPeriod.CycleEndDay)
                    period = currentPeriod;
                if (CurrentPeriod != null && FuturePeriods != null && FuturePeriods.Count > 0 && DateTime.Today >= CurrentPeriod.CycleEndDay)
                    period = FuturePeriods.FirstOrDefault();

                return period;

            }
        }
       
        public PeriodDay Today
        {
            get
            {
                if (_today != null)
                    return _today;
                _today = new PeriodDay(GetPeriodForDate(DateTime.Today));
                return _today;
            }
        }


        public PeriodMonth GetPeriodForDate(DateTime date)
        {
            return Periods.Where(item => date >= item.CycleStartDay && date <= item.PeriodEndDay).FirstOrDefault();
        }
        #endregion

        #region event handlers
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

      
    }
}
