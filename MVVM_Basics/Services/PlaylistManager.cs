using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.EventAndCommandHandlers;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_Basics.Services;

public interface IPlaylistManager
{
    ICommand AddSongToPlaylistCommand { get; set; }
    ObservableCollection<string> AllPlaylistsNames { get; set; }
}

public class PlaylistManager : IPlaylistManager
{
    private readonly IServiceProvider _ServiceProvider;

    public ICommand AddSongToPlaylistCommand { get; set; }
    public ObservableCollection<string> AllPlaylistsNames { get; set; }

    public PlaylistManager(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;
        
        PopulateCollections();
        InitializeCommands();
    }

    private void PopulateCollections()
    {
        AllPlaylistsNames = new ObservableCollection<string>();

        // populate AllPlaylistsNames with data from the database
        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            var playlists = context.Playlists.ToList();

            foreach (var playlist in playlists)
            {
                AllPlaylistsNames.Add(playlist.Name!);
            }
        }
    }

    private void InitializeCommands()
    {
        AddSongToPlaylistCommand = new RelayCommand<object>(
                    (parameters) => { return true; },
                    (parameters) =>
                    {
                        // casting from MultiBindings
                        var values = (object[])parameters;
                        int playlistId = (int)values[0];
                        object selectedItems = values[1];

                        if (selectedItems is System.Collections.IList selectedSongList)
                        {
                            if (selectedSongList.Count > 0)
                            {
                                List<Song> songs = new(selectedSongList.Cast<Song>().ToList());

                                foreach (Song song in songs)
                                {
                                    int songId = song.Id;

                                    AddSongToPlaylist(playlistId, songId);
                                }
                            }
                        }
                    });
    }

    private void AddSongToPlaylist(int playlistId, int songId)
    {
        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            var playlist = context.Playlists.FirstOrDefault(p => string.Equals(p.Id, playlistId));

            if (playlist != null)
            {
                var playlistSong = new PlaylistSong
                {
                    SongId = songId,
                    PlaylistId = playlist.Id,
                    AddedDate = DateTime.Now,
                };

                if (!SongExistsInPlaylist(context, playlistSong))
                {
                    AddSongToPlaylist(context, playlistSong);
                }
                else
                {
                    ShowDuplicateSongConfirmationMessage(playlist.Name!, context, playlistSong);
                }

                context.SaveChanges();
            }
        }
    }

    private bool SongExistsInPlaylist(MusicPlayerVpContext context, PlaylistSong playlistSong)
    {
        return context.PlaylistSongs.Any(ps => ps.PlaylistId == playlistSong.PlaylistId && ps.SongId == playlistSong.SongId);
    }

    private void ShowDuplicateSongConfirmationMessage(string playlistName, MusicPlayerVpContext context, PlaylistSong playlistSong)
    {
        Messenger.Default.Send(new SongAlreadyInPlaylistMessage(playlistName, result =>
        {
            if (result == true)
            {
                AddSongToPlaylist(context, playlistSong);
            }
        }));
    }

    private void AddSongToPlaylist(MusicPlayerVpContext context, PlaylistSong playlistSong)
    {
        context.PlaylistSongs.Add(playlistSong);

        Messenger.Default.Send(new ShowNotificationMessage(Views.MessageAndNotification.NotificationType.AddedPlaylist, Views.MessageAndNotification.TargetType.Playlist));
    }
}
