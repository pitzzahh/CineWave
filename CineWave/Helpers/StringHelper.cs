namespace CineWave.Helpers;

public class StringHelper
{
    public static bool IsWholeNumberOrDecimal(string input)
    {
        return int.TryParse(input, out _) || double.TryParse(input, out _);
    }
}