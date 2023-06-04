using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CineWave.Core;
using CineWave.MVVM.Model;

namespace CineWave.MVVM.ViewModel;

public class SeatBookingViewModel : Core.ViewModel
{
    private readonly List<CinemaHall> _cinemaHall = new();
    private readonly ObservableCollection<MovieButtonViewModel> _movies = new(); // For cinema choose
    private readonly ObservableCollection<SeatCardViewModel> _seats = new(); // For seats choose
    public IEnumerable<MovieButtonViewModel> NowShowingMovies => _movies;
    public IEnumerable<SeatCardViewModel> Seats => _seats;

    public SeatBookingViewModel()
    {
        _cinemaHall.Add(new CinemaHall(40, "A1"));
        _cinemaHall.Add(new CinemaHall(40, "A2"));
        Task.Run(CreateNowShowingMoviesButton);
        Task.Run(CreateSeats);
    }

    public bool IsSeatAvailable(int roomNumber, int seatNumber)
    {
        var seat = _cinemaHall[roomNumber].Seats.FirstOrDefault(s => s.SeatNumber == seatNumber && !s.IsTaken);
        return seat is { IsTaken: false };
    }

    public bool BookSeat(int roomNumber, int seatNumber, Customer customer)
    {
        var seat = _cinemaHall[roomNumber].Seats.FirstOrDefault(s => s.SeatNumber == seatNumber);
        if (seat == null || seat.IsTaken) return false;
        seat.IsTaken = true;
        
        // TODO: Save customer information or perform any necessary operations
        return true;
    }

    private async Task CreateNowShowingMoviesButton()
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            _movies.Add(new MovieButtonViewModel("C1", "SpiderMan Far From Home"));
            _movies.Add(new MovieButtonViewModel("C2", "Loki"));
            _movies.Add(new MovieButtonViewModel("C1", "The Godfather"));
        });
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