﻿#pragma checksum "..\..\AddUserNameForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EF6F0EED2C060C5101197BCAFFBAA147CB052F5AF4B4691BF5DC396FBBE954BF"
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
    /// AddUserNameForm
    /// </summary>
    public partial class AddUserNameForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\AddUserNameForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Forms.DataGridView DG1;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\AddUserNameForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_UserName;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\AddUserNameForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_StaffNumber;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\AddUserNameForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_Name;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\AddUserNameForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_SelectUser;
        
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
            System.Uri resourceLocater = new System.Uri("/DataManager;component/addusernameform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddUserNameForm.xaml"
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
            this.DG1 = ((System.Windows.Forms.DataGridView)(target));
            
            #line 14 "..\..\AddUserNameForm.xaml"
            this.DG1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG1_CellClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txt_UserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txt_StaffNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txt_Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btn_SelectUser = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\AddUserNameForm.xaml"
            this.btn_SelectUser.Click += new System.Windows.RoutedEventHandler(this.btn_SelectUser_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 25 "..\..\AddUserNameForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_JoinUser);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 26 "..\..\AddUserNameForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_Modify_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 27 "..\..\AddUserNameForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_DeleteUser_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

