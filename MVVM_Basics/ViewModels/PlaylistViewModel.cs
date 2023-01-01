﻿using Id3;
using MVVM_Basics.Models;
using System;
using System.Linq;
using System.Text;

namespace MVVM_Basics.ViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        private Playlist _CurrentPlaylist;
        public Playlist CurrentPlaylist { get => _CurrentPlaylist; set { _CurrentPlaylist= value; OnPropertyChanged(); } }

        private int _TotalSongs;
        public int TotalSongs { get => _TotalSongs; set { _TotalSongs = value; OnPropertyChanged(); } }

        private string _TotalDuration;
        public string TotalDuration { get => _TotalDuration; set { _TotalDuration = value; OnPropertyChanged(); } }

        public PlaylistViewModel()
        {

        }
        public PlaylistViewModel(Playlist playlist)
        {
            CurrentPlaylist = playlist;
            TotalSongs = CurrentPlaylist.PlaylistSongs.Count;
            
            _TotalDuration = GetTotalDuration(CurrentPlaylist).ToString("%h' hr '%m' min'");
        }

        private TimeSpan GetTotalDuration(Playlist currentPlaylist)
        {
            TimeSpan result = new TimeSpan(0, 0, 0);

            foreach (PlaylistSong playlistSong in currentPlaylist.PlaylistSongs)
            {
                using (var database = new MusicPlayerVpContext())
                {
                    var MusicPath = database.Songs.Where(s => s.Id == playlistSong.SongId).First().PcLink;
                    
                    using (var mp3 = new Mp3(MusicPath))
                    {
                        Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);

                        result += tag.Length.Value;
                    }
                }
            }

            return result;
        }
    }
}
