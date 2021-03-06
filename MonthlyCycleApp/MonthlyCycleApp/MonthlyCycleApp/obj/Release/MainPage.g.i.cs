﻿#pragma checksum "C:\Users\georgiana\Documents\GitHub\LunaCycle\MonthlyCycleApp\MonthlyCycleApp\MonthlyCycleApp\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "21A1EB8CB45372A5BAF693E08DBDA486"
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
using MonthlyCycleApp.Controls;
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
using WPControls;


namespace MonthlyCycleApp {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal MonthlyCycleApp.Converters.ColorConverter ColorConverter;
        
        internal MonthlyCycleApp.Converters.EnumConverter EnumConverter;
        
        internal MonthlyCycleApp.Converters.BoolToVisibilityConverter BoolToVisibilityConverter;
        
        internal MonthlyCycleApp.Converters.StringToVisibilityConverter StringToVisibilityConverter;
        
        internal MonthlyCycleApp.Converters.IntToVisibilityConverter IntToVisibilityConverter;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image imgTopGradient;
        
        internal System.Windows.Controls.Grid TitleGrid;
        
        internal Microsoft.Phone.Controls.Pivot panoramaControl;
        
        internal Microsoft.Phone.Controls.PivotItem DropPage;
        
        internal MonthlyCycleApp.Controls.LunaDropControl dropControl;
        
        internal WPControls.Calendar Cal;
        
        internal System.Windows.Documents.Paragraph p1;
        
        internal System.Windows.Documents.Paragraph p2;
        
        internal System.Windows.Documents.Paragraph p3;
        
        internal System.Windows.Documents.Paragraph p4;
        
        internal System.Windows.Documents.Paragraph p5;
        
        internal System.Windows.Controls.ScrollViewer settingsScroll;
        
        internal System.Windows.Controls.Grid SettingsGrid;
        
        internal Microsoft.Phone.Controls.ToggleSwitch toggleBtnPillAllarm;
        
        internal System.Windows.Controls.TextBlock toggleBtnPillAllarmExplanation;
        
        internal System.Windows.Controls.Grid toggleBtnPillAllarmTimePanel;
        
        internal System.Windows.Controls.TextBlock tbAlarmTimeText;
        
        internal Microsoft.Phone.Controls.TimePicker alarmTimePicker;
        
        internal Microsoft.Phone.Controls.ToggleSwitch toggleBtnMenstruationAllarm;
        
        internal System.Windows.Controls.TextBlock toggleBtnMenstruationAllarmExplanation;
        
        internal System.Windows.Controls.Grid toggleBtnMenstruationAllarmTimePanel;
        
        internal System.Windows.Controls.TextBlock tMenstruationTimeText;
        
        internal Microsoft.Phone.Controls.TimePicker menstruationTimePicker;
        
        internal Microsoft.Phone.Controls.ToggleSwitch toggleBtnOvulationAllarm;
        
        internal System.Windows.Controls.TextBlock toggleBtnOvulationAllarmExplanation;
        
        internal System.Windows.Controls.Grid toggleBtnOvulationAllarmTimePanel;
        
        internal System.Windows.Controls.TextBlock tbOvulationTimeText;
        
        internal Microsoft.Phone.Controls.TimePicker ovulationTimePicker;
        
        internal System.Windows.Controls.Image imgBottomGradient;
        
        internal System.Windows.Controls.StackPanel dialogPanel;
        
        internal System.Windows.Controls.StackPanel firstRow;
        
        internal Microsoft.Phone.Controls.DatePicker pkStartDateCycle;
        
        internal System.Windows.Controls.TextBlock questionMark;
        
        internal System.Windows.Controls.StackPanel secondRow;
        
        internal System.Windows.Controls.TextBlock secondRowText;
        
        internal System.Windows.Controls.StackPanel thirdRow;
        
        internal Microsoft.Phone.Controls.DatePicker pkEndDateCycle;
        
        internal System.Windows.Controls.StackPanel forthRow;
        
        internal System.Windows.Controls.TextBlock forthRowText;
        
        internal System.Windows.Controls.Button okBtn;
        
        internal System.Windows.Controls.Button cancelBtn;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MonthlyCycleApp;component/MainPage.xaml", System.UriKind.Relative));
            this.ColorConverter = ((MonthlyCycleApp.Converters.ColorConverter)(this.FindName("ColorConverter")));
            this.EnumConverter = ((MonthlyCycleApp.Converters.EnumConverter)(this.FindName("EnumConverter")));
            this.BoolToVisibilityConverter = ((MonthlyCycleApp.Converters.BoolToVisibilityConverter)(this.FindName("BoolToVisibilityConverter")));
            this.StringToVisibilityConverter = ((MonthlyCycleApp.Converters.StringToVisibilityConverter)(this.FindName("StringToVisibilityConverter")));
            this.IntToVisibilityConverter = ((MonthlyCycleApp.Converters.IntToVisibilityConverter)(this.FindName("IntToVisibilityConverter")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.imgTopGradient = ((System.Windows.Controls.Image)(this.FindName("imgTopGradient")));
            this.TitleGrid = ((System.Windows.Controls.Grid)(this.FindName("TitleGrid")));
            this.panoramaControl = ((Microsoft.Phone.Controls.Pivot)(this.FindName("panoramaControl")));
            this.DropPage = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("DropPage")));
            this.dropControl = ((MonthlyCycleApp.Controls.LunaDropControl)(this.FindName("dropControl")));
            this.Cal = ((WPControls.Calendar)(this.FindName("Cal")));
            this.p1 = ((System.Windows.Documents.Paragraph)(this.FindName("p1")));
            this.p2 = ((System.Windows.Documents.Paragraph)(this.FindName("p2")));
            this.p3 = ((System.Windows.Documents.Paragraph)(this.FindName("p3")));
            this.p4 = ((System.Windows.Documents.Paragraph)(this.FindName("p4")));
            this.p5 = ((System.Windows.Documents.Paragraph)(this.FindName("p5")));
            this.settingsScroll = ((System.Windows.Controls.ScrollViewer)(this.FindName("settingsScroll")));
            this.SettingsGrid = ((System.Windows.Controls.Grid)(this.FindName("SettingsGrid")));
            this.toggleBtnPillAllarm = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("toggleBtnPillAllarm")));
            this.toggleBtnPillAllarmExplanation = ((System.Windows.Controls.TextBlock)(this.FindName("toggleBtnPillAllarmExplanation")));
            this.toggleBtnPillAllarmTimePanel = ((System.Windows.Controls.Grid)(this.FindName("toggleBtnPillAllarmTimePanel")));
            this.tbAlarmTimeText = ((System.Windows.Controls.TextBlock)(this.FindName("tbAlarmTimeText")));
            this.alarmTimePicker = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("alarmTimePicker")));
            this.toggleBtnMenstruationAllarm = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("toggleBtnMenstruationAllarm")));
            this.toggleBtnMenstruationAllarmExplanation = ((System.Windows.Controls.TextBlock)(this.FindName("toggleBtnMenstruationAllarmExplanation")));
            this.toggleBtnMenstruationAllarmTimePanel = ((System.Windows.Controls.Grid)(this.FindName("toggleBtnMenstruationAllarmTimePanel")));
            this.tMenstruationTimeText = ((System.Windows.Controls.TextBlock)(this.FindName("tMenstruationTimeText")));
            this.menstruationTimePicker = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("menstruationTimePicker")));
            this.toggleBtnOvulationAllarm = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("toggleBtnOvulationAllarm")));
            this.toggleBtnOvulationAllarmExplanation = ((System.Windows.Controls.TextBlock)(this.FindName("toggleBtnOvulationAllarmExplanation")));
            this.toggleBtnOvulationAllarmTimePanel = ((System.Windows.Controls.Grid)(this.FindName("toggleBtnOvulationAllarmTimePanel")));
            this.tbOvulationTimeText = ((System.Windows.Controls.TextBlock)(this.FindName("tbOvulationTimeText")));
            this.ovulationTimePicker = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("ovulationTimePicker")));
            this.imgBottomGradient = ((System.Windows.Controls.Image)(this.FindName("imgBottomGradient")));
            this.dialogPanel = ((System.Windows.Controls.StackPanel)(this.FindName("dialogPanel")));
            this.firstRow = ((System.Windows.Controls.StackPanel)(this.FindName("firstRow")));
            this.pkStartDateCycle = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("pkStartDateCycle")));
            this.questionMark = ((System.Windows.Controls.TextBlock)(this.FindName("questionMark")));
            this.secondRow = ((System.Windows.Controls.StackPanel)(this.FindName("secondRow")));
            this.secondRowText = ((System.Windows.Controls.TextBlock)(this.FindName("secondRowText")));
            this.thirdRow = ((System.Windows.Controls.StackPanel)(this.FindName("thirdRow")));
            this.pkEndDateCycle = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("pkEndDateCycle")));
            this.forthRow = ((System.Windows.Controls.StackPanel)(this.FindName("forthRow")));
            this.forthRowText = ((System.Windows.Controls.TextBlock)(this.FindName("forthRowText")));
            this.okBtn = ((System.Windows.Controls.Button)(this.FindName("okBtn")));
            this.cancelBtn = ((System.Windows.Controls.Button)(this.FindName("cancelBtn")));
        }
    }
}

