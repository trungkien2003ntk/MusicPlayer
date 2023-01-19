using Microsoft.Extensions.DependencyInjection;
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
        ObservableCollection<Song> QueueSongs { get; set; }
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

        private ObservableCollection<Song> _QueueSongs;

        public ObservableCollection<Song> QueueSongs
        {
            get { return _QueueSongs; }
            set { _QueueSongs = value; }
        }


        private MusicPlayerVpContext _Database;
        private readonly IServiceProvider _ServiceProvider;

        public SharedDataContext(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
            _Database = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();
            LoginedUserId = _Database.Users.Where(x => x.Name == "admin").FirstOrDefault()!.Id;
            CurrentOpeningPlaylist = null;
            _QueueSongs = new();
            _AllPlaylists = new();


            // Establist CurrentPlayingSong
            CurrentPlayingSong = new Song()
            {
                Id = 100,
                PcLink = "C:\\Users\\ADMIN\\Downloads\\Music\\Songs\\Tăng Duy Tân - Bên Trên Tầng Lầu - Official Lyric Video.mp3",
            };

            PopulateCollection();
        }

        private void PopulateCollection()
        {
            using var database = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();
            var playlists = database.Playlists
                        .Where(s => s.UsersId == LoginedUserId);

            foreach (Playlist playlist in playlists)
                AllPlaylists.Add(playlist);
        }

    }
}
