using System.Linq;
using CineWave.MVVM.Model;

namespace CineWave.MVVM.ViewModel;

public class SeatBookingViewModel : Core.ViewModel
{
    private readonly CinemaHall _cinemaHall;

    public SeatBookingViewModel()
    {
        _cinemaHall = new CinemaHall(40);
    }
    
    public bool IsSeatAvailable(int seatNumber)
    {
        var seat = _cinemaHall.Seats.FirstOrDefault(s => s.SeatNumber == seatNumber);
        return seat is { IsTaken: false };
    }

    public bool BookSeat(int seatNumber, Customer customer)
    {
        var seat = _cinemaHall.Seats.FirstOrDefault(s => s.SeatNumber == seatNumber);
        if (seat == null || seat.IsTaken) return false;
        seat.IsTaken = true;
        // Save customer information or perform any necessary operations
        return true;
    }
}