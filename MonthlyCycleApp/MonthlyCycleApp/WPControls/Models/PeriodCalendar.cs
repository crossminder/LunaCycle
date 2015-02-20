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
        //[DataMember]
        //public int Year
        //{
        //    get;
        //    set;
        //}

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
        private PeriodMonth _currentPeriod;
        private List<PeriodMonth> _pastPeriods = new List<PeriodMonth>();
        private int _averagePeriodDuration;
        private int _averageCycleDuration;
        private List<PeriodMonth> _futurePeriods;
        private PeriodMonth _nextPeriod;
        private PeriodDay _today;
        #endregion

        #region const

        private const int _defaultAveragePeriod = 28;
        private const int _defaultAverageCycle = 6;

        #endregion

        #region Properties
        public PeriodMonth CurrentPeriod
        {
            get
            {
                if (_currentPeriod != null) return _currentPeriod;


                _currentPeriod = new PeriodMonth();
                if (PastPeriods != null && PastPeriods.Count > 0)
                    _currentPeriod = (from period in PastPeriods
                                      where period.CycleStartDay.Month == DateTime.Today.Month
                                      select period).FirstOrDefault();


                if (_currentPeriod.IsEmpty() && PastPeriods != null && PastPeriods.Count > 0)
                {
                    _currentPeriod.PeriodDuration = AveragePeriodDuration;
                    _currentPeriod.CycleDuration = AverageCycleDuration;
                }


                return _currentPeriod != null ? _currentPeriod : new PeriodMonth();
            }
            set
            {
                if (value != null)
                    _currentPeriod = value;
            }
        }
   
        public int AveragePeriodDuration 
        {
            get 
            {
                if (_averagePeriodDuration == 0)
                {
                    if (PastPeriods != null && PastPeriods.Count > 0)
                    {
                        var pastPeriodsList = (from month in PastPeriods.OrderBy(x => x.CycleStartDay.Month)
                                               where month.CycleStartDay.Month <= DateTime.Today.AddMonths(-3).Month
                                               select month).ToList();
                        _averagePeriodDuration = pastPeriodsList.Sum(x => x.PeriodDuration) / pastPeriodsList.Count();
                    }
                    else
                        _averagePeriodDuration = _defaultAveragePeriod;
                }

                return _averagePeriodDuration;
            }
        }
       
        public int AverageCycleDuration
        {
            get
            {
                if (_averagePeriodDuration == 0)
                {
                    if (PastPeriods != null && PastPeriods.Count > 0)
                    {
                        var pastPeriodsList = (from month in PastPeriods.OrderBy(x => x.CycleStartDay.Month)
                                               where month.CycleStartDay.Month <= DateTime.Today.AddMonths(-3).Month
                                               select month).ToList();
                        _averageCycleDuration = pastPeriodsList.Sum(x => x.CycleDuration) / pastPeriodsList.Count();
                    }
                    else
                        _averageCycleDuration = _defaultAverageCycle;
                }
                return _averageCycleDuration;
            }
        }

        public List<PeriodMonth> FuturePeriods
        {
            get
            {
                if (_futurePeriods == null)
                    _futurePeriods = new List<PeriodMonth>();
                if(_futurePeriods.Count ==0)
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

                        _futurePeriods.Add(estimatedFuture1);
                        _futurePeriods.Add(estimatedFuture2);
                        _futurePeriods.Add(EstimatedFuture3);
                    }
                }
                return _futurePeriods;
            }
        }

        public List<PeriodMonth> _periods;
        public List<PeriodMonth> Periods
        {
            get
            {
                if (_periods == null)
                    _periods = new List<PeriodMonth>();
                if (_periods.Count == 0)
                {
                    if (PastPeriods != null && PastPeriods.Count > 0)
                        _periods = (from item in PastPeriods
                                    select item).ToList();
                    if (FuturePeriods != null && FuturePeriods.Count > 0)
                        _periods = _periods.Concat(FuturePeriods).ToList();
                }
                return _periods;
            }
        }

        public PeriodMonth NextPeriod
        {
            get
            {
                if (_nextPeriod != null)
                    return _nextPeriod;
                _nextPeriod = new PeriodMonth();
                if (CurrentPeriod != null && DateTime.Today < CurrentPeriod.CycleStartDay)
                    _nextPeriod = _currentPeriod;
                if (CurrentPeriod != null && FuturePeriods != null && FuturePeriods.Count > 0 && DateTime.Today >= CurrentPeriod.CycleStartDay)
                    _nextPeriod = FuturePeriods.FirstOrDefault();

                return _nextPeriod;

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
