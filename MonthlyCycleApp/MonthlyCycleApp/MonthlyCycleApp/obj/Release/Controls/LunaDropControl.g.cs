﻿#pragma checksum "C:\Users\georgiana\Documents\GitHub\LunaCycle\MonthlyCycleApp\MonthlyCycleApp\MonthlyCycleApp\Controls\LunaDropControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ED71CB32A9F7342A399FB545D700D113"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MonthlyCycleApp.Converters;
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


namespace MonthlyCycleApp.Controls {
    
    
    public partial class LunaDropControl : System.Windows.Controls.UserControl {
        
        internal MonthlyCycleApp.Converters.BoolToVisibilityConverter BoolToVisibilityConverter;
        
        internal System.Windows.Media.Animation.Storyboard Blink;
        
        internal System.Windows.Controls.Grid LayoutRoot2;
        
        internal System.Windows.Shapes.Ellipse ellipse;
        
        internal System.Windows.Controls.Grid drop;
        
        internal System.Windows.Shapes.Path wave3;
        
        internal System.Windows.Shapes.Path wave2;
        
        internal System.Windows.Shapes.Path wave1;
        
        internal System.Windows.Controls.TextBlock tbToday;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MonthlyCycleApp;component/Controls/LunaDropControl.xaml", System.UriKind.Relative));
            this.BoolToVisibilityConverter = ((MonthlyCycleApp.Converters.BoolToVisibilityConverter)(this.FindName("BoolToVisibilityConverter")));
            this.Blink = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Blink")));
            this.LayoutRoot2 = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot2")));
            this.ellipse = ((System.Windows.Shapes.Ellipse)(this.FindName("ellipse")));
            this.drop = ((System.Windows.Controls.Grid)(this.FindName("drop")));
            this.wave3 = ((System.Windows.Shapes.Path)(this.FindName("wave3")));
            this.wave2 = ((System.Windows.Shapes.Path)(this.FindName("wave2")));
            this.wave1 = ((System.Windows.Shapes.Path)(this.FindName("wave1")));
            this.tbToday = ((System.Windows.Controls.TextBlock)(this.FindName("tbToday")));
        }
    }
}
