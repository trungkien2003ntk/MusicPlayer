using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using MVVM_Basics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVM_Basics.Views;

public partial class PlaylistPage : UserControl
{
    private bool isListViewInitialized = false;

    public PlaylistPage()
    {
        InitializeComponent();

        // Set titlebar's background
        titleBar.Background = ColorHelper.GetGradientColorAtIndex((rootControl.Background as LinearGradientBrush)!, 0);


        // Register this for changing viewmodel when change the playlist by using the sidebar command
        Messenger.Default.Register<PlaylistChangedMessage>(this, HandlePlaylistChangedMessage);
    }

    private void HandlePlaylistChangedMessage(PlaylistChangedMessage message)
    {
        //DataContext = message.ChangedViewModel;
    }

    private void ListView_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        ListView currentListView = (sender as ListView)!;

        for (int i = 0; i < currentListView.Items.Count; i++)
        {
            var item = currentListView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

            if (item!.Tag != null && 
          ((Song)item.Content).Id < 0) { }
        }
    }

    private void ListView_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (isListViewInitialized)
            return;

        AppendAddedDateOfSongToListViewItemTag();

        lvSongs.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;

        isListViewInitialized = true;
    }

    private void ItemContainerGenerator_StatusChanged(object? sender, EventArgs e)
    {
        if (lvSongs.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
        {
            AppendAddedDateOfSongToListViewItemTag();
        }
    }

    private void AppendAddedDateOfSongToListViewItemTag()
    {
        List<DateTime> songAddedDates = new();

        using (var context = App.AppHost!.Services.GetRequiredService<MusicPlayerVpContext>())
        {
            var sharedDataContext = App.AppHost!.Services.GetRequiredService<ISharedDataContext>();

            songAddedDates = context.PlaylistSongs.Where(ps =>
                                                        ps.PlaylistId == sharedDataContext.CurrentOpeningPlaylist!.Id
                                                  )
                                                  .Select(ps => ps.AddedDate)
                                                  .ToList();
        }

        for (int i = 0; i < lvSongs.Items.Count; i++)
        {
            var testItem = lvSongs.Items[i];
            var item = lvSongs.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;

            while (item == null)
            {
                lvSongs.UpdateLayout();
                item = lvSongs.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
            }

            if (item.Content != null) { }

            item!.Tag = songAddedDates[i];
        }
    }
}
