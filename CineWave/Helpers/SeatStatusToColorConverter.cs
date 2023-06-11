using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CineWave.Helpers;

public class SeatStatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFECF39E")) : new SolidColorBrush(Colors.Red);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}