using System.Windows;

namespace CineWave.Helpers;

public static class WindowHelper
{
    public static void ShowOrCloseWindow(Window window)
    {
        if (!window.IsVisible) window.Show();
        else window.Hide();
    }
}