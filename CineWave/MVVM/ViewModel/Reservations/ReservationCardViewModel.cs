using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.Reservations;

public partial class ReservationCardViewModel : BaseViewModel
{
    [ObservableProperty] private string _customerName;
    [ObservableProperty] private string _movieName;
    [ObservableProperty] private string _seatNumber;
    [ObservableProperty] private DateOnly _reservationDate;
    [ObservableProperty] private TimeOnly _runtime;
    [ObservableProperty] private DateTime _screeningDateTime;

    public ReservationCardViewModel(string customerName, string movieName, string seatNumber, TimeOnly runtime,
        DateTime screeningDateTime,
        DateOnly reservationDate)
    {
        CustomerName = customerName;
        MovieName = movieName;
        SeatNumber = seatNumber;
        Runtime = runtime;
        ScreeningDateTime = screeningDateTime;
        ReservationDate = reservationDate;
    }
}