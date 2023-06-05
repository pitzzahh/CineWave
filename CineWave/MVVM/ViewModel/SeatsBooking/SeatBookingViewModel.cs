using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.MVVM.Model;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public class SeatBookingViewModel : Core.ViewModel
{
    public string CurrentMovie { get; set; }
    public string SeatButtonId { get; set; } = null!;

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
                    SeatButtonId = seatNumber;
                    _seats.Add(new SeatCardViewModel(seatNumber, false));
                }
            }
        });
    }
}