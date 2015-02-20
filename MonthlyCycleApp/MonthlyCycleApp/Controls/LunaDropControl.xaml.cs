using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using MonthlyCycleApp.ViewModels;
using MonthlyCycleApp.Helpers;
using MonthlyCycleApp.Model;
using MonthlyCycleApp.Resources;

namespace MonthlyCycleApp.Controls
{
    public partial class LunaDropControl : UserControl
    {

        public LunaDropControl()
        {
            InitializeComponent();
            this.DataContext = App.LunaViewModel;
            Loaded += LunaDropControl_Loaded;
        }

        #region Events
        void LunaDropControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetupOpacityEndOfCycle();
            SetupTimeframePosition();
        }


        private void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            if (currentPeriod.CycleStartDay > DateTime.Today)
            {
                ClearDrop();
                App.LunaViewModel.ShowCycleConfirmation = true;
            }
        }

        public static void SmartAdd()
        { 
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            //before cycle - you might have  an early cycle
            if (DateTime.Today <= currentPeriod.CycleEndDay &&)
            { 
            
            }
        }
       
        private void delay_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //delayed start
            App.MainViewModel.ShowDialog = true;
            App.MainViewModel.SetDelayedAdvancedCounter();
            App.MainViewModel.SetupDialog(AppResources.DelayPeriodMessage, AppResources.DelayPeriodQuestion, string.Empty,string.Empty, false, false);
        }

        private void addStart_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            App.MainViewModel.SetDelayedAdvancedCounter();

            App.MainViewModel.SetupStartPeriodDialog(currentPeriod.CycleStartDay, currentPeriod.CycleEndDay);
        }

        private void endCycle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            App.MainViewModel.SetDelayedAdvancedCounter();

            App.MainViewModel.SetupEndPeriodDialog(currentPeriod.CycleEndDay);
        }


        private void addCycle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            //late start
            App.MainViewModel.ShowDialog = true;
            App.MainViewModel.SetDelayedAdvancedCounter();

            App.MainViewModel.SetupAddPeriodDialog(currentPeriod.CycleEndDay);
        }

        #endregion

        #region UI methods
        private void SetupTimeframePosition()
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            ClearDrop();

            if (!App.MainViewModel.Return && currentPeriod.CycleStartDay <= DateTime.Today && DateTime.Today <= currentPeriod.CycleEndDay)
                App.LunaViewModel.ShowCycleConfirmation = true;
        }

        private void ClearDrop()
        {
            App.LunaViewModel.SetupControlsVisibility(false, false, false, false);
        }

        private void SetupOpacityEndOfCycle()
        {
            var currentPeriod = App.MainViewModel.Calendar.CurrentPeriod;
            double opacity = 0;
            if (App.MainViewModel.ShowSelectEndDay &&
                 DateTime.Today >= currentPeriod.CycleStartDay.AddDays(2) && DateTime.Today <= currentPeriod.CycleEndDay)
                opacity = 0;

            if (App.MainViewModel.ShowSelectEndDay &&
                DateTime.Today > currentPeriod.CycleEndDay)
            {
                int diff = (DateTime.Today - currentPeriod.CycleEndDay).Days;
                opacity = 0;

                if (diff < 8)
                    opacity = (8 - diff) * 12.5;
            }

            addEnd.Opacity = opacity;
        }

        #endregion

       
    }
}
