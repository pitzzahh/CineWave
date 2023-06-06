using System.Diagnostics;
using CineWave.Components;
using CineWave.Messages.SeatsBooking;
using CineWave.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatCardViewModel : BaseViewModel
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
        else
        {
            seatBookingRegistrationForm.Show();
            var currentMovie = App.ServiceProvider.GetRequiredService<SeatBookingViewModel>().CurrentMovie;
            WeakReferenceMessenger.Default.Send(new GetSeatInfoMessage(new ReservationInfo(currentMovie ?? "", SeatNumber ?? "")));
        }
    }
}