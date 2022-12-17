using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MVVM_Basics.ViewModels
{
    public interface ISoundPlayer
    {
        event Action Play;
        event Action Pause;
    }

    public class TrackControlViewModel : BaseViewModel, ISoundPlayer
    {
        public event Action Play;

        

        public event Action Pause;

        private bool _IsPlaying;
        public bool IsPlaying
        {
            get => _IsPlaying;
            set
            {
                _IsPlaying = value;
                if (CurrentPlayingSongViewModel.HasSongLoaded)
                    if (_IsPlaying)
                        Play();
                    else
                        Pause();
            }
        }

        private Uri _Source;
        public Uri Source { get => _Source; set { _Source = value; OnPropertyChanged(); } }

        public TrackControlViewModel()
        {
            _IsPlaying = false;

            if (CurrentPlayingSongViewModel.HasSongLoaded)
            {
                _Source = new Uri(CurrentPlayingSongViewModel.CurrentPlayingSong.PcLink);
            }
        }
    }
}
