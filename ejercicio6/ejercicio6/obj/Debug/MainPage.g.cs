﻿#pragma checksum "C:\Users\juan\Desktop\MOV\MOV_tema1\ejercicio6\ejercicio6\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8CE4127BE07B34D41820600B7A4D7728"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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


namespace ejercicio6 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox tbxNombre;
        
        internal System.Windows.Controls.PasswordBox pwdNombre;
        
        internal System.Windows.Controls.Button button1;
        
        internal System.Windows.Controls.TextBlock lblBan;
        
        internal System.Windows.Controls.TextBlock lblError;
        
        internal System.Windows.Controls.Grid panelPrincipal;
        
        internal System.Windows.Controls.Image miImagen;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/ejercicio6;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tbxNombre = ((System.Windows.Controls.TextBox)(this.FindName("tbxNombre")));
            this.pwdNombre = ((System.Windows.Controls.PasswordBox)(this.FindName("pwdNombre")));
            this.button1 = ((System.Windows.Controls.Button)(this.FindName("button1")));
            this.lblBan = ((System.Windows.Controls.TextBlock)(this.FindName("lblBan")));
            this.lblError = ((System.Windows.Controls.TextBlock)(this.FindName("lblError")));
            this.panelPrincipal = ((System.Windows.Controls.Grid)(this.FindName("panelPrincipal")));
            this.miImagen = ((System.Windows.Controls.Image)(this.FindName("miImagen")));
        }
    }
}
