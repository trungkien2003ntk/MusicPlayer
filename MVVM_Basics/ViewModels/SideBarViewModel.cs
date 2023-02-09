using MVVM_Basics.Models;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using MVVM_Basics.EventAndCommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MVVM_Basics.ViewModels;

public class SideBarViewModel : ViewModelBase
{
    public ICommand? CloseWindowCommand { get; set; }
    public ICommand? CreatePlaylistCommand { get; set; }


    private readonly IServiceProvider _ServiceProvider;
    private readonly ISharedDataContext _SharedDataContext;


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


    public SideBarViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;
        _SharedDataContext = _ServiceProvider.GetRequiredService<ISharedDataContext>();
        _LoginedUserId = _SharedDataContext.LoginedUserId;
        _Playlists = _SharedDataContext.AllPlaylists;

        InitializeCommands();
    }


    private void InitializeCommands()
    {
        CreatePlaylistCommand = new RelayCommand<UserControl>
                    (
                        (p) => { return true; },
                        (p) =>
                        {
                            int totalPlaylists;
                            using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
                            {
                                totalPlaylists = context.Playlists.Count();
                            }

                            Playlist addingPlaylist = new Playlist()
                            {
                                Name = $"My playlist #{totalPlaylists + 1}",
                                CreatedDate = DateTime.Now,
                                Description = "",
                                UsersId = LoginedUserId,
                            };

                            Playlists.Add(addingPlaylist);

                            using (var context = _ServiceProvider.GetRequiredService<MusicPlayerVpContext>())
                            {
                                context.Playlists.Add(addingPlaylist);
                                context.SaveChanges();
                            }
                        }
                    );

        CloseWindowCommand = new RelayCommand<object>
        (
            (p) => { return true; },
            (p) =>
            {
                Application.Current.MainWindow.Close();
            }
        );
    }
}
