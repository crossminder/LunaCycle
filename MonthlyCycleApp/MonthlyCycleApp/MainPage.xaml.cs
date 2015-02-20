using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using MonthlyCycleApp.Resources;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using MonthlyCycleApp.Model;


namespace MonthlyCycleApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.MainViewModel;
            Loaded += MainPage_Loaded;
            App.MainViewModel.Return = false;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            Cal.PeriodCalendarProperty = App.MainViewModel.Calendar;
            //startingWeekDayList.DataContext = daysOfWeek;
            startingWeekDayList.ItemsSource = App.MainViewModel.DaysOfWeek;
          //  startingPageList.SelectedIndex = Convert.ToInt32( App.MainViewModel.FirstDayOfWeek);

            App.MainViewModel.SelectedStartCycle = DateTime.Today;
            App.MainViewModel.SelectedEndCycle = (App.MainViewModel.ShowSelectStartDay && App.MainViewModel.ShowSelectEndDay) ?
                DateTime.Today.AddDays(App.MainViewModel.Calendar.CurrentPeriod.CycleDuration) :
                DateTime.Today;
        }

        #region Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.SetupViewModel.ShowInitialSetup = !App.SetupViewModel.SetupCompleted;

            //remove initial setup page from history
            if (NavigationService.BackStack.Count() > 0)
            {
                var previousPage = this.NavigationService.BackStack.FirstOrDefault();

                if (previousPage != null && previousPage.Source.ToString().StartsWith("/InitialSetupPage.xaml"))
                {
                    this.NavigationService.RemoveBackEntry();
                }
            }
        }
     
        #endregion

        #region Events

        private void startingPageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void toggleBtnPillAllarm_CheckedUnchecked(object sender, RoutedEventArgs e)
        {       
            ToggleSwitch toggleSwitchBtn = (sender as ToggleSwitch);
           Grid gridContainer = (toggleSwitchBtn.Parent as Grid).Children.OfType<Grid>().SingleOrDefault(x => x.Name == toggleSwitchBtn.Name + "TimePanel") as Grid;

           
            Storyboard sbUp;
            Storyboard sbDown;
            if (gridContainer != null)
            {
                DefineAnimations(gridContainer,80, out sbUp, out sbDown);
                SetupCheckUncheckBehaviour(toggleSwitchBtn,sbUp, sbDown, AppResources.On, AppResources.Off);
            }
            
        }

      
      
        private void panoramaControl_Loaded(object sender, RoutedEventArgs e)
        {
            panoramaControl.Visibility = Visibility.Visible;
        }
   
        private void StartCycle_Click(object sender, EventArgs e)
        {
            App.MainViewModel.Calendar.CurrentPeriod.CycleStartDay = Cal.SelectedDate;
          //  RestoreAppBarDefaultValues();
          //  periodChosen = true;
        }

        private void EndCycle_Click(object sender, EventArgs e)
        {
            App.MainViewModel.Calendar.CurrentPeriod.CycleEndDay = Cal.SelectedDate;
          //  RestoreAppBarDefaultValues();
         //   periodChosen = true;
        }


        private void startingWeekDayList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = (sender as ListPicker).SelectedIndex;
            if (selectedIndex >= 0)
                App.MainViewModel.FirstDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), selectedIndex.ToString());
        }

        private void toggleBtnPeriodForecast_CheckedUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitchBtn = (sender as ToggleSwitch);
          //  Grid gridContainer = (toggleSwitchBtn.Parent as Grid).Children.OfType<Grid>().SingleOrDefault(x => x.Name == toggleSwitchBtn.Name + "TimePanel") as Grid;
            TextBlock tblExplanation = (toggleSwitchBtn.Parent as Grid).Children.OfType<TextBlock>().SingleOrDefault(x => x.Name == toggleSwitchBtn.Name + "Explanation") as TextBlock;

            if (toggleSwitchBtn.IsChecked.GetValueOrDefault())
            {
                toggleSwitchBtn.Content = AppResources.Advanced;
            }
            else
            {
                toggleSwitchBtn.Content = AppResources.Standard;
            }
            /*
            Storyboard sbUp;
            Storyboard sbDown;

            if (gridContainer != null)
            {
                double height =  80;
                DefineAnimations(gridContainer, height, out sbUp, out sbDown);
                SetupCheckUncheckBehaviour(toggleSwitchBtn,  sbUp, sbDown, AppResources.Advanced, AppResources.Standard);

               if (tblExplanation != null)
                   tblExplanation.Visibility = System.Windows.Visibility.Visible;
            }

            //settingsScroll.UpdateLayout();
            //settingsScroll.ScrollToVerticalOffset(SettingsGrid.ActualHeight);
             * */
        }
        #endregion

        #region Private Methods

        private static void SetupCheckUncheckBehaviour(ToggleSwitch toggleSwitchBtn,Storyboard sbUp, Storyboard sbDown, string onValue, string offValue)
        {
            TextBlock tblExplanation = (toggleSwitchBtn.Parent as Grid).Children.OfType<TextBlock>().SingleOrDefault(x => x.Name == toggleSwitchBtn.Name + "Explanation") as TextBlock;

            //is checked
            if (toggleSwitchBtn.IsChecked.GetValueOrDefault())
            {
                toggleSwitchBtn.Content = onValue;
                sbDown.Stop();
                sbUp.Begin();
               if( tblExplanation != null )
                   tblExplanation.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                toggleSwitchBtn.Content = offValue;
                sbUp.Stop();
                sbDown.Begin();
                if (tblExplanation != null)
                    tblExplanation.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private static void DefineAnimations(Grid gridContainer, double height, out Storyboard sbUp, out Storyboard sbDown)
        {
            sbUp = new Storyboard();
            sbUp.Duration = new Duration(new TimeSpan(0, 0, 1));
            sbDown = new Storyboard();
            sbDown.Duration = new Duration(new TimeSpan(0, 0, 1));


            DoubleAnimation heightUp = new DoubleAnimation()
            {
                From = 0,
                To = height,
                SpeedRatio = 4,
                Duration = new Duration(new TimeSpan(0, 0, 1)),

            };

            DoubleAnimation heightDown = new DoubleAnimation()
            {
                From = height,
                To = 0,
                SpeedRatio = 4,
                Duration = new Duration(new TimeSpan(0, 0, 1)),

            };
            Storyboard.SetTarget(heightUp, gridContainer);
            Storyboard.SetTarget(heightDown, gridContainer);

            Storyboard.SetTargetProperty(heightUp, new PropertyPath("Height"));
            Storyboard.SetTargetProperty(heightDown, new PropertyPath("Height"));

            sbUp.Children.Add(heightUp);
            sbDown.Children.Add(heightDown);
        }


        private void AppBarContextualMenu()
        {
          //  cycleDropControl.Opacity = 0.7;
            this.ApplicationBar.Mode = ApplicationBarMode.Default;
            this.ApplicationBar.Opacity = 1;
            (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).IsEnabled = true;
            (ApplicationBar.MenuItems[1] as ApplicationBarMenuItem).IsEnabled = App.MainViewModel.EndOfCycleEnabled;
        }

        private void RestoreAppBarDefaultValues()
        {
         //   cycleDropControl.Opacity = 1;
            this.ApplicationBar.Opacity = 0.5;
            this.ApplicationBar.Mode = ApplicationBarMode.Minimized;
            (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).IsEnabled = false;
            (ApplicationBar.MenuItems[1] as ApplicationBarMenuItem).IsEnabled = false;
        }
        #endregion

        #region Orchestration

        public void Refresh(Timeframe momentInPeriod, Step startCycleStage, Step endCycleStage)
        {
            switch (momentInPeriod)
            {
                case Timeframe.Before:
                    {
                        //invisible
                        //if Tap Refresh
                        break;
                    }
                case Timeframe.During:
                    {
                        switch (startCycleStage)
                        {
                            case Step.OnTime:
                                {
                                    switch (endCycleStage)
                                    {
                                        case Step.OnTime:
                                            {
                                                //Refresh(duringCycle,confirmedCycleStartOnTime,confirmedCycleEndOnTime)
                                                break;
                                            }
                                        case Step.Pending:
                                            {
                                                //NormalStartNormalEnd(duringCycle,confirmedCycleStartOnTime,pendingEndCycleConfirmation)
                                                break;
                                            }
                                        case Step.Early:
                                            {
                                                //  NormalStartEarlyEnd(duringCycle,confirmedCycleStartOnTime, confirmedCycleEndBeforeTime, counter=no of days(diff) )
                                                break;
                                            }
                                        case Step.Late:
                                            {
                                                // NormalStartLateEnd(duringCycle,confirmedCycleStartOnTime, confirmedCycleEndAtferTime, counter=  a cata zi de la normal end=day0 si 7 este pentru opacitate )
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case Step.Late:
                                {
                                    switch (endCycleStage)
                                    {
                                        case Step.Unconfirmed:
                                            {
                                                //LateStart(duringCycle,notConfirmedStart) = show message
                                                break;
                                            }
                                        case Step.Delayed:
                                            {
                                                //LateStartDelayed(duringCycle,delayedStart) = shows button
                                                break;
                                            }


                                    }
                                    break;
                                }
                            case Step.Pending:
                                {
                                    switch (endCycleStage)
                                    {
                                        case Step.Unconfirmed:
                                            {
                                                //today<estimated initial end
                                                //LateStartDelayedWithinEstimation(duringCycle, pendingCycleStartLater,notConfirmedCycleEnd) - button pressed- show message to select start
                                                break;
                                            }
                                        case Step.Pending:
                                            {
                                                //today>estimated initial end
                                                //LateStartDelayedOutsideEstimation(afterCycle,pendingCycleStartLater, pendingEndCycleConfirmation)- button pressed- show message to select start and end

                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case Timeframe.After:
                    {
                        switch (startCycleStage)
                        {
                            case Step.Late:
                                {
                                    switch (endCycleStage)
                                    {
                                        case Step.Late:
                                            {
                                                //Refresh(afterCycle,confirmedCycleStartLater,confirmedCycleEndLater)
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case Step.Unconfirmed:
                                {
                                    switch (endCycleStage)
                                    {
                                        case Step.Unconfirmed:
                                            {
                                                //Refresh(afterCycle,notConfirmedCycleStart,notConfirmedCycleEnd) - today>=28 days (cycle duration)
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public void AdjustUI()
        {
            //set visibility on buttons

            //set visibility on screens

            // set opacity

        }


        #endregion



        private void cbPeriodForecastTimeText_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void tbPeriodForecast_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbPeriodForecast_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            if (App.MainViewModel.ShowSelectStartDay && App.MainViewModel.ShowSelectEndDay)
            {
                currentPeriod.CycleStartDay = App.MainViewModel.SelectedStartCycle;
                currentPeriod.CycleEndDay = App.MainViewModel.SelectedEndCycle;
                
                //clear 
                App.LunaViewModel.SetupControlsVisibility(false, false, false, false);
            }

            if (App.MainViewModel.ShowSelectStartDay)
            {
                currentPeriod.CycleStartDay = App.MainViewModel.SelectedStartCycle;
                currentPeriod.CycleEndDay = currentPeriod.CycleStartDay.AddDays(currentPeriod.CycleDuration);
                currentPeriod.PeriodEndDay = currentPeriod.CycleStartDay.AddDays(currentPeriod.PeriodDuration);

                App.LunaViewModel.SetupControlsVisibility(false, true, false, false);
            }

            if (App.MainViewModel.ShowSelectEndDay)
            {
                if (App.MainViewModel.Calendar.CurrentPeriod.CycleEndDay != App.MainViewModel.SelectedEndCycle)
                {
                    currentPeriod.CycleEndDay = App.MainViewModel.SelectedEndCycle;

                    int computedCycleDuration = (currentPeriod.CycleEndDay - currentPeriod.CycleStartDay).Days;
                    if (computedCycleDuration != currentPeriod.CycleDuration)
                        currentPeriod.CycleDuration = computedCycleDuration;
                }

                //clear
               App.LunaViewModel.SetupControlsVisibility(true, false, false, false);
            }
            App.MainViewModel.Calendar.CurrentPeriod = currentPeriod;

            //delayed
            if (!App.MainViewModel.ShowSelectStartDay && !App.MainViewModel.ShowSelectEndDay)
            {
                App.LunaViewModel.SetupControlsVisibility(false, false, false, true);
                App.LunaViewModel.DaysToPeriodText = "?";

            }
            App.LunaViewModel.SetDropValues();
            App.MainViewModel.ShowDialog = false;
        }

       
        

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.ShowDialog = false;
        }

        
        private void pkEndDateCycle_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            App.MainViewModel.Return = true;
        }

        private void pkEndDateCycle_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (e.NewDateTime != e.OldDateTime && e.NewDateTime.HasValue)
            {
                App.MainViewModel.SelectedEndCycle = e.NewDateTime.Value;
                App.MainViewModel.SetDelayedAdvancedCounter(false,true);
            }
        }

      

        private void pkStartDateCycle_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (e.NewDateTime != e.OldDateTime && e.NewDateTime.HasValue)
            {
                App.MainViewModel.SelectedStartCycle = e.NewDateTime.Value;
                App.MainViewModel.SelectedEndCycle = e.NewDateTime.Value.AddDays(App.MainViewModel.Calendar.CurrentPeriod.CycleDuration);
              
                App.MainViewModel.SetDelayedAdvancedCounter(true,true);
            }
        }

     

        

    }
}