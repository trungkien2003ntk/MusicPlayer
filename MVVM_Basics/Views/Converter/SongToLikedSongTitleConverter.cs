using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Models;
using MVVM_Basics.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVVM_Basics.Views.Converter;

public class SongToLikedSongTitleConverter : IValueConverter
{
    private string AddText = "Save to your Liked Songs";
    private string RemoveText = "Remove from your Liked Songs";


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        using (var context = App.AppHost!.Services.GetRequiredService<MusicPlayerVpContext>())
        {
            var sharedDataContext = App.AppHost.Services.GetRequiredService<ISharedDataContext>();

            if (value is System.Collections.IList selectedItemsInList)
            {
                if (selectedItemsInList.Count > 0)
                {
                    List<Song> songsToCheck = new(selectedItemsInList.Cast<Song>().ToList());

                    foreach (Song songToCheck in songsToCheck)
                    {
                        //if (songToCheck == null) { return ""; }

                        if (!context.LikedSongs.Any(ls => ls.UsersId == sharedDataContext.LoginedUserId && ls.SongId == songToCheck.Id))
                        {
                            return AddText;
                        }
                    }
                }
            }
            return RemoveText;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
