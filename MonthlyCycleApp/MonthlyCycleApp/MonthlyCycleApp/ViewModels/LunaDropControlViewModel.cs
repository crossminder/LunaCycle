using MonthlyCycleApp.Helpers;
using MonthlyCycleApp.Model;
using MonthlyCycleApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPControls.Models;

namespace MonthlyCycleApp.ViewModels
{
    public class LunaDropControlViewModel : INotifyPropertyChanged
    {

        private const double MAX_HEIGHT = 385;
        private double top = 0;
        private double bottom = -303;


        #region Event handlers
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Properties
        private bool? startCycleConfirmed;
        public bool StartCycleConfirmed
        {
            get
            {
                return startCycleConfirmed.HasValue ? startCycleConfirmed.Value : ApplicationSettings.GetProperty<bool>(ApplicationSettings.IS_CYCLE_START_CONFIRMED); ;
            }
            set
            {
                if (value != startCycleConfirmed)
                {
                    startCycleConfirmed = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.IS_CYCLE_START_CONFIRMED, startCycleConfirmed);
                    NotifyPropertyChanged("StartCycleConfirmed");
                }
            }
        }

        private bool? endCycleConfirmed;
        public bool EndCycleConfirmed
        {
            get
            {
                return endCycleConfirmed.HasValue ? endCycleConfirmed.Value : ApplicationSettings.GetProperty<bool>(ApplicationSettings.IS_CYCLE_END_CONFIRMED); ;
            }
            set
            {
                if (value != endCycleConfirmed)
                {
                    endCycleConfirmed = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.IS_CYCLE_END_CONFIRMED, endCycleConfirmed);
                    NotifyPropertyChanged("EndCycleConfirmed");
                }
            }
        }

        #endregion

        #region Drop infos
        public string Today
        {
            get
            {
                return DateTime.Now.ToString("m");
            }
        }

        private int daysToPeriod;
        public int DaysToPeriod
        {
            get
            {
                return daysToPeriod;
            }
            set
            {
                if (value != daysToPeriod)
                {
                    daysToPeriod = value;
                    NotifyPropertyChanged("DaysToPeriod");
                }
            }
        }

        private string daysToPeriodText;
        public string DaysToPeriodText
        {
            get { return daysToPeriodText; }
            set
            {
                if (value != daysToPeriodText)
                {
                    daysToPeriodText = value;
                    NotifyPropertyChanged("DaysToPeriodText");
                }
            }
        }

        public Visibility IsSetupCompleted
        {
            get
            {
                //do some computing
                return System.Windows.Visibility.Visible;

            }
        }

        private double topMarginValue;
        public double TopMarginValue 
        {
            get { return topMarginValue; }
            set
            {
                if (value != topMarginValue)
                {
                    topMarginValue = value;
                    NotifyPropertyChanged("TopMarginValue");
                }
            }
        }
        
        private double bottomMarginValue;
        public double BottomMarginValue
        {
            get { return bottomMarginValue; }
            set
            {
                if (value != bottomMarginValue)
                {
                    bottomMarginValue = value;
                    NotifyPropertyChanged("BottomMarginValue");
                }
            }
        }

        private Thickness wavingGridMargins;
        public Thickness WavingGridMargins
        {
            get { return wavingGridMargins; }
            set
            {
                if (value != wavingGridMargins)
                {
                    wavingGridMargins = value;
                    NotifyPropertyChanged("WavingGridMargins");
                }
            }
        }


        #endregion

        public LunaDropControlViewModel()
        {
            App.MainViewModel.ShowSelectStartDay = !StartCycleConfirmed;
            App.MainViewModel.ShowSelectEndDay = !EndCycleConfirmed;
      
            var currentPeriod = App.MainViewModel.NextPeriod;
            SetDropValues();
        }


        public void SetupControlsVisibility(bool showCycleConfirmation, bool startCycleConfimed, bool endCycleConfirmed, bool needToAddCycleManually)
        {
            StartCycleConfirmed = startCycleConfimed;
            EndCycleConfirmed = endCycleConfirmed;
        }

        public void SetDropValues()
        {
            var nextPeriod = App.MainViewModel.NextPeriod;

            if(DateTime.Today < nextPeriod.CycleStartDay)
            {
                int remainingDays = ((TimeSpan)(nextPeriod.CycleStartDay - DateTime.Today)).Days;
     
                DaysToPeriodText = AppResources.DaysToPeriodText;
                DaysToPeriod = Math.Abs( remainingDays);
            }
            else
                if (DateTime.Today >= nextPeriod.CycleStartDay && DateTime.Today <= nextPeriod.CycleEndDay)
                {
                    int daysUntilEndCycle = ((TimeSpan)(nextPeriod.CycleEndDay - DateTime.Today)).Days + 1;
                    DaysToPeriodText = AppResources.DayOfPeriodText;
                    DaysToPeriod = Math.Abs(daysUntilEndCycle);
                }

            SetWaveHeigth(nextPeriod);

            ClearCache();
        }


        private void SetWaveHeigth(PeriodMonth currentPeriod)
        {
            double top = 0;
            //from begining to half of cycle you have 100%
            if (DateTime.Today >= currentPeriod.CycleStartDay &&
                DateTime.Today <= currentPeriod.CycleStartDay.AddDays(currentPeriod.CycleDuration/2))
                top = 0;
            else
                //from half cycle to end, decreasing
                if (DateTime.Today > currentPeriod.CycleStartDay.AddDays(currentPeriod.CycleDuration / 2) &&
                    DateTime.Today <= currentPeriod.CycleEndDay)
                {
                    double totalDaysSpan = (currentPeriod.CycleEndDay - currentPeriod.CycleStartDay.AddDays(currentPeriod.CycleDuration / 2)).Days;
                    double percentage = Math.Round(MAX_HEIGHT / totalDaysSpan);
                    double remaining = (currentPeriod.CycleEndDay - DateTime.Today).Days +1;
                    top = - percentage * remaining;
                }
                else
                //half duration to start cycle, increasing
                {
                    double totalDaysSpan = currentPeriod.CycleDuration / 2;
                    if (DaysToPeriod < totalDaysSpan &&
                         DateTime.Today < currentPeriod.CycleStartDay)
                    { 
                        double percentage = Math.Round(MAX_HEIGHT / totalDaysSpan);
                        double remaining = (DateTime.Today- currentPeriod.CycleStartDay).Days + 1;
                        top = -percentage * (totalDaysSpan - remaining);
                    }
                }

            double bottom = -303 - top;

            TopMarginValue = top;
            BottomMarginValue = bottom;

            WavingGridMargins = new Thickness(-40, top, 40, bottom);

         
        }

        private void ClearCache()
        {
            if (StartCycleConfirmed && EndCycleConfirmed)
            {
                ApplicationSettings.RemoveProperty(ApplicationSettings.IS_CYCLE_START_CONFIRMED);
                ApplicationSettings.RemoveProperty(ApplicationSettings.IS_CYCLE_END_CONFIRMED);
            }
        }


    }
}
