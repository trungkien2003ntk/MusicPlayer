using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.EventAndCommandHandlers;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;


public class LibraryPageViewModel : ViewModelBase
{
    private readonly IServiceProvider _ServiceProvider;
    private readonly ISharedDataContext _SharedDataContext;


    private ObservableCollection<Song> _AllSongs;
    public ObservableCollection<Song> AllSongs
    {
        get { return _AllSongs; }
        set { _AllSongs = value; OnPropertyChanged(); }
    }


    public LibraryPageViewModel(IServiceProvider serviceProvider, ISharedDataContext sharedDataContext)
    {
        _ServiceProvider = serviceProvider;
        _SharedDataContext = sharedDataContext;
        _AllSongs = new();
        

        PopulateCollection();

        Messenger.Default.Register<AddSongToQueueMessage>(this, HandleAddSongToQueue);
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

        AllSongs = new(AllSongs.Reverse());
    }
    
    
    public async void AddSongToLibrary(string filePath)
    {
        using var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();

        if (File.Exists(filePath))
        {
            // Get the artist name from the mp3 file
            List<string> artistNames = Mp3Helper.GetArtistNames(filePath).ToList();

            // Add artist to library if it doesn't already exist
            foreach (string artistName in artistNames)
            { 
                var artist = new Artist { Name = artistName };

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
                AllSongs.Insert(0, song);
            }
        }
    }
    
    public override void Cleanup()
    {
        Messenger.Default.Unregister(this);
    }
}
