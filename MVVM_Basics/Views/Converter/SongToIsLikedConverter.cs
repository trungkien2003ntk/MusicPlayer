using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MVVM_Basics.Views.Converter;

public class SongToIsLikedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Song song = (Song)value;

        using (var context = App.AppHost!.Services.GetRequiredService<MusicPlayerVpContext>())
        {
            if (context.LikedSongs.Any(ls => ls.SongId == song.Id))
            {
                return true;
            }
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isLiked = (bool)value;

        if (isLiked)
        {
            return false;
        }

        return true;
    }
}
