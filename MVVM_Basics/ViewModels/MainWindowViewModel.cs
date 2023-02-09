using System;
using MVVM_Basics.Models;
using System.Windows.Input;
using System.Windows.Controls;
using MVVM_Basics.EventAndCommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MVVM_Basics.Helpers;

namespace MVVM_Basics.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand? DisplayHomePageCommand { get; set; }
    public ICommand? DisplaySearchPageCommand { get; set; }
    public ICommand? DisplayPlaylistPageCommand { get; set; }
    public ICommand? DisplayLibraryPageCommand { get; set; }
    public ICommand? DisplayQueuePageCommand { get; set; }
    public ICommand? DisplayLikedSongsPageCommand { get; set; }



    private readonly IServiceProvider _ServiceProvider;
    private readonly ISharedDataContext _SharedDataContext;


    private PageType _CurrentPageType;
    public PageType CurrentPageType
    {
        get { return _CurrentPageType; }
        set { _CurrentPageType = value; }
    }


    private Visibility _ContentControlVisibility;

    public Visibility ContentControlVisibility
    {
        get { return _ContentControlVisibility; }
        set { _ContentControlVisibility = value; OnPropertyChanged(); }
    }



    private ViewModelBase _CurrentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get { return _CurrentViewModel; }
        set { _CurrentViewModel = value; OnPropertyChanged(); }
    }

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _ServiceProvider = serviceProvider;
        _SharedDataContext = _ServiceProvider.GetRequiredService<ISharedDataContext>();
        _CurrentViewModel = _ServiceProvider.GetRequiredService<HomePageViewModel>();

        InitializeCommands();
    }

    private void InitializeCommands()
    {
        DisplayHomePageCommand = new RelayCommand<UserControl>
        (
            (p) => { return true; },
            (p) =>
            {
                if (CurrentPageType != PageType.HomePage)
                {
                    CurrentViewModel.Cleanup();
                    CurrentViewModel = _ServiceProvider.GetRequiredService<HomePageViewModel>();
                    CurrentPageType = PageType.HomePage;

                    ContentControlVisibility = Visibility.Visible;
                }
            }
        );

        DisplaySearchPageCommand = new RelayCommand<UserControl>
        (
           (p) => { return true; },
           (p) =>
           {
               if (CurrentPageType != PageType.SearchPage)
               {
                   CurrentViewModel.Cleanup();
                   CurrentViewModel = _ServiceProvider.GetRequiredService<SearchPageViewModel>();
                   CurrentPageType = PageType.SearchPage;
                   
                   ContentControlVisibility = Visibility.Visible;
               }
           }
        );

        DisplayPlaylistPageCommand = new RelayCommand<Playlist>
        (
           (p) => { return true; },
           (p) =>
           {
               if (CurrentPageType != PageType.PlaylistPage || p.Id != ((PlaylistPageViewModel)CurrentViewModel)?.CurrentPlaylist?.Id)
               {
                   _SharedDataContext.CurrentOpeningPlaylist = p;
                   CurrentViewModel = _ServiceProvider.GetRequiredService<PlaylistPageViewModel>();
                   ((PlaylistPageViewModel)CurrentViewModel).UpdateData(p);
                   CurrentPageType = PageType.PlaylistPage;
                   Messenger.Default.Send(new PlaylistChangedMessage((PlaylistPageViewModel)CurrentViewModel));
               }
           }
        );

        DisplayLibraryPageCommand = new RelayCommand<UserControl>
        (
           (p) => { return true; },
           (p) =>
           {
               if (CurrentPageType != PageType.LibraryPage)
               {
                   CurrentViewModel.Cleanup();

                   CurrentViewModel = _ServiceProvider.GetRequiredService<LibraryPageViewModel>();
                   CurrentPageType = PageType.LibraryPage;
               }
           }
        );

        DisplayQueuePageCommand = new RelayCommand<UserControl>
        (
           (p) => { return true; },
           (p) =>
           {
               if (CurrentPageType != PageType.QueuePage)
               {
                   CurrentViewModel.Cleanup();
                   CurrentViewModel = _ServiceProvider.GetRequiredService<QueuePageViewModel>();
                   CurrentPageType = PageType.QueuePage;
               }
           }
        );

        DisplayLikedSongsPageCommand = new RelayCommand<UserControl>
        (
           (p) => { return true; },
           (p) =>
           {
               if (CurrentPageType != PageType.LikedSongsPage)
               {
                   CurrentViewModel.Cleanup();
                   CurrentViewModel = _ServiceProvider.GetRequiredService<LikedSongsPageViewModel>();
                   CurrentPageType = PageType.LikedSongsPage;
               }
           }
        );
    }
}
