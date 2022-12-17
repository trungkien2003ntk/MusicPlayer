using MaterialDesignThemes.Wpf.Transitions;
using MVVM_Basics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_Basics.Views
{
    /// <summary>
    /// Interaction logic for TrackControl.xaml
    /// </summary>
    public partial class TrackControl : UserControl
    {
        ISoundPlayer _SoundPlayer;

        public TrackControl()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
            Unloaded += OnUnloaded;
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
            TimeSpan ts = new TimeSpan(0,0,0,0,(int)sliderTime.Value);

            TxblPlayedDuration.Text = ts.ToString("mm:ss");
            mediaPlayer.Position = ts;
        }
    }
}
