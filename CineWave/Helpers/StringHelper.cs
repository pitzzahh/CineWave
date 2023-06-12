using System;
using System.Globalization;

namespace CineWave.Helpers;

public static class StringHelper
{
    public static bool IsWholeNumberOrDecimal(string input)
    {
        return int.TryParse(input, out _) || double.TryParse(input, out _);
    }
    
    public static string GetMonthString(int month)
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
    }

    public static int GetMonthInt(string? month)
    {
        if (month == null) return 1;
        var monthInt = Array.FindIndex(CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames,
            m => m.Equals(month, StringComparison.OrdinalIgnoreCase));

        return monthInt + 1;
    }
    
    public static string GetCurrencySymbol(CultureInfo culture)
    {
        return new RegionInfo(culture.Name).CurrencySymbol;
    }
}