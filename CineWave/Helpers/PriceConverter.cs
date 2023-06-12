using System;
using System.Globalization;
using System.Windows.Data;

namespace CineWave.Helpers;


public class PriceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string priceString) return value;
        return priceString == "0" ? "Free" : $"{StringHelper.GetCurrencySymbol(culture)}{priceString}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
