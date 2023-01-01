using MVVM_Basics.Models;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using MVVM_Basics.EventAndCommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace MVVM_Basics.ViewModels
{
    public class SideBarViewModel : ViewModelBase
    {
        #region Commands
        public ICommand CreatePlaylistCommand { get; set; }
        #endregion Commands

        public int? LoginedUserId { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set ; }

        public SideBarViewModel()
        {
            LoginedUserId = App.AppHost!.Services.GetRequiredService<MainWindowViewModel>().LoginedUserId;

            Playlists = new ObservableCollection<Playlist>();

            PopulateCollection();

            CreatePlaylistCommand = new RelayCommand<UserControl>
            (
                (p) => { return true; },
                (p) =>
                {
                    Playlists.Add(new Playlist() { Name = "My playlist", CreatedDate = DateTime.Now, Description = "", UsersId = LoginedUserId }); ;
                    OnPropertyChanged(nameof(Playlists));
                }
            );
        }

        private void PopulateCollection()
        {
            using (var database = new MusicPlayerVpContext())
            {
                var playlists = database.Playlists
                            .Where(s => s.UsersId == LoginedUserId);

                foreach (Playlist playlist in playlists)
                    Playlists.Add(playlist);
            }

        }
    }
}
