﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CF22576FB2C2A516EFB2E82F06E907E2DED00F2C8767FC61D61472AE903E5C2A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GoGitaReportsApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GoGitaReportsApp {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 10 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid pnlMainGrid;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton reportRadioButton1;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton reportRadioButton2;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton reportRadioButton3;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radioButton1;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radioButton2;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView srcListView;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView destListView;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerFrom;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerTo;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GoGitaReportsApp;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.pnlMainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.reportRadioButton1 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 28 "..\..\MainWindow.xaml"
            this.reportRadioButton1.Checked += new System.Windows.RoutedEventHandler(this.ReportRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.reportRadioButton2 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 29 "..\..\MainWindow.xaml"
            this.reportRadioButton2.Checked += new System.Windows.RoutedEventHandler(this.ReportRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.reportRadioButton3 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 30 "..\..\MainWindow.xaml"
            this.reportRadioButton3.Checked += new System.Windows.RoutedEventHandler(this.ReportRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.radioButton1 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 42 "..\..\MainWindow.xaml"
            this.radioButton1.Checked += new System.Windows.RoutedEventHandler(this.DeliveryRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.radioButton2 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 43 "..\..\MainWindow.xaml"
            this.radioButton2.Checked += new System.Windows.RoutedEventHandler(this.DeliveryRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 53 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).Drop += new System.Windows.DragEventHandler(this.SrcItemList_Drop);
            
            #line default
            #line hidden
            
            #line 54 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).GiveFeedback += new System.Windows.GiveFeedbackEventHandler(this.SrcItemList_GiveFeedback);
            
            #line default
            #line hidden
            
            #line 55 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).DragEnter += new System.Windows.DragEventHandler(this.SrcItemList_DragEnter);
            
            #line default
            #line hidden
            
            #line 55 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).DragLeave += new System.Windows.DragEventHandler(this.SrcItemList_DragLeave);
            
            #line default
            #line hidden
            
            #line 56 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).DragOver += new System.Windows.DragEventHandler(this.SrcItemList_DragOver);
            
            #line default
            #line hidden
            return;
            case 8:
            this.srcListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            
            #line 76 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).Drop += new System.Windows.DragEventHandler(this.DestItemList_Drop);
            
            #line default
            #line hidden
            
            #line 77 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).GiveFeedback += new System.Windows.GiveFeedbackEventHandler(this.DestItemList_GiveFeedback);
            
            #line default
            #line hidden
            
            #line 78 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).DragEnter += new System.Windows.DragEventHandler(this.DestItemList_DragEnter);
            
            #line default
            #line hidden
            
            #line 78 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).DragLeave += new System.Windows.DragEventHandler(this.DestItemList_DragLeave);
            
            #line default
            #line hidden
            
            #line 79 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).DragOver += new System.Windows.DragEventHandler(this.DestItemList_DragOver);
            
            #line default
            #line hidden
            return;
            case 11:
            this.destListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 13:
            this.datePickerFrom = ((System.Windows.Controls.DatePicker)(target));
            
            #line 105 "..\..\MainWindow.xaml"
            this.datePickerFrom.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePickerFrom_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.datePickerTo = ((System.Windows.Controls.DatePicker)(target));
            
            #line 113 "..\..\MainWindow.xaml"
            this.datePickerTo.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePickerTo_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\MainWindow.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.GenerateButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 9:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseMoveEvent;
            
            #line 65 "..\..\MainWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseEventHandler(this.SrcItem_MouseMove);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 12:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseMoveEvent;
            
            #line 88 "..\..\MainWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseEventHandler(this.DestItem_MouseMove);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

