using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace MVVM_Basics.ViewModels;


public class SearchPageViewModel : ViewModelBase
{
    private readonly IServiceProvider _ServiceProvider;


	private ObservableCollection<Song> _FilteredSongs = new();
	public ObservableCollection<Song> FilteredSongs
    {
		get { return _FilteredSongs; }
		set { _FilteredSongs = value; OnPropertyChanged(); }
	}


	private ObservableCollection<Playlist> _FilteredPlaylists = new();
    public ObservableCollection<Playlist> FilteredPlaylists
    {
		get { return _FilteredPlaylists; }
		set { _FilteredPlaylists = value; OnPropertyChanged(); }
	}


	private ObservableCollection<Song> _FilteredSongsByArtist = new();
    public ObservableCollection<Song> FilteredSongsByArtist
    {
		get { return _FilteredSongsByArtist; }
		set { _FilteredSongsByArtist = value; OnPropertyChanged(); }
	}


	public SearchPageViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;

		Messenger.Default.Register<SearchMessage>(this, HandleSearchMessageReceived);
    }

    private void HandleSearchMessageReceived(SearchMessage message)
    {
		string searchString = (message.SearchString.ToLower());

		using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
		{
			_FilteredSongs.Clear();
			_FilteredPlaylists.Clear();

			FilteredSongs = new(context.Songs
								.Where(s =>
                                    s.Name!.ToLower().Contains(searchString)
								)
								.OrderBy(s => s.Id)
								);
            FilteredPlaylists = new(context.Playlists
								.Where(p =>
                                    p.Name!.ToLower().Contains(searchString)
                                )
								.OrderBy(p => p.Id)
								);

            FilteredSongsByArtist = new((from artist in context.Artists
										join artistSong in context.SongArtists on artist.Id equals artistSong.ArtistId
										join song in context.Songs on artistSong.SongId equals song.Id
										where artist.Name!.Contains(searchString)
										select song)
										.Distinct()
										);
        }

    }


    
}
