// Updated by XamlIntelliSenseFileGenerator 2020-09-17 12:58:38
#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "21CE742844F312AEEE37A4BF0EDCAA826C7792DD7FB7D214979F6B95C3D8C780"
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


namespace Price_comparison_engine
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 72 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ieškojimoLaukas;

#line default
#line hidden


#line 85 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Ieškoti;

#line default
#line hidden


#line 86 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button prisijungimosMygtukas;

#line default
#line hidden


#line 91 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button registracijosMygtukas;

#line default
#line hidden


#line 93 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DUKMygtukas;

#line default
#line hidden


#line 94 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button kontaktuMygtukas;

#line default
#line hidden


#line 96 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock pranešimas;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Price-comparison-engine;component/mainwindow.xaml", System.UriKind.Relative);

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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.pagrindinisLangas = ((Price_comparison_engine.MainWindow)(target));

#line 8 "..\..\MainWindow.xaml"
                    this.pagrindinisLangas.SizeChanged += new System.Windows.SizeChangedEventHandler(this.Window_SizeChanged);

#line default
#line hidden
                    return;
                case 2:
                    this.ieškojimoLaukas = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 3:
                    this.Ieškoti = ((System.Windows.Controls.Button)(target));

#line 85 "..\..\MainWindow.xaml"
                    this.Ieškoti.Click += new System.Windows.RoutedEventHandler(this.Ieškoti_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.prisijungimosMygtukas = ((System.Windows.Controls.Button)(target));

#line 86 "..\..\MainWindow.xaml"
                    this.prisijungimosMygtukas.Click += new System.Windows.RoutedEventHandler(this.PrisijungtiMygtukas_Click);

#line default
#line hidden
                    return;
                case 5:
                    this.registracijosMygtukas = ((System.Windows.Controls.Button)(target));

#line 92 "..\..\MainWindow.xaml"
                    this.registracijosMygtukas.Click += new System.Windows.RoutedEventHandler(this.RegistruotisMygtukas_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.DUKMygtukas = ((System.Windows.Controls.Button)(target));

#line 93 "..\..\MainWindow.xaml"
                    this.DUKMygtukas.Click += new System.Windows.RoutedEventHandler(this.DUKMygtukas_Click);

#line default
#line hidden
                    return;
                case 7:
                    this.kontaktuMygtukas = ((System.Windows.Controls.Button)(target));

#line 94 "..\..\MainWindow.xaml"
                    this.kontaktuMygtukas.Click += new System.Windows.RoutedEventHandler(this.KontaktaiMygtukas_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.pasiūlymųLangas = ((System.Windows.Controls.ScrollViewer)(target));
                    return;
                case 9:
                    this.pranešimas = ((System.Windows.Controls.TextBlock)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window pagrindinisLangas;
    }
}

