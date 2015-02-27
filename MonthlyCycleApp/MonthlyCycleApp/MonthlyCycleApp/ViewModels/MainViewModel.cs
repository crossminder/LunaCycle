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
using System.Linq;

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
        public PeriodMonth NextPeriod
        {
            get
            {

             //   PeriodMonth period = new PeriodMonth(); 
                PeriodMonth currentPeriod = Calendar.CurrentPeriod;
                List<PeriodMonth> futurePeriods = Calendar.FuturePeriods;

                if (currentPeriod != null && futurePeriods != null && futurePeriods.Count > 0 && DateTime.Today >= currentPeriod.PeriodEndDay)
                    return futurePeriods.FirstOrDefault();

                if (currentPeriod != null && 
                    (!StartCycleConfirmed || !EndCycleConfirmed ||
                    DateTime.Today <= currentPeriod.PeriodEndDay))
                  return currentPeriod;
             
               

                return new PeriodMonth();
            }
        }

        public bool EndOfCycleEnabled
        {
            get
            {
                DateTime startCycle = Calendar.CurrentPeriod.PeriodStartDay;
                return startCycle == DateTime.MinValue ? false : startCycle <= DateTime.Today.AddDays(-2) ? true : false;
            }
        }

        private PeriodCalendar calendar;
        public PeriodCalendar Calendar
        {

            get
            {
                if (calendar == null || calendar.IsNew())
                    calendar = //PersistanceStorage.MockCalendar();
                PersistanceStorage.ReadDataFromPersistanceStorage();
                if (calendar == null || calendar.IsNew())
                    calendar = PersistanceStorage.GenerateCalendarData(CycleDurationSetting, PeriodDurationSetting, LastPeriodDateSetting);
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

        public DateTime LastPeriodDateSetting
        {
            get { return ApplicationSettings.GetProperty<DateTime>(ApplicationSettings.LAST_PERIOD_SETTING); }
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
                return selectedStartCycle.HasValue ? selectedStartCycle.Value : NextPeriod.PeriodStartDay <= DateTime.Today ? NextPeriod.PeriodStartDay : DateTime.Today; 
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
                return selectedEndCycle.HasValue ? selectedEndCycle.Value : NextPeriod.PeriodStartDay <= DateTime.Today ? NextPeriod.PeriodEndDay : SelectedStartCycle.AddDays(Calendar.AveragePeriodDuration - 1); 
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
        #endregion

        public MainViewModel()
        {
            
        }

        #region Dialog methods

        public void SetupDialog(ValidationEnum validationType, PeriodMonth period)
        {
            switch (validationType)
            {
                case ValidationEnum.NoNeedForValidation:
                    {
                        if (!StartCycleConfirmed &&
                            !EndCycleConfirmed &&
                            DateTime.Now >= SelectedStartCycle.AddDays(period.PeriodDuration))
                        {
                            FirstRowText = AppResources.PeriodStartedQuestion;
                            SecondRowText = string.Empty;
                            ThirdRowText = AppResources.PeriodEndedQuestion;
                            ForthRowText = string.Empty;
                            ShowSelectStartDay = true;
                            ShowSelectEndDay = true;
                        }
                        else
                            if (!StartCycleConfirmed)
                            {
                                FirstRowText = AppResources.PeriodStartedQuestion;
                                ThirdRowText = string.Empty;
                                ForthRowText = string.Empty;
                                ShowSelectStartDay = true;
                            }
                            else
                            {
                                FirstRowText = string.Empty;
                                SecondRowText = string.Empty;

                                if (!EndCycleConfirmed && DateTime.Now >= SelectedStartCycle.AddDays(2))
                                {
                                    if (SelectedEndCycle > DateTime.Today)
                                        SelectedEndCycle = DateTime.Today;

                                    ThirdRowText = AppResources.PeriodEndedQuestion;
                                    ShowSelectEndDay = true;
                                }
                                else
                                {
                                    OkButtonContent = string.Empty;
                                    CancelButtonContent = string.Empty;
                                    ShowDialog = false;
                                    break;
                                }
                            }
                        if (ShowSelectStartDay || ShowSelectEndDay)
                        {
                            OkButtonContent = AppResources.OkButton;
                            CancelButtonContent = AppResources.CancelButton;

                            SetDelayedAdvancedCounter(period);
                        }
                        break;
                    }
                case ValidationEnum.StartDateInFuture:
                    {
                        FirstRowText = AppResources.PeriodStartedQuestion;
                        SecondRowText = AppResources.StartDateInFutureValidation;
                        ShowSelectEndDay = false;
                        ThirdRowText = string.Empty;

                        break;
                    }
                case ValidationEnum.DateOverlappsExistingPeriod:
                    {
                        FirstRowText = AppResources.PeriodStartedQuestion;
                        SecondRowText = AppResources.OverlapExistingPeriodValidation;
                        OkButtonContent = AppResources.ReplaceButton;
                        CancelButtonContent = AppResources.CancelButton;
    
                        //enable modify
                        //recall this page
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

            DateTime expectedEndDate = SelectedStartCycle.AddDays(currentPeriod.PeriodDuration - 1);

            if (ShowSelectStartDay && (SelectedStartCycle < currentPeriod.PeriodStartDay || SelectedStartCycle > currentPeriod.PeriodStartDay))
            {
                 counter = (SelectedStartCycle - currentPeriod.PeriodStartDay).Days;
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
            var currentPeriod = NextPeriod;

            if (ShowSelectStartDay)
            {
                currentPeriod.PeriodStartDay = SelectedStartCycle;
                currentPeriod.PeriodEndDay = currentPeriod.PeriodStartDay.AddDays(currentPeriod.PeriodDuration - 1);
                currentPeriod.CycleEndDay = currentPeriod.PeriodStartDay.AddDays(currentPeriod.CycleDuration - 1);
 
                StartCycleConfirmed = true;
                EndCycleConfirmed = false;
            }

            if (ShowSelectEndDay)
            {
                if (Calendar.CurrentPeriod.PeriodEndDay != SelectedEndCycle)
                {
                    currentPeriod.PeriodEndDay = SelectedEndCycle;

                 
                }
                EndCycleConfirmed = true;
            }

            SetupCalendarData(currentPeriod);

            App.LunaViewModel.SetDropValues(currentPeriod);

            //if there are confirmed, there is no need to show them
            ShowSelectStartDay = !StartCycleConfirmed;
            ShowSelectEndDay = !EndCycleConfirmed;

            ShowDialog = false;
        }

        private void SetupCalendarData(PeriodMonth modifiedCurrentPeriod)
        {
            var currentStoredPeriod = NextPeriod;
            var pastPeriods = Calendar.PastPeriods;

            if (pastPeriods.Contains(currentStoredPeriod))
            {
                PeriodMonth month = pastPeriods.FirstOrDefault(item => item.PeriodStartDay == currentStoredPeriod.PeriodStartDay && item.PeriodEndDay == currentStoredPeriod.PeriodEndDay);
                month = modifiedCurrentPeriod;
            }
            else
            {
                if (StartCycleConfirmed && EndCycleConfirmed)
                {
                    pastPeriods.Add(modifiedCurrentPeriod);
                    Calendar.PastPeriods = pastPeriods;
                    Calendar.CurrentPeriod = Calendar.FuturePeriods.FirstOrDefault();
                    
                    ClearCache();
                }
                else
                {
                    if (StartCycleConfirmed && pastPeriods.Count > 0)
                    {
                        PeriodMonth previousPeriod = pastPeriods[pastPeriods.Count - 1];

                        //this period started early/ later -> this means the previous period ended early/later
                        if (previousPeriod.CycleEndDay.AddDays(1) != modifiedCurrentPeriod.PeriodStartDay)
                        {
                            previousPeriod.CycleEndDay = modifiedCurrentPeriod.PeriodStartDay.AddDays(-1);
                            previousPeriod.CycleDuration = (previousPeriod.CycleEndDay - previousPeriod.PeriodStartDay).Days + 1;
                        }

                        pastPeriods[pastPeriods.Count - 1] = previousPeriod;
                    }
                    else
                        if (EndCycleConfirmed)
                        {
                            int computedCycleDuration = (modifiedCurrentPeriod.PeriodEndDay - modifiedCurrentPeriod.PeriodStartDay).Days + 1;
                           
                            //this period ended later/earlier -> this means the duration was longer/shorter
                            if (computedCycleDuration != modifiedCurrentPeriod.PeriodDuration)
                                modifiedCurrentPeriod.PeriodDuration = computedCycleDuration;
                        }

                    Calendar.CurrentPeriod = modifiedCurrentPeriod;
                    Calendar.PastPeriods = pastPeriods;
                }
            }
            PersistanceStorage.WriteDataToPersistanceStorage(calendar);

        }

        public void CancelCommand()
        {
            var currentPeriod = NextPeriod;
            SelectedStartCycle = currentPeriod.PeriodStartDay;
            SelectedEndCycle = currentPeriod.PeriodEndDay;
            ShowDialog = false;
            Return = false;
        }

        private void ClearCache()
        {
            if (StartCycleConfirmed && EndCycleConfirmed)
            {
                ApplicationSettings.RemoveProperty(ApplicationSettings.IS_CYCLE_START_CONFIRMED);
                ApplicationSettings.RemoveProperty(ApplicationSettings.IS_CYCLE_END_CONFIRMED);
            }
        }
        #endregion





        public void ReplaceCommand()
        {
            PeriodMonth period = new PeriodMonth(SelectedStartCycle, Calendar.AverageCycleDuration, Calendar.AveragePeriodDuration);
            SelectedEndCycle = SelectedStartCycle.AddDays(Calendar.AveragePeriodDuration - 1);

            SetupDialog(ValidationEnum.NoNeedForValidation, period);
        }
    }
}