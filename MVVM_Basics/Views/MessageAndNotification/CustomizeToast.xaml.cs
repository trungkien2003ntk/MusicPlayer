using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MVVM_Basics.Views.MessageAndNotification;

public enum NotificationType
{
    Added,
    Removed,
    AddedPlaylist,
    RemovedPlaylist,
}

public enum TargetType
{
    LikedSongs,
    Artists,
    Library,
    Playlist,
}

public partial class CustomizeToast : UserControl, INotifyPropertyChanged
{
    private readonly Dictionary<NotificationType, string> NotificationStrings = new()
    {
        {NotificationType.Added, "Added to your " },
        {NotificationType.Removed, "Removed from your " },
        {NotificationType.AddedPlaylist, "Added to " },
        {NotificationType.RemovedPlaylist, "Removed from " },
    };
    private readonly Dictionary<TargetType, string> TargetStrings = new()
    {
        { TargetType.Artists, "Artists" },
        { TargetType.LikedSongs, "Liked Songs" },
        { TargetType.Library, "Library" },
        { TargetType.Playlist, "Playlist" },
    };


    
    private const double TIMEOUT_THRESHOLD = 1.5;
    private const double FADE_OUT_DURATION = 0.5;
    
    private DispatcherTimer _Timer;
    private DoubleAnimation _FadeOutAnimation;
    private double _CountSec = 0;


    private string _Notification = null!;
    public string Notification
    {
		get { return _Notification; }
		set { _Notification = value; OnPropertyChanged(); }
	}

    private string _Target = null!;
    public string Target
    {
        get { return _Target; }
        set { _Target = value; OnPropertyChanged(); }
    }


    public CustomizeToast(NotificationType notificationType, TargetType targetType)
    {
        InitializeComponent();

        // Initialize Timer
        _Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(0.5),
            IsEnabled = false,
        };
        _Timer.Tick += OnTimeTick;


        // Initialize Animation
        _FadeOutAnimation = new()
        {
            From = 1,
            To = 0,
            Duration = new Duration(TimeSpan.FromSeconds(FADE_OUT_DURATION)),
        };


        // Set DataContext for binding
        DataContext = this;


        // Set Notification and Destination
        Notification = NotificationStrings[notificationType];
        Target = TargetStrings[targetType];
    }

    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        _Timer.Start();
    }

    private void OnTimeTick(object? sender, EventArgs e)
    {
        _CountSec += 0.5;

        if (_CountSec == TIMEOUT_THRESHOLD)
        {
            BeginAnimation(OpacityProperty, _FadeOutAnimation);
        }

        if (_CountSec == TIMEOUT_THRESHOLD + FADE_OUT_DURATION)
        {
            var parentAsContentControl = (ContentControl)Parent;
            
            if (parentAsContentControl != null)
            {
                parentAsContentControl.Content = null;
            }

            _CountSec = 0;
            _Timer.Stop();
        }

    }

    // Implement InotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
