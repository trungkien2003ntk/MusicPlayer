using System;
using MVVM_Basics.Models;
using System.Windows.Input;
using System.Windows.Controls;
using MVVM_Basics.EventAndCommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace MVVM_Basics.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand? DisplayHomePageCommand { get; set; }
    public ICommand? DisplaySearchPageCommand { get; set; }
    public ICommand? DisplayPlaylistPageCommand { get; set; }
    public ICommand? DisplayLibraryPageCommand { get; set; }
    public ICommand? DisplayQueuePageCommand { get; set; }



    private readonly IServiceProvider _ServiceProvider;
    private readonly ISharedDataContext _SharedDataContext;


    private PageType _CurrentPageType;
    public PageType CurrentPageType
    {
        get { return _CurrentPageType; }
        set { _CurrentPageType = value; }
    }


    private ViewModelBase _CurrentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get { return _CurrentViewModel; }
        set { _CurrentViewModel = value; OnPropertyChanged(); }
    }

    public MainWindowViewModel()
    {

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
                    CurrentViewModel = _ServiceProvider.GetRequiredService<HomePageViewModel>();
                    CurrentPageType = PageType.HomePage;
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
                   CurrentViewModel = _ServiceProvider.GetRequiredService<SearchPageViewModel>();
                   CurrentPageType = PageType.SearchPage;
               }
           }
        );

        DisplayPlaylistPageCommand = new RelayCommand<Playlist>
        (
           (p) => { return true; },
           (p) =>
           {
               if (CurrentPageType != PageType.PlaylistPage || p.Id != ((PlaylistPageViewModel)CurrentViewModel).CurrentPlaylist?.Id)
               {
                   _SharedDataContext.CurrentOpeningPlaylist = p;
                   CurrentViewModel = _ServiceProvider.GetRequiredService<PlaylistPageViewModel>();
                   CurrentPageType = PageType.PlaylistPage;
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
                   CurrentViewModel = _ServiceProvider.GetRequiredService<QueuePageViewModel>();
                   CurrentPageType = PageType.QueuePage;
               }
           }
        );
    }
}
