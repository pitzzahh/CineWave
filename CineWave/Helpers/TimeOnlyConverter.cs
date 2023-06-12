
using System;
using System.Globalization;
using System.Windows.Data;

namespace CineWave.Helpers;

public class TimeOnlyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TimeOnly time) return string.Empty;
        var hours = time.Hour > 0 ? time.Hour + " Hour" : string.Empty;
        var minutes = time.Minute > 0 ? time.Minute.ToString("00") + " Minutes" : string.Empty;

        if (!string.IsNullOrEmpty(hours) && !string.IsNullOrEmpty(minutes))
            return $"{hours} and {minutes}";
        return $"{hours}{minutes}";

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
