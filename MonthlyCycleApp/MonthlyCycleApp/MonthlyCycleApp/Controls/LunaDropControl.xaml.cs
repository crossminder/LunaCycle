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
using System.Windows.Media.Animation;

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

        }
        private void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (App.MainViewModel.ShowSelectStartDay || App.MainViewModel.ShowSelectEndDay)
            {
                (this.Resources["Blink"] as Storyboard).Pause();

                App.MainViewModel.ShowDialog = true;

                App.MainViewModel.SetupDialog(ValidationEnum.NoNeedForValidation, App.MainViewModel.NextPeriod);
            }
        }
        #endregion

        private void waving_Completed(object sender, EventArgs e)
        {

        }

    }
}
