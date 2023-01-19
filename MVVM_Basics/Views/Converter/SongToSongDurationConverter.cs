using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MVVM_Basics.Views.Converter;

public class SongToSongDurationCoverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Song song && !string.IsNullOrEmpty(song.PcLink))
        {
            return TimeSpan.FromSeconds(Math.Round(Mp3Helper.GetDuration(song.PcLink)));
        }

        return TimeSpan.Zero;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
