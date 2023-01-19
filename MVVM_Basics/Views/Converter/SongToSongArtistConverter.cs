using Microsoft.Extensions.DependencyInjection;
using MVVM_Basics.Helpers;
using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MVVM_Basics.Views.Converter
{
    public class SongToSongArtistConverter : IValueConverter
    {
        //private readonly IServiceProvider _ServiceProvider;

        //public SongToSongArtistConverter(IServiceProvider serviceProvider)
        //{
        //    _ServiceProvider = serviceProvider;
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Song song && !string.IsNullOrEmpty(song.PcLink))
            {
                return string.Join(", ", Mp3Helper.GetArtistNames(song.PcLink));
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
