using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CineWave.Helpers;

public class ImageBrushConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ImageSource imageSource)
        {
            return new ImageBrush(imageSource);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}