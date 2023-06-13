using System.Collections.Generic;

namespace CineWave.Helpers;

public class SeatNumberComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        if (y != null && x != null && (x.Length < 2 || y.Length < 2))
            return string.CompareOrdinal(x, y);
        var xRow = x?[..1];
        var yRow = y?[..1];
        var xNumber = int.Parse(x?[1..] ?? string.Empty);
        var yNumber = int.Parse(y?[1..] ?? string.Empty);

        var rowComparison = string.CompareOrdinal(xRow, yRow);
        return rowComparison != 0 ? rowComparison : xNumber.CompareTo(yNumber);
    }
}