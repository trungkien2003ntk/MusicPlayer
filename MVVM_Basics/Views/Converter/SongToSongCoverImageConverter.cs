using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MVVM_Basics.Views.Converter;

public class SongToSongCoverImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Song song && !string.IsNullOrEmpty(song.PcLink))
        {
            return Mp3Helper.GetCoverImage(song.PcLink);
        }

        return new BitmapImage(new Uri(@"Images\DefaultSongCover.png", UriKind.RelativeOrAbsolute));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
