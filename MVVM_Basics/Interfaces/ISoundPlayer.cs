using System;

namespace MVVM_Basics.Interfaces
{
    public interface ISoundPlayer
    {
        event Action Play;
        event Action Pause;

        public delegate void ChangeSong(string songPath);
        public event ChangeSong? OnSongChanged;
    }
}
