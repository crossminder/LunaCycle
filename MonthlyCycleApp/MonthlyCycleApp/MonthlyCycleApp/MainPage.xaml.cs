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
using System.Windows.Data;
using WPControls.Models;
using MonthlyCycleApp.Helpers;
using WPControls.Helpers;


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
            (dropControl.Resources["Blink"] as Storyboard).Begin();

            //Cal.PeriodCalendarProperty = App.MainViewModel.Calendar;
            //startingWeekDayList.DataContext = daysOfWeek;
            startingWeekDayList.ItemsSource = App.MainViewModel.DaysOfWeek;
          //  startingPageList.SelectedIndex = Convert.ToInt32( App.MainViewModel.FirstDayOfWeek);
          
            //if (!App.MainViewModel.Return)
            //{
            //    App.MainViewModel.SelectedStartCycle = App.MainViewModel.Calendar.CurrentPeriod.CycleStartDay;
            //    App.MainViewModel.SelectedEndCycle = App.MainViewModel.Calendar.CurrentPeriod.CycleEndDay;
            //}
        }

        #region Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.SetupViewModel.ShowInitialSetup = true;
                //!App.SetupViewModel.SetupCompleted;

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
                DefineAnimations(gridContainer, 80, out sbUp, out sbDown);
                SetupCheckUncheckBehaviour(toggleSwitchBtn, sbUp, sbDown, AppResources.On, AppResources.Off);
            }
        }
      
        private void panoramaControl_Loaded(object sender, RoutedEventArgs e)
        {
            panoramaControl.Visibility = Visibility.Visible;
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

        private void cbPeriodForecastTimeText_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void tbPeriodForecast_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbPeriodForecast_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        #region Dialog events

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.OkCommand();

            Cal.PeriodCalendarProperty = null;
            Cal.PeriodCalendarProperty = App.MainViewModel.Calendar;
            Cal.Refresh();
            (dropControl.Resources["Blink"] as Storyboard).Resume();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.CancelCommand();

            (dropControl.Resources["Blink"] as Storyboard).Resume();
        }
        
        private void pkEndDateCycle_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            App.MainViewModel.Return = true;
        }

        private void pkEndDateCycle_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (e.NewDateTime != DateTime.MinValue && e.NewDateTime != e.OldDateTime && e.NewDateTime.HasValue)
            {
                DateTime tempEnd = e.NewDateTime.Value;
                bool validateEndDate = tempEnd < App.MainViewModel.SelectedStartCycle.AddDays(2);

                ValidationEnum validationType = ValidationEnum.NoNeedForValidation;

                if (tempEnd < App.MainViewModel.SelectedStartCycle)
                    validationType = ValidationEnum.EndDateBeforeStart;
                else
                    if (tempEnd < App.MainViewModel.SelectedStartCycle.AddDays(2))
                        validationType = ValidationEnum.EndDateBeforeStart;
                    else
                        //faaar in the future
                        if (Math.Abs((tempEnd - App.MainViewModel.SelectedEndCycle).Days) > App.MainViewModel.Calendar.AverageCycleDuration)
                            validationType = ValidationEnum.EndDateFarInTheFuture;

                if (validationType == ValidationEnum.NoNeedForValidation)
                {
                    App.MainViewModel.SetupDialog(validationType);
                   // App.MainViewModel.SelectedEndCycle = tempEnd;
                }
                else
                    App.MainViewModel.SetupDialog(validationType);

                (sender as DatePicker).BorderBrush = (validationType == ValidationEnum.NoNeedForValidation) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Red);
                forthRowText.Foreground = (validationType == ValidationEnum.NoNeedForValidation) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Red);
            }
        }

        private void pkStartDateCycle_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (e.NewDateTime != DateTime.MinValue && e.NewDateTime != e.OldDateTime && e.NewDateTime.HasValue)
            {
                DateTime tempStart = e.NewDateTime.Value;
                DateTime tempEnd = e.NewDateTime.Value.AddDays(App.MainViewModel.Calendar.AverageCycleDuration);

                ValidationEnum validationType = ValidationEnum.NoNeedForValidation;

                if (DateTime.Today < tempStart)
                    validationType = ValidationEnum.StartDateInFuture;
                else
                {
                    PeriodMonth nearbyPeriod = ExtensionMethods.FindOverlappingExistingPeriod(tempStart, tempEnd,
                           App.MainViewModel.Calendar.PastPeriods, App.MainViewModel.Calendar.CurrentPeriod);
                    if (nearbyPeriod != null)
                        validationType = ValidationEnum.DateOverlappsExistingPeriod;
                }

                if (validationType == ValidationEnum.NoNeedForValidation)
                {
                    App.MainViewModel.SelectedStartCycle = tempStart;
                    App.MainViewModel.SelectedEndCycle = tempEnd;
                 
                    App.MainViewModel.SetupDialog(validationType);
                }
                else
                    App.MainViewModel.SetupDialog(validationType);

                (sender as DatePicker).BorderBrush = (validationType == ValidationEnum.NoNeedForValidation) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Red);
                secondRowText.Foreground = (validationType == ValidationEnum.NoNeedForValidation) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Red);
                okBtn.IsEnabled = (validationType != ValidationEnum.StartDateInFuture);
            }
        }
        
    

        #endregion

    }
}