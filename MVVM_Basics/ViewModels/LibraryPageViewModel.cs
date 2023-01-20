using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.EventAndCommandHandlers;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;


public class LibraryPageViewModel : ViewModelBase
{
    private readonly IServiceProvider _ServiceProvider;
    private readonly ISharedDataContext _SharedDataContext;

    
    public ICommand? AddSongToQueueCommand { get; set; }
    public ICommand? ToggleLikedSongCommand { get; set; }


    private ObservableCollection<Song> _AllSongs;
    public ObservableCollection<Song> AllSongs
    {
        get { return _AllSongs; }
        set { _AllSongs = value; OnPropertyChanged(); }
    }


    private ObservableCollection<string> _AllPlaylists;
    public ObservableCollection<string> AllPlaylistNames
    {
        get { return _AllPlaylists; }
        set { _AllPlaylists = value; OnPropertyChanged(); }
    }


    public LibraryPageViewModel(IServiceProvider serviceProvider, ISharedDataContext sharedDataContext)
    {
        _ServiceProvider = serviceProvider;
        _SharedDataContext = sharedDataContext;
        _AllSongs = new();
        _AllPlaylists = new();

        Messenger.Default.Register<AddSongToQueueMessage>(this, HandleAddSongToQueue);
        PopulateCollection();
        InitializeCommands();
    }

    private void HandleAddSongToQueue(AddSongToQueueMessage message)
    {
        var songToAdd = message.Song;

        if (songToAdd != null) 
        {
            _SharedDataContext.AddSongToQueue(songToAdd);
        }
    }

    private void PopulateCollection()
    {
        using var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();

        foreach (var song in context.Songs)
        {
            AllSongs.Add(song);
        }

        foreach (var playlist in context.Playlists)
        { 
            AllPlaylistNames.Add(playlist.Name!);
        }
    }
    
    private void InitializeCommands()
    {
        AddSongToQueueCommand = new RelayCommand<Song>
            (
                (s) => { return true; },
                (s) =>
                {
                    _SharedDataContext.AddSongToQueue(s);
                }
            );

        ToggleLikedSongCommand = new RelayCommand<Song>
            (
                (s) => { return true; },
                (s) =>
                {
                    using var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();

                    var likedSongToRemove = context.LikedSongs.Where(ls => ls.UsersId == _SharedDataContext.LoginedUserId && ls.SongId == s.Id).FirstOrDefault();
                    if (likedSongToRemove != null) 
                    {
                        context.LikedSongs.Remove(likedSongToRemove);
                    }
                    else
                    {
                        var likedSongToAdd = new LikedSong
                        {
                            UsersId = _SharedDataContext.LoginedUserId,
                            SongId = s.Id,
                        };

                        context.LikedSongs.Add(likedSongToAdd);
                    }

                    OnPropertyChanged(nameof(s));
                    context.SaveChanges();

                    context.Dispose();
                }
            );
    }


    public async void AddSongToLibrary(string filePath)
    {
        using var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();

        if (File.Exists(filePath))
        {
            // Get the artist name from the mp3 file
            string[] artistNames = Mp3Helper.GetArtistNames(filePath);

            // Add artist to library if it doesn't already exist
            for (int i = 0; i < artistNames.Length; i++)
            {
                var artist = new Artist { Name = artistNames[i] };

                if (!context.Artists.Any(a => a.Name == artist.Name))
                    context.Artists.Add(artist);
            }
            await context.SaveChangesAsync();


            // Add song to library if it doesn't already exist
            Song song = new Song
            {
                Name = Mp3Helper.GetTitle(filePath),
                PcLink = filePath,
                Plays = 0,
            };

            if (!context.Songs.Any(s => s.Name == song.Name))
            {
                context.Songs.Add(song);
                await context.SaveChangesAsync();

                // Link song to artist
                foreach (string artistName in artistNames)
                {
                    var songArtist = new SongArtist
                    {
                        SongId = song.Id,
                        ArtistId = context.Artists.Where(a => a.Name == artistName).FirstOrDefault()!.Id,
                    };

                    if (!context.SongArtists.Any(sa => sa.SongId == songArtist.SongId && sa.ArtistId == songArtist.ArtistId))
                        context.SongArtists.Add(songArtist);
                }
                await context.SaveChangesAsync();

                // Add song to AllSongs for display in the UI
                AllSongs.Add(song);
            }
        }
    }
    
    public override void Cleanup()
        {
        Messenger.Default.Unregister(this);
    }
}
