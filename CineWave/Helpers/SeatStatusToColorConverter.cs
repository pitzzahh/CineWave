using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace CineWave.Helpers;

public class SeatStatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Brushes.Red : new SolidBrush( Color.FromName("#FFECF39E"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}