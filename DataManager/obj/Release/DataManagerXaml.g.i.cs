﻿#pragma checksum "..\..\DataManagerXaml.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "09D628451396B798B9B797074E84E99058BE608A05978447E8E13D525A35273C"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using DataManager;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
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


namespace DataManager {
    
    
    /// <summary>
    /// AddData
    /// </summary>
    public partial class AddData : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Forms.DataGridView DG2;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_StaffNumber;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_People;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dp_OrginTime;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dp_EndTime;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_Staellite;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_Orbit;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_ModifyData;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_DeleteData;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\DataManagerXaml.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Refresh;
        
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
            System.Uri resourceLocater = new System.Uri("/DataManager;component/datamanagerxaml.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DataManagerXaml.xaml"
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
            this.DG2 = ((System.Windows.Forms.DataGridView)(target));
            
            #line 13 "..\..\DataManagerXaml.xaml"
            this.DG2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG2_CellClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txt_StaffNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txt_People = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.dp_OrginTime = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.dp_EndTime = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.cmb_Staellite = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.cmb_Orbit = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            
            #line 29 "..\..\DataManagerXaml.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectData_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 30 "..\..\DataManagerXaml.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_Add_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btn_ModifyData = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\DataManagerXaml.xaml"
            this.btn_ModifyData.Click += new System.Windows.RoutedEventHandler(this.btn_Modify_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btn_DeleteData = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\DataManagerXaml.xaml"
            this.btn_DeleteData.Click += new System.Windows.RoutedEventHandler(this.btn_Delete_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btn_Refresh = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\DataManagerXaml.xaml"
            this.btn_Refresh.Click += new System.Windows.RoutedEventHandler(this.btn_Refresh_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
