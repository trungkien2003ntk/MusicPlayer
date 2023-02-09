using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.EventAndCommandHandlers;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using MVVM_Basics.Services;
using MVVM_Basics.Views.MessageAndNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    private readonly ISharedDataContext _SharedDataContext;
    private readonly IServiceProvider _ServiceProvider;
    private readonly IPlaylistManager _PlaylistManager;

    public ICommand? AddSongToQueueCommand { get; set; }
    public ICommand? RemoveSongFromQueueCommand { get; set; }
    public ICommand? ToggleLikedSongCommand { get; set; }
    public ICommand? AddSongToPlaylistCommand { get; set; }


    private ObservableCollection<Playlist> _AllPlaylists;
    public ObservableCollection<Playlist> AllPlaylists
    {
        get { return _AllPlaylists; }
        set { _AllPlaylists = value; OnPropertyChanged(); }
    }


    private ObservableCollection<string> _AllPlaylistNames;
    public ObservableCollection<string> AllPlaylistNames
    {
        get { return _AllPlaylistNames; }
        set { _AllPlaylistNames = value; OnPropertyChanged(); }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ViewModelBase()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _ServiceProvider = App.AppHost!.Services.GetRequiredService<IServiceProvider>(); // -> can be passed through constructor
        _SharedDataContext = _ServiceProvider.GetRequiredService<ISharedDataContext>();
        _PlaylistManager =  _ServiceProvider.GetRequiredService<IPlaylistManager>();

        PopulateCollections();
        InitializeCommands();
    }

    private void PopulateCollections()
    {
        _AllPlaylists = _SharedDataContext.AllPlaylists;
        _AllPlaylistNames = _PlaylistManager.AllPlaylistsNames;
    }

    private void InitializeCommands()
    {
        AddSongToQueueCommand = new RelayCommand<object>
            (
                (selectedItems) => { return true; },
                (selectedItems) =>
                {
                    if (selectedItems is System.Collections.IList selectedSongList) 
                    {
                        if (selectedSongList.Count > 0)
                        {
                            List<Song> songsForQueue = new(selectedSongList.Cast<Song>().ToList());

                            foreach (Song songForQueue in songsForQueue)
                                _SharedDataContext.AddSongToQueue(songForQueue);
                        }
                    }
                }
            );
        
        RemoveSongFromQueueCommand = new RelayCommand<object>
            (
                (selectedItems) => { return true; },
                (selectedItems) =>
                {
                    if (selectedItems is System.Collections.IList selectedSongList) 
                    {
                        if (selectedSongList.Count > 0)
                        {
                            List<Song> songsForQueue = new(selectedSongList.Cast<Song>().ToList());

                            foreach (Song songForQueue in songsForQueue)
                                _SharedDataContext.RemoveSongFromQueue(songForQueue);
                        }
                    }
                }
            );

        ToggleLikedSongCommand = new RelayCommand<object>
            (
                (selectedItems) => { return true; },
                (selectedItems) =>
                {
                    using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
                    {
                        if (selectedItems is System.Collections.IList selectedSongList)
                        {
                            if (selectedSongList.Count > 0)
                            {
                                List<Song> songs = new(selectedSongList.Cast<Song>().ToList());

                                bool isRemoving = IsRemovingFromLikedSongs(songs, context);

                                if (isRemoving)
                                {
                                    foreach (Song song in songs)
                                    {
                                        RemoveSongFromLikedSongs(context, song);
                                    }
                                }
                                else
                                {
                                    foreach (Song song in songs)
                                    {
                                        AddSongToLikedSongs(context, song);
                                    }
                                }

                                context.SaveChanges();
                            }
                        }
                    }
                }
            );

        AddSongToPlaylistCommand = _PlaylistManager.AddSongToPlaylistCommand;
    }

    private void AddSongToLikedSongs(MusicPlayerVpContext context, Song song)
    {
        if (!IsSongAlreadyInLikedSongs(context, song))
        {
            LikedSong likedSongToAdd = new LikedSong
            {
                UsersId = _SharedDataContext.LoginedUserId,
                SongId = song.Id,
            };

            // Add the LikedSong to the context
            context.LikedSongs.Add(likedSongToAdd);

            // Notify the user that the song was added to the LikedSongs playlist
            Messenger.Default.Send(new ShowNotificationMessage(NotificationType.Added, TargetType.LikedSongs));
        }
    }

    private void RemoveSongFromLikedSongs(MusicPlayerVpContext context, Song song)
    {
        LikedSong removingSong = GetLikedSong(context, song)!;

        context.LikedSongs.Remove(removingSong);

        // This method is for other class's overriding
        OnSongRemoved(removingSong);

        // Notify the user that the song was removed from the LikedSongs playlist
        Messenger.Default.Send(new ShowNotificationMessage(NotificationType.Removed, TargetType.LikedSongs));
    }

    protected virtual void OnSongRemoved(LikedSong likedSongToRemove) { }

    private LikedSong? GetLikedSong(MusicPlayerVpContext context, Song song)
    {
        return context.LikedSongs
            .FirstOrDefault(ls => ls.UsersId == _SharedDataContext.LoginedUserId && ls.SongId == song.Id);
    }

    private bool IsRemovingFromLikedSongs(List<Song> songs, MusicPlayerVpContext context)
    {
        foreach (var song in songs)
        {
            var likedSong = context.LikedSongs
                .Where(ls => ls.UsersId == _SharedDataContext.LoginedUserId && ls.SongId == song.Id)
                .FirstOrDefault();

            if (likedSong == null)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsSongAlreadyInLikedSongs(MusicPlayerVpContext context, Song song)
    {
        return GetLikedSong(context, song) != null;
    }


    // Implement InotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void Cleanup() { }
}
