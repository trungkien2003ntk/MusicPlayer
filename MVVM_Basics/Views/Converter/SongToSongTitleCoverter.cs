using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MVVM_Basics.Views.Converter;

public class SongToSongTitleCoverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Song song && !string.IsNullOrEmpty(song.PcLink))
        {
            return Mp3Helper.GetTitle(song.PcLink);
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
