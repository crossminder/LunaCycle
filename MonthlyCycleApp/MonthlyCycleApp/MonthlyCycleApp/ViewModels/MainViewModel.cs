using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MonthlyCycleApp.Resources;
using WPControls.Models;
using WPControls.Helpers;
using Monthly.Helpers;
using MonthlyCycleApp.Helpers;
using System.Globalization;
using System.Collections.Generic;
using MonthlyCycleApp.Model;
using System.Windows;

namespace MonthlyCycleApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
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

        public bool EndOfCycleEnabled
        {
            get
            {
                DateTime startCycle = Calendar.CurrentPeriod.CycleStartDay;
                return startCycle == DateTime.MinValue ? false : startCycle <= DateTime.Today.AddDays(-2) ? true : false;
            }
        }

        private PeriodCalendar _calendar;
        public PeriodCalendar Calendar
        {

            get
            {
                if (_calendar == null || _calendar.IsNew())
                    _calendar = PersistanceStorage.MockCalendar();
                //PersistanceStorage.ReadDataFromPersistanceStorage();
                return _calendar;
            }
            set
            {
                if (value != _calendar && !value.IsNew())
                {
                    _calendar = value;
                }
            }
        }

        public List<DayOfWeek> DaysOfWeek = new List<DayOfWeek>() { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };


        #endregion

        #region Initial Settings

        public string PeriodDurationSetting
        {
            get { return ApplicationSettings.GetProperty<string>(ApplicationSettings.PERIOD_DURATION_SETTING); }
        }

        public string CycleDurationSetting
        {
            get { return ApplicationSettings.GetProperty<string>(ApplicationSettings.CYCLE_DURATION_SETTING); }
        }

        public string LastPeriodDate
        {
            get { return ApplicationSettings.GetProperty<DateTime>(ApplicationSettings.LAST_PERIOD_SETTING).ToString("dd MMM"); }
        }


        #endregion

        #region Settings - Notification section
       
        private bool? isPillAllarmOn;
        public bool IsPillAllarmOn
        {
            get
            {
                return isPillAllarmOn.HasValue ? isPillAllarmOn.Value : ApplicationSettings.GetProperty<bool>(ApplicationSettings.IS_PILL_ALARM_ON);
            }
            set
            {
                if (value != isPillAllarmOn)
                {
                    isPillAllarmOn = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.IS_PILL_ALARM_ON, isPillAllarmOn);
                    NotifyPropertyChanged("IsPillAllarmOn");
                }
            }
        }

        private DateTime? takePillHour;
        public DateTime TakePillHour
        {
            get
            {
                DateTime persisted = ApplicationSettings.GetProperty<DateTime>(ApplicationSettings.PILL_ALARM_TIME);
                return takePillHour.HasValue ? takePillHour.Value : persisted != default(DateTime) ? persisted : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            }
            set
            {
                if (value != takePillHour)
                {
                    takePillHour = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.PILL_ALARM_TIME, takePillHour);
                }
            }
        }

        private bool? isMenstruationAllarmOn;
        public bool IsMenstruationAllarmOn
        {
            get
            {
                return isMenstruationAllarmOn.HasValue ? isMenstruationAllarmOn.Value : ApplicationSettings.GetProperty<bool>(ApplicationSettings.IS_MENSTRUATION_ALARM_ON);
            }
            set
            {
                if (value != isMenstruationAllarmOn)
                {
                    isMenstruationAllarmOn = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.IS_MENSTRUATION_ALARM_ON, isMenstruationAllarmOn);
                    NotifyPropertyChanged("IsMenstruationAllarmOn");
                }
            }
        }

        private DateTime? menstruationAlarmHour;
        public DateTime MenstruationAlarmHour
        {
            get
            {
                DateTime persisted = ApplicationSettings.GetProperty<DateTime>(ApplicationSettings.MENSTRUATION_ALARM_TIME);
                return menstruationAlarmHour.HasValue ? menstruationAlarmHour.Value : persisted != default(DateTime) ? persisted : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            }
            set
            {
                if (value != menstruationAlarmHour)
                {
                    menstruationAlarmHour = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.MENSTRUATION_ALARM_TIME, menstruationAlarmHour);
                }
            }
        }

        private bool? isOvulationAllarmOn;
        public bool IsOvulationAllarmOn
        {
            get
            {
                return isOvulationAllarmOn.HasValue ? isOvulationAllarmOn.Value : ApplicationSettings.GetProperty<bool>(ApplicationSettings.IS_OVULATION_ALARM_ON);
            }
            set
            {
                if (value != isOvulationAllarmOn)
                {
                    isOvulationAllarmOn = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.IS_OVULATION_ALARM_ON, isOvulationAllarmOn);
                    NotifyPropertyChanged("IsOvulationAllarmOn");
                }
            }
        }

        private DateTime? ovulationAlarmHour;
        public DateTime OvulationAlarmHour
        {
            get
            {
                DateTime persisted = ApplicationSettings.GetProperty<DateTime>(ApplicationSettings.OVULATION_ALARM_TIME);
                return ovulationAlarmHour.HasValue ? ovulationAlarmHour.Value : persisted != default(DateTime) ? persisted : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            }
            set
            {
                if (value != ovulationAlarmHour)
                {
                    ovulationAlarmHour = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.OVULATION_ALARM_TIME, ovulationAlarmHour);
                }
            }
        }

        #endregion

        #region Settings - preferences

        private DayOfWeek? firstDayOfWeek;
        public DayOfWeek FirstDayOfWeek
        {
            get
            {
                CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                DayOfWeek firstDayCulture = cultureInfo.DateTimeFormat.FirstDayOfWeek;

                var storedValue = ApplicationSettings.GetPropertyWithDefault<DayOfWeek>(ApplicationSettings.FIRST_DAY_OF_WEEK, firstDayCulture);

                return firstDayOfWeek.HasValue ? firstDayOfWeek.Value : storedValue;
            }
            set
            {
                if (value != firstDayOfWeek)
                {
                    firstDayOfWeek = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.FIRST_DAY_OF_WEEK, firstDayOfWeek);
                    //  NotifyPropertyChanged("FirstDayOfWeek");
                }
            }
        }
 
        //public Forecast PeriodForecast
        //{
        //    get
        //    {
        //        return IsPeriodForecastAdvanced ? Forecast.Advanced : Forecast.Standard;
        //    }
        //}

        private bool? isPeriodForecastEnabled;
        public bool IsPeriodForecastEnabled
        {
            get
            {
                return isPeriodForecastEnabled.HasValue ? isPeriodForecastEnabled.Value : ApplicationSettings.GetProperty<bool>(ApplicationSettings.IS_PERIOD_FORECAST_ENABLED);
            }
            set
            {
                if (value != isPeriodForecastEnabled)
                {
                    isPeriodForecastEnabled = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.IS_PERIOD_FORECAST_ENABLED, isPeriodForecastEnabled);
                    NotifyPropertyChanged("IsPeriodForecastEnabled");
                    NotifyPropertyChanged("PeriodForecastExplanation");
                }
            }
        }

        public string PeriodForecastExplanation
        {
            get
            {
                return IsPeriodForecastEnabled ? AppResources.PeriodForecastExplanationAdvanced : AppResources.PeriodForecastExplanationStandard;
            }
        }

        private bool? isFertilityForecastEnabled;
        public bool IsFertilityForecastEnabled
        {
            get
            {
                return isFertilityForecastEnabled.HasValue ? isFertilityForecastEnabled.Value : ApplicationSettings.GetProperty<bool>(ApplicationSettings.IS_FERTILITY_FORECAST_ENABLED);
            }
            set
            {
                if (value != isFertilityForecastEnabled)
                {
                    isFertilityForecastEnabled = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.IS_FERTILITY_FORECAST_ENABLED, isFertilityForecastEnabled);
                    NotifyPropertyChanged("IsFertilityForecastEnabled");
                    NotifyPropertyChanged("FertilityForecastExplanation");
                }
            }
        }

        public string FertilityForecastExplanation
        {
            get
            {
                return IsFertilityForecastEnabled ? AppResources.FertilityForecastExplanationAdvanced : AppResources.FertilityForecastExplanationStandard;
            }
        }


        #endregion

        #region drop screens

        private string firstRowText;
        public string FirstRowText
        {
            get
            {
                return
                    firstRowText;
            }
            set
            {
                if (firstRowText != value)
                {
                    firstRowText = value;
                    NotifyPropertyChanged("FirstRowText");
                }
            }
        }

        private string secondRowText;
        public string SecondRowText
        {
            get
            {
                return
                    secondRowText;
            }
            set
            {
                if (secondRowText != value)
                {
                    secondRowText = value;
                    NotifyPropertyChanged("SecondRowText");
                }
            }
        }

        private string thirdRowText;
        public string ThirdRowText
        {
            get
            {
                return
                    thirdRowText;
            }
            set
            {
                if (thirdRowText != value)
                {
                    thirdRowText = value;
                    NotifyPropertyChanged("ThirdRowText");
                }
            }
        }

        private string forthRowText;
        public string ForthRowText
        {
            get
            {
                return
                    forthRowText;
            }
            set
            {
                if (forthRowText != value)
                {
                    forthRowText = value;
                    NotifyPropertyChanged("ForthRowText");
                }
            }
        }
        private bool? showDialog;
        public bool ShowDialog
        {
            get
            {
                return showDialog.HasValue ? showDialog.Value : false; 
            }
            set
            {
                if (showDialog != value)
                    showDialog = value;
                NotifyPropertyChanged("ShowDialog");
            } 
        }

        private bool? showSelectStartDay;
        public bool ShowSelectStartDay
        {
            get
            {
                return showSelectStartDay.HasValue ? showSelectStartDay.Value : false;
            }
            set
            {
                if (showSelectStartDay != value)
                    showSelectStartDay = value;
                NotifyPropertyChanged("ShowSelectStartDay");
            }
        }
        public bool Return { get; set; }

        private bool? showSelectEndDay;
        public bool ShowSelectEndDay
        {
            get
            {
                return showSelectEndDay.HasValue ? showSelectEndDay.Value : false;
            }
            set
            {
                if (showSelectEndDay != value)
                    showSelectEndDay = value;
                NotifyPropertyChanged("ShowSelectEndDay");
            }
        }

        private DateTime? selectedStartCycle;
        public DateTime SelectedStartCycle
        {
            get 
            {
                var storedValue = ApplicationSettings.GetProperty<DateTime>(ApplicationSettings.START_CYCLE_DATE);

                return selectedStartCycle.HasValue ? selectedStartCycle.Value : storedValue; 
            }
            set
            {
                if (value != selectedStartCycle)
                {
                    selectedStartCycle = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.START_CYCLE_DATE,selectedStartCycle);
                    NotifyPropertyChanged("SelectedStartCycle");
                }
            }
        }

        private DateTime? selectedEndCycle;
        public DateTime SelectedEndCycle
        {
            get
            {
                var storedValue = ApplicationSettings.GetProperty<DateTime>(ApplicationSettings.END_CYCLE_DATE);

                return selectedEndCycle.HasValue ? selectedEndCycle.Value : storedValue;
            }
            set
            {
                if (value != selectedEndCycle)
                {
                    selectedEndCycle = value;
                    ApplicationSettings.SetProperty(ApplicationSettings.END_CYCLE_DATE, selectedEndCycle);
                    NotifyPropertyChanged("SelectedEndCycle");
                }
            }
        }

        private int delayedAdvancedStart;
        public int DelayedAdvancedStart
        {
            get
            {
                return delayedAdvancedStart;
            }
            set
            {
                delayedAdvancedStart = value;
                NotifyPropertyChanged("DelayedAdvancedStart");
            }
        }

        private int delayedAdvancedEnd;
        public int DelayedAdvancedEnd
        {
            get
            {
                return delayedAdvancedEnd;
            }
            set
            {
                delayedAdvancedEnd = value;
                NotifyPropertyChanged("DelayedAdvancedEnd");
            }
        }

        #endregion

        public MainViewModel()
        {
            
        }

        #region Dialogs

        public void SetupAddPeriodDialog(DateTime endDate)
        {
            if (DateTime.Today < endDate)
            {
                SetupDialog(AppResources.PeriodStartedQuestion, string.Format(AppResources.PeriodLaterMessage,DelayedAdvancedStart), string.Empty, string.Empty, true, false);
            }
            else
            {
                SetupDialog(AppResources.PeriodStartedQuestion, string.Format(AppResources.PeriodLaterMessage, DelayedAdvancedStart), AppResources.DelayedPeriodEndQuestion, string.Format(AppResources.PeriodLaterMessage, DelayedAdvancedEnd), true, true);
            }
        }

        public  void SetupEndPeriodDialog(DateTime endDate)
        {
            //early end
            if (DateTime.Today < endDate)
            {
                ShowDialog = true;
                SetupDialog(string.Empty, string.Empty, AppResources.PeriodEndedQuestion, string.Format(AppResources.PeriodEarlierMessage, DelayedAdvancedEnd), false, true);
            }

            //late end
            if (DateTime.Today > endDate)
            {
                ShowDialog = true;
                SetupDialog(string.Empty, string.Empty, AppResources.PeriodEndedQuestion, string.Format(AppResources.PeriodLaterMessage, DelayedAdvancedEnd), false, true);
            }
        }

        public  void SetupStartPeriodDialog(DateTime startDate, DateTime endDate)
        {
            //early start
            if (DateTime.Today < startDate)
            {
                ShowDialog = true;
                SetupDialog(AppResources.PeriodStartedQuestion, string.Format(AppResources.PeriodEarlierMessage, DelayedAdvancedStart), string.Empty, string.Empty, true, false);
            }

            //later start
            if (DateTime.Today > startDate && DateTime.Today < endDate)
            {
                ShowDialog = true;
                SetupDialog(AppResources.PeriodStartedQuestion, string.Format(AppResources.PeriodLaterMessage, DelayedAdvancedStart), string.Empty, string.Empty, true, false);
            }
        }


        public void SetupDialog(string firstRowText, string secondRowText, string thirdRowText, string forthRowText, bool showSelectStartDay, bool showSelectEndDay)
        {
            FirstRowText = firstRowText;
            SecondRowText = secondRowText;
            ThirdRowText = thirdRowText;
            ForthRowText = forthRowText;
            ShowSelectStartDay = showSelectStartDay;
            ShowSelectEndDay = showSelectEndDay;
        }

        public void SetDelayedAdvancedCounter()
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            DelayedAdvancedStart = 0;
            DelayedAdvancedEnd = 0;

            if (DateTime.Today < currentPeriod.CycleStartDay || DateTime.Today > currentPeriod.CycleStartDay)
               DelayedAdvancedStart =  Math.Abs((DateTime.Today - currentPeriod.CycleStartDay).Days + 1);            
            else
                if (DateTime.Today < currentPeriod.CycleEndDay || DateTime.Today > currentPeriod.CycleEndDay)
                    DelayedAdvancedEnd = Math.Abs((DateTime.Today - currentPeriod.CycleEndDay).Days + 1);
               
        }


        public void SetDelayedAdvancedCounter(bool setStart, bool setEnd)
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            int counter;
            DelayedAdvancedStart = 0;
            DelayedAdvancedEnd = 0;

            DateTime expectedEndDate = SelectedStartCycle.AddDays(currentPeriod.CycleDuration);

            if (setStart && (SelectedStartCycle < currentPeriod.CycleStartDay || SelectedStartCycle > currentPeriod.CycleStartDay))
            {
                counter = (SelectedStartCycle - currentPeriod.CycleStartDay).Days + 1;
                 DelayedAdvancedStart = Math.Abs(counter);

                if (counter < 0)
                    SecondRowText = string.Format(AppResources.PeriodEarlierMessage, DelayedAdvancedStart);
                else
                    SecondRowText = string.Format(AppResources.PeriodLaterMessage, DelayedAdvancedStart);
                   
            }

            if (ShowSelectEndDay && (SelectedEndCycle < expectedEndDate || SelectedEndCycle > expectedEndDate))
            {
                counter = (SelectedEndCycle - expectedEndDate).Days + 1;
                DelayedAdvancedEnd = Math.Abs(counter);

                if (counter < 0)
                    ForthRowText = string.Format(AppResources.PeriodEarlierMessage, DelayedAdvancedEnd);
                else
                    ForthRowText = string.Format(AppResources.PeriodLaterMessage, DelayedAdvancedEnd);
            }

        }
        #endregion
    }
}