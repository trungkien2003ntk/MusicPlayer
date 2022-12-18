using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Basics.ViewModels
{
    public interface ISoundPlayer
    {
        event Action Play;
        event Action Pause;

        public delegate void ChangeSong(string songPath);
        //public event ChangeSong? OnSongChanged;
    }
}
