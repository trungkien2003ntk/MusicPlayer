using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Basics.ViewModels
{
    public interface ISharedDataContext
    {
        Playlist? CurrentOpeningPlaylist { get; set; }
        Song? CurrentPlayingSong { get; set; }
        int LoginedUserId { get; set; }
        ObservableCollection<Playlist> AllPlaylists { get; set; }
        ObservableCollection<Song> SongQueue { get; set; }

        public void AddSongToQueue(Song s);
        public void RemoveSongFromQueue(Song s);
    }


    public class SharedDataContext : ISharedDataContext
    {
        private int _LoginedUserId;
        public int LoginedUserId
        {
            get { return _LoginedUserId; }
            set { _LoginedUserId = value; }
        }


        private Playlist? _CurrentOpeningPlaylist;
        public Playlist? CurrentOpeningPlaylist
        {
            get { return _CurrentOpeningPlaylist; }
            set { _CurrentOpeningPlaylist = value; }
        }


        private Song? _CurrentPlayingSong;
        public Song? CurrentPlayingSong
        {
            get { return _CurrentPlayingSong; }
            set { _CurrentPlayingSong = value; }
        }


        private ObservableCollection<Playlist> _AllPlaylists;
        public ObservableCollection<Playlist> AllPlaylists
        {
            get => _AllPlaylists;
            set { _AllPlaylists = value; }
        }

        private ObservableCollection<Song> _SongQueue;

        public ObservableCollection<Song> SongQueue
        {
            get { return _SongQueue; }
            set { _SongQueue = value; }
        }

        private readonly IServiceProvider _ServiceProvider;

        public SharedDataContext(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;

            using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
            {
                LoginedUserId = context.Users.Where(x => x.Name == "admin").FirstOrDefault()!.Id;
                CurrentPlayingSong = context.Songs.FirstOrDefault();
            }

            CurrentOpeningPlaylist = null;
            _SongQueue = new();

            PopulateCollection();
        }

        private void PopulateCollection()
        {
            using var database = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();
            AllPlaylists = new(database.Playlists
                                .Where(s => s.UsersId == LoginedUserId));

        }

        public void AddSongToQueue(Song song)
        {
            SongQueue.Add(song);
        }

        public void RemoveSongFromQueue(Song song)
        {
            SongQueue.Remove(song);
        }

        
    }
}
