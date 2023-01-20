using Id3;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Basics.ViewModels;

public class LikedSongsPageViewModel : ViewModelBase
{
    private readonly ISharedDataContext _SharedDataContext;
    private readonly IServiceProvider _ServiceProvider;
    private ObservableCollection<Song>? _LikedSongsList;
    public ObservableCollection<Song>? LikedSongsList
    {
        get => _LikedSongsList;
        set { _LikedSongsList = value; OnPropertyChanged(); }
    }


    private User _LoginedUser;

    public User LoginedUser
    {
        get { return _LoginedUser; }
        set { _LoginedUser = value; OnPropertyChanged(); }
    }


    private int? _TotalSongs;
    public int? TotalSongs
    {
        get => _TotalSongs;
        set { _TotalSongs = value; OnPropertyChanged(); }
    }


    public LikedSongsPageViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;

        _SharedDataContext = _ServiceProvider.GetRequiredService<ISharedDataContext>();

        PopulateCollection();

        TotalSongs = LikedSongsList!.Count;

        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            _LoginedUser = context.Users.Where(u => u.Id == _SharedDataContext.LoginedUserId).FirstOrDefault()!;
        }
    }

    private void PopulateCollection()
    {
        LikedSongsList = new();

        using var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();

        var likedSongIds = new List<int>();
        foreach (var likedSong in context.LikedSongs)
        {
            likedSongIds.Add(likedSong.SongId);
        }

        foreach (var likedSongId in likedSongIds)
        {
            LikedSongsList.Add(context.Songs.FirstOrDefault(s => s.Id == likedSongId)!);
        }
    }
}
