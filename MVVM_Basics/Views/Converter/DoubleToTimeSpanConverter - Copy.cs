using MVVM_Basics.Models;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace MVVM_Basics.Views.Converter;

public class ItemIndexConverter : IMultiValueConverter
{

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var item = values[0] as Song;
        var collection = values[1] as ObservableCollection<Song>;
        int index = collection.IndexOf(item) + 1;
        
        return index;
    }


    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
