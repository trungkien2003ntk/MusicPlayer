using MVVM_Basics.Models;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using MVVM_Basics.EventAndCommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace MVVM_Basics.ViewModels;

public class SideBarViewModel : ViewModelBase
{
    public ICommand? CreatePlaylistCommand { get; set; }


    private readonly IServiceProvider _ServiceProvider;


    private int? _LoginedUserId;
    public int? LoginedUserId 
    { 
        get => _LoginedUserId;
        set { _LoginedUserId = value; OnPropertyChanged(); }
    }


    private ObservableCollection<Playlist> _Playlists;
    public ObservableCollection<Playlist> Playlists
    { 
        get => _Playlists; 
        set { _Playlists = value; OnPropertyChanged(); }
    }

    //public SideBarViewModel()
    //{

    //}

    public SideBarViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;

        LoginedUserId = _ServiceProvider.GetRequiredService<ISharedDataContext>().LoginedUserId;
        _Playlists = _ServiceProvider.GetRequiredService<ISharedDataContext>().AllPlaylists;

        //PopulateCollection();
        InitializeCommands();
    }

    

    //private void PopulateCollection()
    //{
    //    using var database = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>();
    //    var playlists = database.Playlists
    //                .Where(s => s.UsersId == LoginedUserId);

    //    foreach (Playlist playlist in playlists)
    //        Playlists.Add(playlist);
    //}

    private void InitializeCommands()
    {
        CreatePlaylistCommand = new RelayCommand<UserControl>
                    (
                        (p) => { return true; },
                        (p) =>
                        {
                            Playlists.Add(new Playlist() { Name = "My playlist", CreatedDate = DateTime.Now, Description = "", UsersId = LoginedUserId });
                            OnPropertyChanged(nameof(Playlists));
                        }
                    );
    }
}
