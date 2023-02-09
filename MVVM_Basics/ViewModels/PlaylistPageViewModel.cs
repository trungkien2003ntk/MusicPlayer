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
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;

public class PlaylistPageViewModel : ViewModelBase
{
    private readonly ISharedDataContext _SharedDataContext;
    private readonly IServiceProvider _ServiceProvider;

    public ICommand? RemoveSongFromPlaylistCommand { get; set; }
    public ICommand? AddAllPlaylistSongsToQueue { get; set; }
    public ICommand? RemoveCurrentPlaylistCommand { get; set; }


    private Playlist? _CurrentPlaylist;
    public Playlist? CurrentPlaylist 
    { 
        get => _CurrentPlaylist; 
        set { _CurrentPlaylist= value; OnPropertyChanged(); }
    }


    private int? _TotalSongs;
    public int? TotalSongs 
    { 
        get => _TotalSongs; 
        set { _TotalSongs = value; OnPropertyChanged(); } 
    }


    private string? _TotalDuration;
    public string? TotalDuration
    { 
        get => _TotalDuration; 
        set { _TotalDuration = value; OnPropertyChanged(); } 
    }


    private ObservableCollection<Song> _PlaylistSongs = null!;
    public ObservableCollection<Song> PlaylistSongs
    {
        get { return _PlaylistSongs; }
        set { _PlaylistSongs = value; OnPropertyChanged(); }
    }


    public PlaylistPageViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;
        _SharedDataContext = _ServiceProvider.GetRequiredService<ISharedDataContext>();

        UpdateData(_SharedDataContext.CurrentOpeningPlaylist);
        InitializeCommands();
    }


    public void UpdateData(Playlist? updatePlaylistData)
    {
        CurrentPlaylist = updatePlaylistData;

        TotalSongs = GetTotalSongs(_CurrentPlaylist);

        TotalDuration = GetTotalDuration(_CurrentPlaylist);

        PopulateCollection();
    }

    
    private void InitializeCommands()
    {
        RemoveSongFromPlaylistCommand = new RelayCommand<ListView>(
            (listView) => { return true; },
            (listView) =>
            {
                if (listView.SelectedItems.Count > 0)
                {
                    List<int> selectedIndices = GetAllSelectedItemIndicesFromListView(listView);

                    RemoveSelectedSongsFromLocalPlaylist(selectedIndices, PlaylistSongs);

                    using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
                    {
                        RemoveSongsFromDatabasePlaylist(listView, context, selectedIndices);
                    }
                }
            });

        AddAllPlaylistSongsToQueue = new RelayCommand<Playlist>(
            (p) => { return true; },
            (p) =>
            {
                _SharedDataContext.SongQueue = new(PlaylistSongs);
                
                if (TotalSongs > 0)
                {
                    Messenger.Default.Send(new ChangeSongMessage(_SharedDataContext.SongQueue[0]));

                    _SharedDataContext.SongQueue.RemoveAt(0);
                }
            });

        RemoveCurrentPlaylistCommand = new RelayCommand<object>(
            (p) => { return true; },
            (p) =>
            {
                using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
                {
                    int currentPlaylistId = CurrentPlaylist.Id;

                    List<PlaylistSong> playlistSongsToRemove = new(context.PlaylistSongs
                                                                .Where(ps =>
                                                                    ps.PlaylistId == currentPlaylistId
                                                                ).ToList());

                    foreach (PlaylistSong playlistSongToRemove in playlistSongsToRemove)
                    {
                        context.PlaylistSongs.Remove(playlistSongToRemove);
                    }

                    context.Playlists.Remove(CurrentPlaylist);
                    _SharedDataContext.AllPlaylists.Remove(CurrentPlaylist);
                    _SharedDataContext.CurrentPlayingSong = null;

                    // return to HomePage
                    _ServiceProvider.GetRequiredService<MainWindowViewModel>().DisplayHomePageCommand!.Execute(null);

                    context.SaveChanges();
                }
            });
    }


    private int? GetTotalSongs(Playlist? currentPlaylist)
    {
        if (currentPlaylist == null) return null;

        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            return context.PlaylistSongs
                    .Where(ps =>
                        ps.PlaylistId == currentPlaylist.Id
                    )
                    .Count();
        }
    }


    private string GetTotalDuration(Playlist? currentPlaylist)
    {
        if (currentPlaylist == null)
            return string.Empty;

        TimeSpan result = new(0, 0, 0);

        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            List<PlaylistSong> playlistSongs = context.PlaylistSongs
                                                .Where(ps =>
                                                    ps.PlaylistId == currentPlaylist.Id
                                                )
                                                .ToList();

            foreach (PlaylistSong playlistSong in playlistSongs)
            {
                var MusicPath = context.Songs
                                .Where(s =>
                                    s.Id == playlistSong.SongId
                                )
                                .First().PcLink;

                if (File.Exists(MusicPath))
                    result += TimeSpan.FromSeconds(Mp3Helper.GetDuration(MusicPath));
            }
        }

        if (result.Hours > 0)
            return result.ToString("%h' hr '%m' min'");
        
        if (result.Minutes > 0)
            return result.ToString("%m' min '%s' sec'");

        return result.ToString("%s' sec'");
    }


    private void PopulateCollection()
    {
        PlaylistSongs = new();

        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            List<PlaylistSong> playlistSongs = context.PlaylistSongs
                                                .Where(ps =>
                                                    ps.PlaylistId == CurrentPlaylist!.Id
                                                )
                                                .ToList();
            List<DateTime> songAddedDates = context.PlaylistSongs
                                .Where(ps =>
                                    ps.PlaylistId == CurrentPlaylist!.Id
                                )
                                .Select(ps =>
                                    ps.AddedDate
                                )
                                .ToList();

            foreach (PlaylistSong playlistSong in playlistSongs)
            {
                PlaylistSongs.Add(
                    context.Songs
                    .Where(s =>
                        s.Id == playlistSong.SongId
                    )
                    .FirstOrDefault()!
                );
            }

            var sortedPlaylistSongs = PlaylistSongs
                                        .Zip(songAddedDates, (song, addedDate) => (Song: song, AddedDate: addedDate))
                                        .OrderBy(tuple => tuple.AddedDate)
                                        .Select(tuple => tuple.Song)
                                        .ToList();

            PlaylistSongs = new(sortedPlaylistSongs);
        }
    }


    private List<int> GetAllSelectedItemIndicesFromListView(ListView listView)
    {
        List<int> result = new();

        for (int i = 0; i < listView.Items.Count; i++)
        {
            ListViewItem? listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

            if (listViewItem!.IsSelected)
                result.Add(i);
        }

        return result;
    }


    private void RemoveSelectedSongsFromLocalPlaylist(List<int> selectedIndices, ObservableCollection<Song> playlistSongs)
    {
        for (int i = selectedIndices.Count - 1; i >= 0; i--)
        {
            playlistSongs.RemoveAt(selectedIndices[i]);
        }
    }


    private void RemoveSongsFromDatabasePlaylist(ListView listView, MusicPlayerVpContext context, List<int> selectedIndices)
    {
        List<Song> removingSongs = GetAllSelectedItemsFromListView(listView, selectedIndices);
        List<DateTime> songAddedDates = GetAllSelectedItemAddedDatesFromListView(listView, selectedIndices);

        for (int i = 0; i < removingSongs.Count; i++)
        {
            var playlistSongPairToRemove = new PlaylistSong()
            {
                PlaylistId = CurrentPlaylist!.Id,
                SongId = removingSongs[i].Id,
                AddedDate = songAddedDates[i],
            };

            RemoveSongFromPlaylist(context, playlistSongPairToRemove);
        }

        context.SaveChanges();
    }


    private List<Song> GetAllSelectedItemsFromListView(ListView listView, List<int> selectedIndices)
    {
        List<Song> result = new();

        foreach (int selectedIndex in selectedIndices)
        {
            //ListViewItem? listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(selectedIndex) as ListViewItem;

            result.Add((Song)listView.Items[selectedIndex]);
        }

        return result;
    }

    
    private List<DateTime> GetAllSelectedItemAddedDatesFromListView(ListView listView, List<int> selectedIndices)
    {
        List<DateTime> result = new();
        
        foreach (int selectedIndex in selectedIndices)
        { 
            ListViewItem? listViewItem = listView.ItemContainerGenerator.ContainerFromIndex(selectedIndex) as ListViewItem;

            result.Add((DateTime)listViewItem!.Tag);
        }

        return result;
    }


    private void RemoveSongFromPlaylist(MusicPlayerVpContext context, PlaylistSong playlistSong)
    {
        context.PlaylistSongs.Remove(playlistSong);
    }
}
