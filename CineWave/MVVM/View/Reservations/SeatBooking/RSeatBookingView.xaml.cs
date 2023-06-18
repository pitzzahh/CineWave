using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace CineWave.MVVM.View.Reservations.SeatBooking;

public partial class RSeatBookingView
{
    public RSeatBookingView()
    {
        InitializeComponent();
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wp, int lp);

    private void OnMoveWindow(object sender, MouseButtonEventArgs e)
    {
        SendMessage(new WindowInteropHelper(this).Handle, 161, 2, 0);
    }

    private void OnEnterControlBar(object sender, MouseEventArgs e)
    {
        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }
}