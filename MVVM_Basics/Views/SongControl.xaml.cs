using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using MVVM_Basics.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.ViewModels;
using System.Windows.Threading;
using System.Transactions;
using GalaSoft.MvvmLight.Messaging;
using MVVM_Basics.Helpers;

namespace MVVM_Basics.Views;

public partial class SongControl : UserControl
{
    private const int MINIMUM_PLAY_DURATION = 30;
    private const int SAVE_DATE_PLAY_THRESHOLD = 3;
    private readonly DispatcherTimer timer;
    private readonly string _TimeFormat = @"mm\:ss";


    private double secondsPlayed;
    private double _SavedSliderVolumeValue;
    private ISoundPlayer? _SoundPlayer;
    private bool playCounted = false;
    private bool isPlayDateSaved = false;

    public SongControl()
    {
        InitializeComponent();

        // Register DataContextChanged event
        DataContextChanged += OnDataContextChanged;

        // Resolve DataContext
        DataContext = App.AppHost!.Services.GetRequiredService<SongControlViewModel>();
        
        // Register Unloaded Event
        Unloaded += OnUnloaded;

        // Initialize Timer
        timer = new()
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        timer.Tick += OnTimerTick;
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        if (mediaPlayer.Source != null)
        {
            sliderTime.Value += 1;
            secondsPlayed += 1;
        }

        // save the song as played
        if (secondsPlayed >= SAVE_DATE_PLAY_THRESHOLD && !isPlayDateSaved)
        {
            Messenger.Default.Send(new SaveSongPlayDateMessage());
            isPlayDateSaved = true;
        }

        // increase the play of the song
        if (secondsPlayed >= MINIMUM_PLAY_DURATION && !playCounted)
        {
            Messenger.Default.Send(new IncreasePlayCountMessage());

            playCounted = true;
        }
    }

    void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (DataContext is ISoundPlayer player && _SoundPlayer != player)
        {
            if (_SoundPlayer != null)
            {
                _SoundPlayer.Play -= OnPlay;
                _SoundPlayer.Pause -= OnPause;
            }

            _SoundPlayer = player;
            _SoundPlayer.Play += OnPlay;
            _SoundPlayer.Pause += OnPause;
            _SoundPlayer.OnSongChanged += _SoundPlayer_OnSongChanged;
        }
    }

    private void _SoundPlayer_OnSongChanged(string songPath)
    {
        if (!string.IsNullOrEmpty(songPath))
        {
            mediaPlayer.Source = null;
            mediaPlayer.Source = new Uri(songPath);
            mediaPlayer.Position = TimeSpan.Zero;
            sliderTime.Value = 0;

            // reset play counter
            secondsPlayed = 0;
            playCounted = false;
            isPlayDateSaved = false;

            OnPlay();
        }
    }

    void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (_SoundPlayer != null)
        {
            _SoundPlayer.Play -= OnPlay;
            secondsPlayed = 0;
            playCounted = false;
        }
    }

    private void OnPlay()
    {
        mediaPlayer.Play();
        timer.Start();
    }

    private void OnPause()
    {
        mediaPlayer.Pause();
        timer.Stop();
    }

    private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // Change Volume Button Icon
        PackIcon icon = new()
        {
            Width = 25,
            Height = 25,
        };

        if (sliderVolume.Value > 60)
        {
            icon.Kind = PackIconKind.VolumeHigh;
        }
        else if (20 < sliderVolume.Value && sliderVolume.Value <= 60) 
        {
            icon.Kind = PackIconKind.VolumeMedium;
        }
        else if (0 < sliderVolume.Value && sliderVolume.Value <= 20) 
        {
            icon.Kind = PackIconKind.VolumeLow;
        }
        else
        {
            icon.Kind = PackIconKind.VolumeMute;
        }

        btnVolume.Content = icon;

        // Change MediaElment's Volume
        mediaPlayer.Volume = sliderVolume.Value / 100f;
    }

    private void btnVolumeMax_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sliderVolume.Value != 0)
        {
            _SavedSliderVolumeValue = sliderVolume.Value;
            sliderVolume.Value = 0;
        }
        else
        {
            sliderVolume.Value = _SavedSliderVolumeValue;
        }
    }

    private void sliderTime_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds((int)sliderTime.Value);
        TimeSpan mediaTimeSpan = new(0, 0, 0, 0, (int)timeSpan.TotalMilliseconds);

        mediaPlayer.Position = mediaTimeSpan;
    }
}
