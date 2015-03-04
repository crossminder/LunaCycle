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

            Cal.PeriodCalendarProperty = null;
            Cal.PeriodCalendarProperty = App.MainViewModel.Calendar;
          
          //  startingWeekDayList.ItemsSource = App.MainViewModel.DaysOfWeek;
        }

        #region Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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

            SetupAnimation(toggleSwitchBtn, gridContainer, 80);
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

        //private void toggleBtnPeriodForecast_CheckedUnchecked(object sender, RoutedEventArgs e)
        //{
        //    ToggleSwitch toggleSwitchBtn = (sender as ToggleSwitch);
        //    TextBlock tblExplanation = (toggleSwitchBtn.Parent as Grid).Children.OfType<TextBlock>().SingleOrDefault(x => x.Name == toggleSwitchBtn.Name + "Explanation") as TextBlock;

        //    if (toggleSwitchBtn.IsChecked.GetValueOrDefault())
        //    {
        //        toggleSwitchBtn.Content = AppResources.Advanced;
        //    }
        //    else
        //    {
        //        toggleSwitchBtn.Content = AppResources.Standard;
        //    }
        //    /*
        //    Storyboard sbUp;
        //    Storyboard sbDown;

        //    if (gridContainer != null)
        //    {
        //        double height =  80;
        //        DefineAnimations(gridContainer, height, out sbUp, out sbDown);
        //        SetupCheckUncheckBehaviour(toggleSwitchBtn,  sbUp, sbDown, AppResources.Advanced, AppResources.Standard);

        //       if (tblExplanation != null)
        //           tblExplanation.Visibility = System.Windows.Visibility.Visible;
        //    }

        //    //settingsScroll.UpdateLayout();
        //    //settingsScroll.ScrollToVerticalOffset(SettingsGrid.ActualHeight);
        //     * */
        //}
        #endregion

        #region Private Methods

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

        #region Setttings password
        private void togglePwdProtection_CheckedUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitchBtn = (sender as ToggleSwitch);
            if (toggleSwitchBtn.IsChecked.HasValue)
            {
                bool firstTimePassword = string.IsNullOrWhiteSpace(App.MainViewModel.ApplicationPassword);

                if (firstTimePassword)
                {
                    btnResetPin.Content = AppResources.SetPinButtonText;

                    bool isChecked = toggleSwitchBtn.IsChecked.Value;

                    oldPin.Visibility = tblOldPin.Visibility = isChecked ? Visibility.Collapsed : Visibility.Visible;
                    btnChangePin.Content = isChecked ? AppResources.SetPinButtonText : AppResources.ChangeButtonText;

                    Grid gridContainer = (toggleSwitchBtn.Parent as Grid).Children.OfType<Grid>().SingleOrDefault(x => x.Name == toggleSwitchBtn.Name + "Panel") as Grid;

                    SetupAnimation(toggleSwitchBtn, gridContainer, 110);

                    btnResetPin.Background = isChecked ? Application.Current.Resources["PinkColor"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                }
                else
                    btnResetPin.Content = AppResources.ResetPinButtonText;
            }
        }
        private void resetPin_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.SetPassword = true;
        }
        #endregion

        #region Animation
        private static void SetupCheckUncheckBehaviour(ToggleSwitch toggleSwitchBtn, Storyboard sbUp, Storyboard sbDown, string onValue, string offValue)
        {
            TextBlock tblExplanation = (toggleSwitchBtn.Parent as Grid).Children.OfType<TextBlock>().SingleOrDefault(x => x.Name == toggleSwitchBtn.Name + "Explanation") as TextBlock;

            //is checked
            if (toggleSwitchBtn.IsChecked.GetValueOrDefault())
            {
                toggleSwitchBtn.Content = onValue;
                sbDown.Stop();
                sbUp.Begin();
                if (tblExplanation != null)
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

        private static void SetupAnimation(ToggleSwitch toggleSwitchBtn, Grid gridContainer, double height)
        {
            Storyboard sbUp;
            Storyboard sbDown;
            if (gridContainer != null)
            {
                DefineAnimations(gridContainer, height, out sbUp, out sbDown);
                SetupCheckUncheckBehaviour(toggleSwitchBtn, sbUp, sbDown, AppResources.On, AppResources.Off);
            }
        }

        private static void AnimateValidation(TextBox tb)
        {
            Grid gridContainer = tb.Parent as Grid;
            Storyboard sbUp;
            Storyboard sbDown;
            if (gridContainer != null)
            {
                DefineAnimations(gridContainer, 110, out sbUp, out sbDown);
            }
        }
        #endregion

        #region Dialog events

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals(AppResources.OkButton))
            {
                App.MainViewModel.OkCommand();

                Cal.PeriodCalendarProperty = null;
                Cal.PeriodCalendarProperty = App.MainViewModel.Calendar;
                Cal.Refresh();
            }

            if ((sender as Button).Content.Equals(AppResources.ReplaceButton))
            {
                App.MainViewModel.ReplaceCommand();
            }

            (dropControl.Resources["Blink"] as Storyboard).Resume();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals(AppResources.CancelButton))
            {
                App.MainViewModel.CancelCommand();
            }
         
            (dropControl.Resources["Blink"] as Storyboard).Resume();
        }
        
        private void pkEndDateCycle_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            App.MainViewModel.Return = true;
        }

        private void pkEndDateCycle_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (e.NewDateTime != DateTime.MinValue && 
                e.NewDateTime != e.OldDateTime &&
                e.NewDateTime.HasValue && 
                (sender as DatePicker).Name == pkEndDateCycle.Name)
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
                        if (Math.Abs((tempEnd - App.MainViewModel.SelectedEndCycle).Days) > App.MainViewModel.Calendar.AveragePeriodDuration)
                            validationType = ValidationEnum.EndDateFarInTheFuture;
             
                var period = App.MainViewModel.NextPeriod;
              
                if (validationType == ValidationEnum.NoNeedForValidation)
                {
                    App.MainViewModel.SelectedEndCycle = tempEnd;
                    App.MainViewModel.SetupDialog(validationType, period);
                }
                else
                {
                    App.MainViewModel.SelectedEndCycle = tempEnd;
                    App.MainViewModel.SetupDialog(validationType, period);
                }

                (sender as DatePicker).BorderBrush = (validationType == ValidationEnum.NoNeedForValidation) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Red);
                forthRowText.Foreground = (validationType == ValidationEnum.NoNeedForValidation) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Red);
            }
        }

        private void pkStartDateCycle_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (e.NewDateTime != DateTime.MinValue && 
                e.NewDateTime != e.OldDateTime && 
                e.NewDateTime.HasValue &&
                (sender as DatePicker).Name == pkStartDateCycle.Name)
            {
                DateTime tempStart = e.NewDateTime.Value;
                DateTime tempEnd = e.NewDateTime.Value.AddDays(App.MainViewModel.Calendar.AveragePeriodDuration - 1);

                ValidationEnum validationType = ValidationEnum.NoNeedForValidation;

                if (DateTime.Today < tempStart)
                    validationType = ValidationEnum.StartDateInFuture;
                else
                {
                    PeriodMonth nearbyPeriod = ExtensionMethods.FindOverlappingExistingPeriod(tempStart, tempEnd,
                           App.MainViewModel.Calendar.PastPeriods, App.MainViewModel.NextPeriod);
                    if (nearbyPeriod != null)
                        validationType = ValidationEnum.DateOverlappsExistingPeriod;
                }

                var period = App.MainViewModel.NextPeriod;
                if (validationType == ValidationEnum.NoNeedForValidation)
                {
                    App.MainViewModel.SelectedStartCycle = tempStart;
                    App.MainViewModel.SelectedEndCycle = tempEnd;

                    App.MainViewModel.SetupDialog(validationType, period);
                }
                else
                    App.MainViewModel.SetupDialog(validationType, period);

                (sender as DatePicker).BorderBrush = (validationType == ValidationEnum.NoNeedForValidation) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Red);
                secondRowText.Foreground = (validationType == ValidationEnum.NoNeedForValidation) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Red);
                okBtn.IsEnabled = (validationType != ValidationEnum.StartDateInFuture);
            }
        }
        
    

        #endregion

        #region Password
        private void enterPin_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (App.MainViewModel.IsPasswordProtected)
            {
                string pinEntered = enterPin.Text;
                if (pinEntered.Length > 4)
                    MessageBox.Show("Enter a 4 digit pin");
                else
                {
                    string pwd = App.MainViewModel.ApplicationPassword;
                    if (pwd != pinEntered)
                        MessageBox.Show("Pins don't match");
                    else
                        App.MainViewModel.LoggedInNeeded = false;
                }
            }
        }

        private void btnChangePin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(newPinValue))
            {
                App.MainViewModel.ApplicationPassword = newPinValue;
                App.MainViewModel.SetPassword = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.SetPassword = false;
        }

        private void oldPin_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            tblValidation.SetValue(Grid.RowProperty, 0);

            if (ValidatePin(tb, string.Format(AppResources.PinValidationMissing, tblOldPin.Text), AppResources.PinValidationInvalidSize))
            {
                string oldPwd = App.MainViewModel.ApplicationPassword;
                if (ValidatePinWithExisting(tb, oldPwd, AppResources.PinValidationOldPinNotMatch))
                {
                    newPin.IsEnabled = newPinConfirm.IsEnabled = true;
                    newPin.Focus();
                }
                else
                {
                    AnimateValidation(tb);

                    newPin.IsEnabled = newPinConfirm.IsEnabled = false;

                }

            }
        }

        string newPinValue = string.Empty;
        private void newPin_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            tblValidation.SetValue(Grid.RowProperty, 1);

            if (ValidatePin(tb, string.Format(AppResources.PinValidationMissing, tblNewPin.Text), AppResources.PinValidationInvalidSize))
            {
                newPinValue = tb.Text;
                newPinConfirm.IsEnabled = true;
                newPinConfirm.Focus();
            }
            else
                newPinConfirm.IsEnabled = false;
        }

        private void newPinConfirm_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            tblValidation.SetValue(Grid.RowProperty, 2);

            if (ValidatePin(tb, string.Format(AppResources.PinValidationMissing, tblConfirmPin.Text), AppResources.PinValidationInvalidSize)
                && ValidatePinWithExisting(tb, newPinValue, AppResources.PinValidationNewPinNotMatch))
            {
                btnChangePin.Focus();
            }
            else 
            {
            }
        }

        #endregion

        #region Password validation

        private bool ValidatePin(TextBox tb, string validationMissingMessage, string validationInvalidMessage)
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BorderBrush = Application.Current.Resources["PinkColor"] as SolidColorBrush;
                tblValidation.Text = validationMissingMessage;
                return false;
            }
            if (tb.Text.Length > 4)
            {
                tb.BorderBrush = Application.Current.Resources["PinkColor"] as SolidColorBrush;
                tblValidation.Text = validationInvalidMessage;
                return false;
            }
            tb.BorderBrush = new SolidColorBrush(Colors.White);
            tblValidation.Text = string.Empty;
            return true;
        }

        private bool ValidatePinWithExisting(TextBox tb, string existingPwd, string validationMessage)
        {
            if (tb.Text != existingPwd)
            {
                tb.BorderBrush = Application.Current.Resources["PinkColor"] as SolidColorBrush;
                tblValidation.Text = validationMessage;
                return false;
            }

            tb.BorderBrush = new SolidColorBrush(Colors.White);
            tblValidation.Text = string.Empty;
            return true;
        }

        #endregion


    }
}