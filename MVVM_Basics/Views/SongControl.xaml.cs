using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using MVVM_Basics.Interfaces;

namespace MVVM_Basics.Views
{
    public partial class SongControl : UserControl
    {
        ISoundPlayer _SoundPlayer;
        double savedSliderVolumeValue;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SongControl()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
            Unloaded += OnUnloaded;

            TxblPlayedDuration.Text = mediaPlayer.Position.ToString(@"mm\:ss");
            TxblTotalDuration.Text = GetMediaDuration(mediaPlayer.Source.ToString()).ToString(@"mm\:ss");
            sliderTime.Maximum = GetMediaDuration(mediaPlayer.Source.ToString()).TotalSeconds;
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
                //_SoundPlayer.OnSongChanged += _SoundPlayer_OnSongChanged;
            }
        }

        private void _SoundPlayer_OnSongChanged(string songPath)
        {
            if (!string.IsNullOrEmpty(songPath))
            {
                mediaPlayer.Source = null;
                mediaPlayer.Source = new Uri(songPath);
            }
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (_SoundPlayer != null)
                _SoundPlayer.Play -= OnPlay;
        }

        private void OnPlay() => mediaPlayer.Play();
        private void OnPause() => mediaPlayer.Pause();

        private void sliderTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds((int)sliderTime.Value);
            TimeSpan mediaTimeSpan = new TimeSpan(0, 0, 0, 0, (int)timeSpan.TotalMilliseconds);

            TxblPlayedDuration.Text = timeSpan.ToString(@"mm\:ss");
            mediaPlayer.Position = mediaTimeSpan;
        }

        private TimeSpan GetMediaDuration(string filePath)
        {
            using (var shell = ShellObject.FromParsingName(filePath))
            {
                IShellProperty prop = shell.Properties.System.Media.Duration;
                var t = (ulong)prop.ValueAsObject;
                return TimeSpan.FromTicks((long)t);
            }
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Change Volume Button Icon
            PackIcon icon = new PackIcon()
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
                savedSliderVolumeValue = sliderVolume.Value;
                sliderVolume.Value = 0;
            }
            else
            {
                sliderVolume.Value = savedSliderVolumeValue;
            }
        }
    }
}
