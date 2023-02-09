using System;
using System.Globalization;
using System.Windows.Data;

namespace MVVM_Basics.Views.Converter;

public class ObjectToDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            DateTime dateTime= (DateTime)value;

            return dateTime.ToString("MMM dd, yyyy");
        }
        catch
        {
            return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
