﻿#pragma checksum "..\..\..\DefaultPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BD3C781267DD311D4198D11C08BD71BB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace ArduinoRFID {
    
    
    /// <summary>
    /// DefaultPage
    /// </summary>
    public partial class DefaultPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\DefaultPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ArduinoPortName;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\DefaultPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnConnect;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\DefaultPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox GroupBoxReg;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\DefaultPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbxCourse;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\DefaultPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnEnroll;
        
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
            System.Uri resourceLocater = new System.Uri("/ArduinoRFID;component/defaultpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DefaultPage.xaml"
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
            this.ArduinoPortName = ((System.Windows.Controls.ComboBox)(target));
            
            #line 26 "..\..\..\DefaultPage.xaml"
            this.ArduinoPortName.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ArduinoPortName_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BtnConnect = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\DefaultPage.xaml"
            this.BtnConnect.Click += new System.Windows.RoutedEventHandler(this.BtnConnect_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.GroupBoxReg = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 4:
            this.CbxCourse = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.BtnEnroll = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\DefaultPage.xaml"
            this.BtnEnroll.Click += new System.Windows.RoutedEventHandler(this.BtnEnroll_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

