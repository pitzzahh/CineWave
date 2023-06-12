using System;
using System.Globalization;
using System.Windows.Data;

namespace CineWave.Helpers;

public class DateToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            DateOnly date => date.ToString("MMMM d yyyy"),
            DateTime dateTime => dateTime.ToString("MMMM d yyyy h:mm tt"),
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}