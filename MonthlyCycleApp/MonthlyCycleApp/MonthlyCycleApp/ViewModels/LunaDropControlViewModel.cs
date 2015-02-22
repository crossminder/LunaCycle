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

namespace MonthlyCycleApp.ViewModels
{
    public class LunaDropControlViewModel : INotifyPropertyChanged
    {
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

        private string daysToPeriod;
        public string DaysToPeriod
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

        #endregion

        public LunaDropControlViewModel()
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            SetDropValues();
        }


        public void SetupControlsVisibility(bool showCycleConfirmation, bool startCycleConfimed, bool endCycleConfirmed, bool needToAddCycleManually)
        {
            StartCycleConfirmed = startCycleConfimed;
            EndCycleConfirmed = endCycleConfirmed;
        }

        public void SetDropValues()
        {
            var currentPeriod = App.MainViewModel.Calendar.NextPeriod;

            if(DateTime.Today < currentPeriod.CycleStartDay)
            {
                int remainingDays = ((TimeSpan)(currentPeriod.CycleStartDay - DateTime.Today)).Days;
     
                DaysToPeriodText = AppResources.DaysToPeriodText;
                DaysToPeriod = Math.Abs( remainingDays).ToString();
            }
            else
                if (DateTime.Today >= currentPeriod.CycleStartDay && DateTime.Today <= currentPeriod.CycleEndDay)
                {
                    int daysIntoCycle = ((TimeSpan)(currentPeriod.CycleEndDay - DateTime.Today)).Days + 1;
                    DaysToPeriodText = AppResources.DayOfPeriodText;
                    DaysToPeriod = Math.Abs(daysIntoCycle).ToString();
                }

            ClearCache();
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
