using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CineWave.MVVM.ViewModel.Reservations;

public partial class ReservationCardViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _customerName;
    private string _movieName;
    [ObservableProperty] private TimeOnly _runtime;
    [ObservableProperty] private DateTime _screeningDateTime;
    [ObservableProperty] private DateOnly _reservationDate;
    
    [RelayCommand]
    public void OpenForm()
    {
        throw new NotImplementedException();
    }
}