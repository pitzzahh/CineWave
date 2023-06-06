using System.Diagnostics;
using CineWave.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatCardViewModel : Core.ViewModel
{
    [ObservableProperty] private bool _isSeatAvailable;
    [ObservableProperty] private string? _seatNumber;

    public SeatCardViewModel(string seatNumber, bool isTaken)
    {
        IsSeatAvailable = !isTaken;
        SeatNumber = seatNumber;
    }

    [RelayCommand]
    public void OpenForm()
    {
        if (!IsSeatAvailable) return;
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        var seatBookingRegistrationForm = App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>();
        if (seatBookingRegistrationForm.IsVisible)
        {
            seatBookingRegistrationForm.Hide();
        }
        else seatBookingRegistrationForm.Show();
    }
}