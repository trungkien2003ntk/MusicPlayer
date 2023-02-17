﻿#pragma checksum "..\..\..\..\ResourcesXAML\PlaylistResource.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3628ED2E3C04E0134DADDBD4B04AB6A1E1DE59A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MVVM_Basics.Views;
using MVVM_Basics.Views.Converter;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace MVVM_Basics.ResourcesXAML {
    
    
    /// <summary>
    /// PlaylistResource
    /// </summary>
    public partial class PlaylistResource : System.Windows.ResourceDictionary, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MVVM_Basics;component/resourcesxaml/playlistresource.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 55 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MenuItemAddToPlaylist_Loaded1);
            
            #line default
            #line hidden
            
            #line 56 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.MenuItemAddToPlaylist_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 179 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).Closed += new System.Windows.RoutedEventHandler(this.ContextMenu_Closed);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 235 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).Closed += new System.Windows.RoutedEventHandler(this.ContextMenu_Closed);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 291 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).Closed += new System.Windows.RoutedEventHandler(this.ContextMenu_Closed);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 5:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.PreviewMouseDoubleClickEvent;
            
            #line 447 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListView_PreviewMouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.PreviewMouseWheelEvent;
            
            #line 448 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseWheelEventHandler(this.ListView_PreviewMouseWheel);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 6:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.PreviewMouseDoubleClickEvent;
            
            #line 461 "..\..\..\..\ResourcesXAML\PlaylistResource.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListView_PreviewMouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

