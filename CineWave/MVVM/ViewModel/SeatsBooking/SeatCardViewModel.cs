using System.Diagnostics;
using CineWave.Components;
using CineWave.Core;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public class SeatCardViewModel : Core.ViewModel
{
    public bool IsSeatAvailable { get; set; }
    public string SeatNumber { get; }

    public RelayCommand OpenForm { get; set; }

    public SeatCardViewModel(string seatNumber, bool isTaken)
    {
        IsSeatAvailable = !isTaken;
        SeatNumber = seatNumber;
        OpenForm = new RelayCommand(o =>
        {
            Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
            var seatBookingRegistrationForm = App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>();
            if (seatBookingRegistrationForm.IsVisible)
            {
                seatBookingRegistrationForm.Hide();
            }
            else seatBookingRegistrationForm.Show();
        }, o => IsSeatAvailable);
    }
}