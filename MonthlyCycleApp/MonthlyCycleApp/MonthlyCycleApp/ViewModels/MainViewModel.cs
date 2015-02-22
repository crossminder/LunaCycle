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

        private PeriodCalendar calendar;
        public PeriodCalendar Calendar
        {

            get
            {
                if (calendar == null || calendar.IsNew())
                    calendar = PersistanceStorage.MockCalendar();
                //PersistanceStorage.ReadDataFromPersistanceStorage();
                return calendar;
            }
            set
            {
                if (value != calendar && !value.IsNew())
                {
                    calendar = value;
                    NotifyPropertyChanged("Calendar");
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

        #region dialog properties

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
                {
                    showDialog = value;
                    NotifyPropertyChanged("ShowDialog");
                }
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
                {
                    showSelectStartDay = value;
                    NotifyPropertyChanged("ShowSelectStartDay");
                }
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
                return selectedStartCycle.HasValue ? selectedStartCycle.Value : Calendar.CurrentPeriod.CycleStartDay; 
            }
            set
            {
                if (value != selectedStartCycle)
                {
                    selectedStartCycle = value;
                    NotifyPropertyChanged("SelectedStartCycle");

                }
            }
        }

        private DateTime? selectedEndCycle;
        public DateTime SelectedEndCycle
        {
            get
            {
                return selectedEndCycle.HasValue ? selectedEndCycle.Value : Calendar.CurrentPeriod.CycleEndDay; 
            }
            set
            {
                if (value != selectedEndCycle)
                {
                    selectedEndCycle = value;
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

        private string okButtonContent;
        public string OkButtonContent
        {
            get
            {
                return
                    okButtonContent;
            }
            set
            {
                if (okButtonContent != value)
                {
                    okButtonContent = value;
                    NotifyPropertyChanged("OkButtonContent");
                }
            }
        }

        private string cancelButtonContent;
        public string CancelButtonContent
        {
            get
            {
                return
                    cancelButtonContent;
            }
            set
            {
                if (cancelButtonContent != value)
                {
                    cancelButtonContent = value;
                    NotifyPropertyChanged("CancelButtonContent");
                }
            }
        }
     
        #endregion

        public MainViewModel()
        {
            
        }

        #region Dialog methods

        public void SetupDialog(ValidationEnum validationType)
        {
            switch (validationType)
            {
                case ValidationEnum.NoNeedForValidation:
                    {
                        var currentPeriod = Calendar.CurrentPeriod;

                        if (!App.LunaViewModel.StartCycleConfirmed &&
                            !App.LunaViewModel.EndCycleConfirmed &&
                            DateTime.Now >= SelectedStartCycle.AddDays(currentPeriod.CycleDuration))
                        {
                            FirstRowText = AppResources.PeriodStartedQuestion;
                            SecondRowText = string.Empty;
                            ThirdRowText = AppResources.PeriodEndedQuestion;
                            ForthRowText = string.Empty;
                            ShowSelectStartDay = true;
                            ShowSelectEndDay = true;
                        }
                        else
                            if (!App.LunaViewModel.StartCycleConfirmed)
                            {
                                FirstRowText = AppResources.PeriodStartedQuestion;
                                ShowSelectStartDay = true;

                            }
                            else
                                if (!App.LunaViewModel.EndCycleConfirmed && DateTime.Now >= SelectedStartCycle.AddDays(2))
                                {
                                    if (SelectedEndCycle != DateTime.Today)
                                        SelectedEndCycle = DateTime.Today;

                                    ThirdRowText = AppResources.PeriodEndedQuestion;
                                    ShowSelectEndDay = true;
                                }

                        if (ShowSelectStartDay || ShowSelectEndDay)
                        {
                            OkButtonContent = AppResources.OkButton;
                            CancelButtonContent = AppResources.CancelButton;

                            SetDelayedAdvancedCounter(currentPeriod);
                        }
                        break;
                    }
                case ValidationEnum.StartDateInFuture:
                    {
                        SecondRowText = AppResources.StartDateInFutureValidation;
                        ShowSelectEndDay = false;
                        ThirdRowText = string.Empty;

                        break;
                    }
                case ValidationEnum.DateOverlappsExistingPeriod:
                    {
                        SecondRowText = AppResources.OverlapExistingPeriodValidation;
                        OkButtonContent = AppResources.ModifyButton;
                        CancelButtonContent = AppResources.KeepButton;
                        //another page
                        //modify, keep this too
                        //
                        break;
                    }
                case ValidationEnum.EndDateBeforeStart:
                    {
                        OkButtonContent = AppResources.OkButton;
                        CancelButtonContent = AppResources.CancelButton;
                        ForthRowText = AppResources.EndDateBeforeStartValidation;
                        break;
                    }
                case ValidationEnum.EndDateTooCloseToStart:
                    {
                        OkButtonContent = AppResources.OkButton;
                        CancelButtonContent = AppResources.CancelButton;
                        ForthRowText = AppResources.EndDateTooCloseToStartValidation;
                        break;
                    }
                case ValidationEnum.EndDateFarInTheFuture:
                    {
                        OkButtonContent = AppResources.OkButton;
                        CancelButtonContent = AppResources.CancelButton;
                        ForthRowText = AppResources.EndDateFarInFutureValidation;
                        break;
                    }
            }
            
        }


        public void SetDelayedAdvancedCounter(PeriodMonth currentPeriod)
        {
            int counter;
            DelayedAdvancedStart = 0;
            DelayedAdvancedEnd = 0;

            DateTime expectedEndDate = SelectedStartCycle.AddDays(currentPeriod.CycleDuration);

            if (ShowSelectStartDay && (SelectedStartCycle < currentPeriod.CycleStartDay || SelectedStartCycle > currentPeriod.CycleStartDay))
            {
                 counter = (SelectedStartCycle - currentPeriod.CycleStartDay).Days;
                 DelayedAdvancedStart = Math.Abs(counter);

                if (counter < 0)
                    SecondRowText = string.Format(AppResources.PeriodEarlierMessage, DelayedAdvancedStart);
                else
                    if (counter > 0)
                    SecondRowText = string.Format(AppResources.PeriodLaterMessage, DelayedAdvancedStart);
                   
            }

            if (ShowSelectEndDay && (SelectedEndCycle < expectedEndDate || SelectedEndCycle > expectedEndDate))
            {
                counter = (SelectedEndCycle - expectedEndDate).Days;
                DelayedAdvancedEnd = Math.Abs(counter);

                if (counter < 0)
                    ForthRowText = string.Format(AppResources.PeriodEarlierMessage, DelayedAdvancedEnd);
                else
                    if (counter > 0)
                    ForthRowText = string.Format(AppResources.PeriodLaterMessage, DelayedAdvancedEnd);
            }

        }

        public void OkCommand()
        {
            Return = false;
            var currentPeriod = Calendar.CurrentPeriod;
            if (ShowSelectStartDay && ShowSelectEndDay)
            {
                currentPeriod.CycleStartDay = SelectedStartCycle;
                currentPeriod.CycleEndDay = SelectedEndCycle;

                App.LunaViewModel.StartCycleConfirmed = true;
                App.LunaViewModel.EndCycleConfirmed = true;
            }
            else
                if (ShowSelectStartDay)
                {
                    currentPeriod.CycleStartDay = SelectedStartCycle;
                    currentPeriod.CycleEndDay = currentPeriod.CycleStartDay.AddDays(currentPeriod.CycleDuration);
                    currentPeriod.PeriodEndDay = currentPeriod.CycleStartDay.AddDays(currentPeriod.PeriodDuration);

                    App.LunaViewModel.StartCycleConfirmed = true;
                    App.LunaViewModel.EndCycleConfirmed = false;
                }
                else
                    if (ShowSelectEndDay)
                    {
                        if (Calendar.CurrentPeriod.CycleEndDay != SelectedEndCycle)
                        {
                            currentPeriod.CycleEndDay = SelectedEndCycle;

                            int computedCycleDuration = (currentPeriod.CycleEndDay - currentPeriod.CycleStartDay).Days;
                            if (computedCycleDuration != currentPeriod.CycleDuration)
                                currentPeriod.CycleDuration = computedCycleDuration;
                        }
                        App.LunaViewModel.EndCycleConfirmed = true;
                    }

            //if there are confirmed, there is no need to show them
            ShowSelectStartDay = !App.LunaViewModel.StartCycleConfirmed;
            ShowSelectEndDay = !App.LunaViewModel.EndCycleConfirmed;

            Calendar.CurrentPeriod = currentPeriod;

            App.LunaViewModel.SetDropValues();
            App.MainViewModel.ShowDialog = false;
        }

        public void CancelCommand()
        {
            var currentPeriod = Calendar.CurrentPeriod;
            App.MainViewModel.SelectedStartCycle = currentPeriod.CycleStartDay;
            App.MainViewModel.SelectedEndCycle = currentPeriod.CycleEndDay;
            ShowDialog = false;
            Return = false;
        }
        #endregion

     
      
      
    }
}