using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_Basics.ViewModels
{
    public class SideBarViewModel : BaseViewModel
    {
        #region Commands
        public ICommand CreatePlaylistCommand { get; set; }

        #endregion Commands

        public ObservableCollection<Playlist> Playlists { get; set ; }
        

        public SideBarViewModel()
        {
            Playlists = new ObservableCollection<Playlist>();

            PopulateCollection();

            CreatePlaylistCommand = new RelayCommand<UserControl>
            (
                (p) => { return true; },
                (p) =>
                {
                    Playlists.Add(new Playlist() { Name = "My playlist", CreatedDate = DateTime.Now, Description = "", UsersId = MainViewModel.LoginedUser.Id }); ;
                    OnPropertyChanged(nameof(Playlists));
                }
            );
        }

        private void PopulateCollection()
        {
            var playlists = DataProvider.Ins.DB.Playlists
                            .Where(s => s.UsersId == MainViewModel.LoginedUser.Id);

            foreach (Playlist playlist in playlists)
                Playlists.Add(playlist);
        }
    }
}
