using MonthlyCycleApp.Helpers;
using MonthlyCycleApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyCycleApp.ViewModels
{
    public class SetupViewModel :INotifyPropertyChanged
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        #region Persisted settings
        private string cycleDuration;
        public string CycleDuration
        {
            get
            {
                return string.IsNullOrWhiteSpace(cycleDuration) ? cycleDuration : ApplicationSettings.GetProperty<string>(ApplicationSettings.CYCLE_DURATION_SETTING);
            }
            set
            {
                if (value != cycleDuration)
                {
                    cycleDuration = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.CYCLE_DURATION_SETTING, cycleDuration);
                    NotifyPropertyChanged("CycleDuration");
                } 
            }
        }

        private string periodDuration;
        public string PeriodDuration
        {
            get
            {
                return string.IsNullOrWhiteSpace(periodDuration) ? periodDuration : ApplicationSettings.GetProperty<string>(ApplicationSettings.PERIOD_DURATION_SETTING);
            }
            set
            {
                if (value != periodDuration)
                {
                    periodDuration = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.PERIOD_DURATION_SETTING, periodDuration);
                    NotifyPropertyChanged("PeriodDuration");
                }
            }
        }

        private DateTime lastPeriodDate;
        public DateTime LastPeriodDate
        { 
            get
            {
                return lastPeriodDate != DateTime.MinValue ? lastPeriodDate : ApplicationSettings.GetProperty<DateTime>(ApplicationSettings.LAST_PERIOD_SETTING);
            }
            set
            {
                if (value != lastPeriodDate)
                {
                    lastPeriodDate = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.LAST_PERIOD_SETTING, lastPeriodDate);
                }
            } }

        private bool? showInitialSetup = null;
        public bool ShowInitialSetup
        {
            get
            {
                return showInitialSetup.HasValue ? showInitialSetup.Value : ApplicationSettings.GetProperty<bool>(ApplicationSettings.SHOW_SETUP);

            }
            set
            {
                if (value != showInitialSetup)
                {
                    showInitialSetup = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.SHOW_SETUP, showInitialSetup);
                }
            }
        }

        #endregion

        private bool setupCompleted = false;
        public bool SetupCompleted
        {
            get
            {
                return setupCompleted;
            }
            set
            {
                setupCompleted = value;
               

            }
        }

        private bool lastDateSelected;
        public bool LastDateSelected
        {
            get 
            {
                return lastDateSelected;
            }
            set 
            {
                lastDateSelected = value;
                NotifyPropertyChanged("LastDateSelected");
            }
        }

        private string endSetupButtonText = AppResources.SetupIncompletedBtnText;
        public string EndSetupButtonText
        {
            get
            {
                return endSetupButtonText;
            }
            set
            {
                endSetupButtonText = value;
            }
        }

        #endregion

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                RefreshButton();
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
               
            }
        }

        private void RefreshButton()
        {
            SetupCompleted = !string.IsNullOrWhiteSpace(CycleDuration) &&
                         !string.IsNullOrWhiteSpace(PeriodDuration) &&
                         !CycleDuration.Equals("0") &&
                         !PeriodDuration.Equals("0")
                         && lastDateSelected;

            PropertyChanged(this,
                   new PropertyChangedEventArgs("SetupCompleted"));
            EndSetupButtonText = SetupCompleted ? AppResources.SetupCompletedBtnText : AppResources.SetupIncompletedBtnText;
            PropertyChanged(this,
                  new PropertyChangedEventArgs("EndSetupButtonText"));
        }

        public SetupViewModel()
        {
            PeriodDuration = "6";
            CycleDuration = "28";
            LastDateSelected = false;
            LastPeriodDate = DateTime.Today;
        }
    }
}
