using System;
using System.Globalization;
using System.Windows.Data;

namespace CineWave.Helpers;

public class ScreeningDateTimeConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2 || values[0] is not DateTime screeningDateTime || values[1] is not TimeOnly runtime)
            return string.Empty;
        var screeningDate = screeningDateTime.ToString("MMMM d yyyy h:mm tt");
        var endTime = screeningDateTime.Add(runtime.ToTimeSpan()).ToString("h:mm tt");
        return $"{screeningDate} - {endTime}";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}