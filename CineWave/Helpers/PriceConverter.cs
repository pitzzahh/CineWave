using System;
using System.Globalization;
using System.Windows.Data;

namespace CineWave.Helpers;

public class PriceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            string priceString => priceString == "0"
                ? "Free"
                : $"{StringHelper.GetCurrencySymbol(culture)}{priceString}",
            double priceDouble => priceDouble == 0
                ? "Free"
                : $"{StringHelper.GetCurrencySymbol(culture)}{priceDouble}",
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}