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
        private PeriodDay _today;
        #endregion

        #region const

        private const int defaultAveragePeriod = 28;
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
                    currentPeriod = PastPeriods.OrderByDescending(x => x.PeriodStartDay).FirstOrDefault();
                        //(from period in PastPeriods
                        //              where period.PeriodStartDay.Month == DateTime.Today.Month
                        //              select period).FirstOrDefault();


                if (currentPeriod.IsEmpty() && PastPeriods != null && PastPeriods.Count > 0)
                {
                    currentPeriod.CycleDuration = AverageCycleDuration;
                    currentPeriod.PeriodDuration = AveragePeriodDuration;
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
   
        public int AverageCycleDuration 
        {
            get 
            {
                if (averageCycleDuration == 0)
                {
                    if (PastPeriods != null && PastPeriods.Count > 0)
                    {
                        //pick the last year's entries
                        var past = PastPeriods;
                       
                        List<int> values = new List<int>();
                        if (past.Count >= 12)
                            values = past
                                        .OrderByDescending(x => x.PeriodStartDay.Month)
                                        .Where(x => x.PeriodStartDay.Month >= DateTime.Today.AddMonths(-12).Month)
                                        .Select(x => x.CycleDuration).ToList();
                        else
                            values = past.Select(x => x.CycleDuration).ToList();

                        var minValue = 21;
                        var maxValue = 45;

                        values = values.Where(x => x > minValue && x < maxValue).ToList();
                        double arithmethicMean = Math.Round(values.Average());

                        averageCycleDuration = (Int32)arithmethicMean;
                    }
                    else
                        averageCycleDuration = defaultAverageCycle;
                }

                return averageCycleDuration;
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
                        var past = PastPeriods;
                        List<int> values = new List<int>();

                        //pick the last year's entries
                        if (past.Count >= 12)
                            values = past
                                        .OrderByDescending(x => x.PeriodStartDay.Month)
                                        .Where(x => x.PeriodStartDay.Month >= DateTime.Today.AddMonths(-12).Month)
                                        .Select(x => x.PeriodDuration).ToList();
                        else
                            values = past.Select(x => x.PeriodDuration).ToList();

                        //remove the most minimal and maximal values 
                        //remove the most minimal and maximal values 
                        var minValue = values.Min();
                        var maxValue = values.Max();

                        int count = values.Count;

                        //we aim to remove only the unusually long/short periods
                        //this means we shouldn't remove regular entries, 
                        //so we search for min+ max values that don't appear very often
                        if (minValue != maxValue &&
                            values.Count(x => x == minValue) < count / 4 &&
                            values.Count(x => x == maxValue) < count / 4)

                            values = values.Where(x => x != minValue || x != maxValue).ToList();

                        
                        double arithmethicMean = Math.Round(values.Average());

                        averagePeriodDuration = (Int32)arithmethicMean;
                    }
                    else
                        averagePeriodDuration = defaultAveragePeriod;
                }
                return averagePeriodDuration;
            }
        }

        public List<PeriodMonth> FuturePeriods
        {
            get
            {
                if (futurePeriods == null)
                    futurePeriods = new List<PeriodMonth>();
                if (futurePeriods.Count == 0)
                {
                    List<PeriodMonth> periods = new List<PeriodMonth>();
                    if (PastPeriods != null && PastPeriods.Count > 0)
                        periods = PastPeriods;
                    periods.Add(CurrentPeriod);


                    if (periods != null && periods.Count > 0)
                    {
                        DateTime lastMonthEndPeriodDay = periods.Last().CycleEndDay;

                        PeriodMonth estimatedFuture1 = new PeriodMonth()
                        {
                            PeriodDuration = AveragePeriodDuration,
                            CycleDuration = AverageCycleDuration,
                            PeriodStartDay = lastMonthEndPeriodDay.AddDays(1)
                        };

                        PeriodMonth estimatedFuture2 = new PeriodMonth()
                        {
                            PeriodDuration = AveragePeriodDuration,
                            CycleDuration = AverageCycleDuration,
                            PeriodStartDay = estimatedFuture1.CycleEndDay.AddDays(1)
                        };

                        PeriodMonth EstimatedFuture3 = new PeriodMonth()
                        {
                            PeriodDuration = AveragePeriodDuration,
                            CycleDuration = AverageCycleDuration,
                            PeriodStartDay = estimatedFuture2.CycleEndDay.AddDays(1)
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
                    if (CurrentPeriod != null && !PastPeriods.Contains(CurrentPeriod))
                        periods.Add(CurrentPeriod);
                    if (FuturePeriods != null && FuturePeriods.Count > 0)
                        periods = periods.Concat(FuturePeriods).ToList();
                }
                return periods;
            }
        }


        //public int FirstFertileDayPrediction
        //{
        //    get
        //    {
        //        int minCycleDuration = PastPeriods.Min(x => x.CycleDuration);
        //        int average = AverageCycleDuration / 2;
        //        return minCycleDuration > 18 ? minCycleDuration - 18 : average < 11 ? 11 : average;
        //    }
        //}

        //public int LastFertileDayPrediction
        //{
        //    get
        //    {
        //        int maxCycleDuration = PastPeriods.Max(x => x.CycleDuration);
        //        int average = AverageCycleDuration / 2;
        //        return maxCycleDuration > 11 ? maxCycleDuration - 11 : average > 21 ? 21 : average;
        //    }
        //}
            
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
       
        #endregion

        public PeriodMonth GetPeriodForDate(DateTime date)
        {
            return Periods.Where(item => date >= item.PeriodStartDay && date <= item.CycleEndDay).FirstOrDefault();
        }

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
