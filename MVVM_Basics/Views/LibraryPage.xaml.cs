using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MVVM_Basics.Helpers;
using MVVM_Basics.Interfaces;
using MVVM_Basics.Models;
using MVVM_Basics.ViewModels;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM_Basics.Views;

public partial class LibraryPage : UserControl
{
    public LibraryPage() 
    { 
        InitializeComponent();

        DataContext = App.AppHost!.Services.GetRequiredService<LibraryPageViewModel>();

        // Set titlebar's background
        titleBar.Background = ColorHelper.GetGradientColorAtIndex((rootControl.Background as LinearGradientBrush)!, 0);
    }

    private void btnImportSong_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog()
        {
            Multiselect = true,
            Filter = "Audio Files (mp3)|*.mp3",
            Title = "Choose Mp3 Files",
            InitialDirectory = @"C:\Users\ADMIN\Music",
        };

        if (dialog.ShowDialog() == true)
        {
            string[] songPcLinks = dialog.FileNames;

            foreach (string songPcLink in songPcLinks)
            {
                (DataContext as LibraryPageViewModel)!.AddSongToLibrary(songPcLink);
            }
        }
    }

    // Bubble scrolling the listview
    private void lvAllSongs_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (!e.Handled)
        {
            e.Handled = true;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = MouseWheelEvent;
            eventArg.Source = sender;

            var parent = scrollViewerMain as UIElement;
            parent.RaiseEvent(eventArg);
        }
    }

    
}
