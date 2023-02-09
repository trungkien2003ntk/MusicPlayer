using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.EventAndCommandHandlers;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM_Basics.ResourcesXAML
{
    public partial class PlaylistResource : ResourceDictionary
    {
        public PlaylistResource()
        {
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            ContextMenu thisContextMenu = (sender as ContextMenu)!;
            DependencyObject parent = thisContextMenu.PlacementTarget;

            while (parent != null && !(parent is ListView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            ListView lvRoot = (parent as ListView)!;
            lvRoot.SelectedIndex = -1;
        }

        private void MenuItemAddToPlaylist_Loaded1(object sender, RoutedEventArgs e)
        {
            MenuItem miAddToPlaylist = (sender as MenuItem)!;
            
            using (var context = App.AppHost!.Services.GetRequiredService<MusicPlayerVpContext>())
            {
                foreach (Playlist playlist in context.Playlists)
                {
                    MenuItem miPlaylist = new MenuItem()
                    {
                        Header = playlist.Name,
                        Tag = playlist.Id,
                    };

                    miAddToPlaylist.Items.Add(miPlaylist);
                }
            }
        }

        private void MenuItemAddToPlaylist_Unloaded(object sender, RoutedEventArgs e)
        {
            MenuItem miAddToPlaylist = (sender as MenuItem)!;
            miAddToPlaylist.Items.Clear();
        }

        private void ListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView lvAllSongs = (sender as ListView)!;

            if (e.ChangedButton == MouseButton.Left)
            {
                var selectedSong = lvAllSongs.SelectedItem as Song;

                Messenger.Default.Send(new ChangeSongMessage(selectedSong!));
            }
        }

        private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ListView lvSender = (sender as ListView)!;

            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;

                RaiseScrollEventOnParentScrollViewer(eventArg, lvSender);
            }
        }

        private void RaiseScrollEventOnParentScrollViewer(MouseWheelEventArgs eventArg, UIElement child)
        {
            UIElement? parent = FindParentScrollViewer(child);

            if (parent != null)
            {
                parent.RaiseEvent(eventArg);
            }
        }

        public ScrollViewer? FindParentScrollViewer(DependencyObject child)
        {
            DependencyObject parentDepObj = child;
            do
            {
                parentDepObj = VisualTreeHelper.GetParent(parentDepObj);
                ScrollViewer? parent = parentDepObj as ScrollViewer;
                if (parent != null) return parent;
            }
            while (parentDepObj != null);

            return null;
        }
    }
}
