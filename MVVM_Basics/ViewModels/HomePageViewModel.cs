using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.EventAndCommandHandlers;
using MVVM_Basics.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    private const int MAX_SONGS_ON_COLLAPSED = 4;
    private const int MAX_SONGS_ON_SHOWED_ALL = 12;

    public ICommand? ShowAllCommand { get; set; }

    private ObservableCollection<Song> _RecentlyPlayedSongs;
    private readonly IServiceProvider _ServiceProvider;

    public ObservableCollection<Song> RecentlyPlayedSongs
    {
        get { return _RecentlyPlayedSongs; }
        set { _RecentlyPlayedSongs = value; OnPropertyChanged(); }
    }


    public HomePageViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;


        using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
        {
            _RecentlyPlayedSongs = new(
                context.Songs
                    .Where(s => s.LastestPlayDate != null)
                    .OrderByDescending(s => s.LastestPlayDate)
                    .ThenBy(s => s.Id)
                    .Take(MAX_SONGS_ON_COLLAPSED)
            );
        }

        IntializeCommands();
    }

    private void IntializeCommands()
    {
        ShowAllCommand = new RelayCommand<bool>(
            (isCollapsed) => { return true; },
            (isCollapsed) =>
            {
                using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
                {
                    if (isCollapsed)
                    {
                        RecentlyPlayedSongs = new(
                            context.Songs
                                .Where(s => s.LastestPlayDate != null)
                                .OrderByDescending(s => s.LastestPlayDate)
                                .ThenBy(s => s.Id)
                                .Take(MAX_SONGS_ON_SHOWED_ALL)
                        );
                    }
                    else
                    {
                        RecentlyPlayedSongs = new(
                            context.Songs
                                .Where(s => s.LastestPlayDate != null)
                                .OrderByDescending(s => s.LastestPlayDate)
                                .ThenBy(s => s.Id)
                                .Take(MAX_SONGS_ON_COLLAPSED)
                        );
                    }
                }
            });
    }
}
