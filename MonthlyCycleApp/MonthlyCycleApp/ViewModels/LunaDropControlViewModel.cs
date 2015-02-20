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

        private bool? showCycleConfirmation;
        public bool ShowCycleConfirmation
        {
            get
            {
                var persisted = ApplicationSettings.GetProperty<bool>(ApplicationSettings.SHOW_CYCLE_CONFIRMATION);
                return showCycleConfirmation.HasValue ? showCycleConfirmation.Value : persisted;
            }
            set
            {
                if (value != showCycleConfirmation)
                {
                    showCycleConfirmation = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.SHOW_CYCLE_CONFIRMATION, showCycleConfirmation);
                    NotifyPropertyChanged("ShowCycleConfirmation");
                }
            }
        }

        private bool? needToAddCycleMannually;
        public bool NeedToAddCycleMannually
        {
            get
            {
                var persisted = ApplicationSettings.GetProperty<bool>(ApplicationSettings.NEED_TO_ADD_CYCLE_MANUALLY);
                return needToAddCycleMannually.HasValue ? needToAddCycleMannually.Value : persisted;

            }
            set
            {
                if (value != needToAddCycleMannually)
                {
                    needToAddCycleMannually = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.NEED_TO_ADD_CYCLE_MANUALLY, needToAddCycleMannually);
                    NotifyPropertyChanged("NeedToAddCycleMannually");
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
            set { daysToPeriod = value; }
        }

        private string daysToPeriodText;
        public string DaysToPeriodText
        {
            get { return daysToPeriodText; }
            set
            {
                if (value != daysToPeriodText)
                    daysToPeriodText = value;
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
            ShowCycleConfirmation = showCycleConfirmation;
            StartCycleConfirmed = startCycleConfimed;
            EndCycleConfirmed = endCycleConfirmed;
            NeedToAddCycleMannually = needToAddCycleManually;
        }

        public void SetDropValues()
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            int remainingDays = ((TimeSpan)(currentPeriod.CycleStartDay - DateTime.Today)).Days;
            if (remainingDays > 0)
            {
                DaysToPeriodText = AppResources.DaysToPeriodText;
                DaysToPeriod = remainingDays.ToString();
            }
            else
            {
                int daysIntoCycle = ((TimeSpan)(DateTime.Today - currentPeriod.CycleStartDay)).Days + 1;
                DaysToPeriodText = AppResources.DayOfPeriodText;
                DaysToPeriod = daysIntoCycle.ToString();
            }
        }


    }
}
