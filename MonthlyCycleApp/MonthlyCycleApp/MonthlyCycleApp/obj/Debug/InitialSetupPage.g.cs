﻿#pragma checksum "C:\Users\georgiana\Documents\GitHub\LunaCycle\MonthlyCycleApp\MonthlyCycleApp\MonthlyCycleApp\InitialSetupPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "96EAB36301AD2828F8EA74FE44916BDD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MonthlyCycleApp {
    
    
    public partial class InitialSetupPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock tbPeriodDurationText;
        
        internal System.Windows.Controls.TextBox tbPeriodDuration;
        
        internal System.Windows.Controls.TextBlock tbPeriodExplanation;
        
        internal System.Windows.Controls.TextBlock tbCycleDurationText;
        
        internal System.Windows.Controls.TextBox tbCycleDuration;
        
        internal System.Windows.Controls.TextBlock tbCycleExplanation;
        
        internal System.Windows.Controls.TextBlock tbLastCycleText;
        
        internal Microsoft.Phone.Controls.DatePicker pkLastCycle;
        
        internal System.Windows.Controls.TextBlock pkLastCycleExplanation;
        
        internal System.Windows.Controls.Button setupBtn;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MonthlyCycleApp;component/InitialSetupPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tbPeriodDurationText = ((System.Windows.Controls.TextBlock)(this.FindName("tbPeriodDurationText")));
            this.tbPeriodDuration = ((System.Windows.Controls.TextBox)(this.FindName("tbPeriodDuration")));
            this.tbPeriodExplanation = ((System.Windows.Controls.TextBlock)(this.FindName("tbPeriodExplanation")));
            this.tbCycleDurationText = ((System.Windows.Controls.TextBlock)(this.FindName("tbCycleDurationText")));
            this.tbCycleDuration = ((System.Windows.Controls.TextBox)(this.FindName("tbCycleDuration")));
            this.tbCycleExplanation = ((System.Windows.Controls.TextBlock)(this.FindName("tbCycleExplanation")));
            this.tbLastCycleText = ((System.Windows.Controls.TextBlock)(this.FindName("tbLastCycleText")));
            this.pkLastCycle = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("pkLastCycle")));
            this.pkLastCycleExplanation = ((System.Windows.Controls.TextBlock)(this.FindName("pkLastCycleExplanation")));
            this.setupBtn = ((System.Windows.Controls.Button)(this.FindName("setupBtn")));
        }
    }
}

