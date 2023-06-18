using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.Reservations;

public partial class ReservationCardViewModel : BaseViewModel
{
    [ObservableProperty] private string _customerName;
    [ObservableProperty] private string _movieName;
    [ObservableProperty] private DateOnly _reservationDate;
    [ObservableProperty] private TimeOnly _runtime;
    [ObservableProperty] private DateTime _screeningDateTime;

    public ReservationCardViewModel(string customerName, string movieName, TimeOnly runtime, DateTime screeningDateTime,
        DateOnly reservationDate)
    {
        _customerName = customerName;
        _movieName = movieName;
        _runtime = runtime;
        _screeningDateTime = screeningDateTime;
        _reservationDate = reservationDate;
    }
}