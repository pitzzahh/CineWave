using System;
using System.Text;

namespace CineWave.Helpers;

internal static class PseudoRandomEncryptor
{
    public static string Encrypt(string input)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
    }

    public static string Decrypt(string input)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(input));
    }
}