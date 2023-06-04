using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.MVVM.Model;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public class SeatBookingViewModel : Core.ViewModel
{
    private string CurrentMovie { get; set; }
    private readonly ObservableCollection<SeatCardViewModel> _seats = new(); // For seats choose
    public IEnumerable<SeatCardViewModel> Seats => _seats;

    public SeatBookingViewModel()
    {
        Task.Run(CreateSeats);
        CurrentMovie = "Spider-man";
    }

    public bool IsSeatAvailable(int roomNumber, string seatNumber)
    {
        return true;
    }

    public bool BookSeat(int roomNumber, string seatNumber, Customer customer)
    {
        // TODO: Save customer information or perform any necessary operations
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
                    _seats.Add(new SeatCardViewModel(seatNumber, true));
                }
            }
        });
    }
}