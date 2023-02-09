using GalaSoft.MvvmLight.Messaging;
using MVVM_Basics.Helpers;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MVVM_Basics.Views;

public partial class SearchPage : UserControl
{
    DispatcherTimer _Timer;

    public SearchPage()
    {
        InitializeComponent();

        _Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.5) };
        _Timer.Tick += OnTimerTick;

        // Set titlebar's background
        titleBar.Background = ColorHelper.GetGradientColorAtIndex((rootControl.Background as LinearGradientBrush)!, 0);
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _Timer.Stop();

        // searching
        Messenger.Default.Send(new SearchMessage(txbSearch.Text.ToLower()));
    }

    private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        _Timer.Stop();
        _Timer.Start();
    }
}
