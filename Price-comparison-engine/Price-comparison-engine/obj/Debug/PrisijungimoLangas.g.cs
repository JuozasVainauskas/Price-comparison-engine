﻿#pragma checksum "..\..\PrisijungimoLangas.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F311146ED5E6EA48B6E7E99C1B1C90922B318B947003E690C7FCE1CEE5EDE254"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Price_comparison_engine;
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


namespace Price_comparison_engine {
    
    
    /// <summary>
    /// PrisijungimoLangas
    /// </summary>
    public partial class PrisijungimoLangas : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 67 "..\..\PrisijungimoLangas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UsernameBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\PrisijungimoLangas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\PrisijungimoLangas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\PrisijungimoLangas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\PrisijungimoLangas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock pranešimas;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\PrisijungimoLangas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock pranešimas_Copy;
        
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
            System.Uri resourceLocater = new System.Uri("/Price-comparison-engine;component/prisijungimolangas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PrisijungimoLangas.xaml"
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
            this.UsernameBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\PrisijungimoLangas.xaml"
            this.UsernameBox.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.Email_Laukas);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 68 "..\..\PrisijungimoLangas.xaml"
            this.PasswordBox.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.Slaptazodzio_Laukas);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 72 "..\..\PrisijungimoLangas.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Prisijungti_mygtukas);
            
            #line default
            #line hidden
            return;
            case 4:
            this.textBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\PrisijungimoLangas.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.Sukurti_nauja_slaptazodi_mygtukas);
            
            #line default
            #line hidden
            return;
            case 6:
            this.pranešimas = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.pranešimas_Copy = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

