using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingViewModel : Core.ViewModel
{
    [ObservableProperty] private string? _currentMovie;
    [ObservableProperty] private string? _seatId;

    private readonly ObservableCollection<SeatCardViewModel> _seats = new(); // For seats choose
    public IEnumerable<SeatCardViewModel> Seats => _seats;

    public SeatBookingViewModel()
    {
        CurrentMovie = "Spider-man";
        Task.Run(CreateSeats);
    }

    public bool IsSeatAvailable(string seatNumber)
    {
        return true;
    }

    public bool BookSeat(string seatNumber, Customer customer)
    {
        return true;
    }

    private async Task CreateSeats()
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            for (var row = 'A'; row <= 'E'; row++)
            {
                for (var column = 1; column <= 8; column++)
                {
                    var seatNumber = $"{row}{column}";
                    SeatId = seatNumber;
                    _seats.Add(new SeatCardViewModel(seatNumber, false));
                }
            }
        });
    }
}