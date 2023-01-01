using System;
using System.Linq;
using MVVM_Basics.Models;
using System.Windows.Input;
using MVVM_Basics.Factories;
using System.Windows.Controls;
using MVVM_Basics.EventAndCommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace MVVM_Basics.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Commands
        public ICommand DisplayHomePageCommand { get; set; }
        public ICommand DisplaySearchPageCommand { get; set; }
        public ICommand DisplayPlaylistPageCommand { get; set; }
        #endregion Commands


        #region Fields
        private int? _LoginedUser;
        private PageType _CurrentPageType;
        private ViewModelBase _CurrentViewModel;
        #endregion Fields


        #region Properties
        public int? LoginedUserId { get => _LoginedUser; set { _LoginedUser = value; } }
        public PageType CurrentPageType { get => _CurrentPageType; set { _CurrentPageType = value; OnPropertyChanged(); } }
        public ViewModelBase CurrentViewModel { get => _CurrentViewModel; set { _CurrentViewModel = value; OnPropertyChanged(); } }
        #endregion Properties


        #region Constructors
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainWindowViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

            using (var _Database = new MusicPlayerVpContext())
            {
                LoginedUserId = _Database.Users.First().Id;
            }

            _CurrentViewModel = App.AppHost!.Services.GetRequiredService<HomePageViewModel>();

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
                        CurrentViewModel = App.AppHost!.Services.GetRequiredService<HomePageViewModel>();
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
                       CurrentViewModel = App.AppHost!.Services.GetRequiredService<SearchPageViewModel>();
                       CurrentPageType = PageType.SearchPage;
                   }
               }
            );

            DisplayPlaylistPageCommand = new RelayCommand<Playlist>
            (
               (p) => { return true; },
               (p) =>
               {
                   if (CurrentPageType != PageType.PlaylistPage || p.Id != ((PlaylistViewModel)CurrentViewModel).CurrentPlaylist.Id)
                   {
                       CurrentViewModel = new PlaylistViewModel(p);
                       CurrentPageType = PageType.PlaylistPage;
                   }
               }
            );
        }
        #endregion Constructors
    }
}
