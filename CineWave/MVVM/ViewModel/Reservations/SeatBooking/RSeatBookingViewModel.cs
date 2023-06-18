using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.Reservations.SeatBooking;

public partial class RSeatBookingViewModel : BaseViewModel
{
    private const string MovieNotFound = "No movie is currently showing";
    private readonly IUnitOfWork _unitOfWork;
    [ObservableProperty] private string? _currentMovie;
    [ObservableProperty] private string? _seatNumber;
    [ObservableProperty] private ObservableCollection<RSeatCardViewModel> _seats = new(); // For seats choose

    public RSeatBookingViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SetCurrentMovie()
    {
        var currentlyShowing = _unitOfWork.MoviesRepository.GetNowShowingMovie();
        var seats = _unitOfWork.SeatsRepository.GetAll().Where(s => s.MovieId == currentlyShowing?.MovieId).ToList();
        if (!seats.Any()) await CreateUnavailableSeats();
        else await CreateSeats();
    }
    
    private async Task CreateUnavailableSeats()
    {
        if (Seats.Count == 50) return;
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            CurrentMovie = MovieNotFound;
            for (var row = 'A'; row <= 'E'; row++)
            {
                for (var column = 1; column <= 10; column++)
                {
                    var seatNumber = $"{row}{column}";
                    Seats.Add(new RSeatCardViewModel(seatNumber, true, _unitOfWork));
                }
            }
        });
    }
    
    private async Task CreateSeats()
    {
        Seats.Clear();
        var currentlyShowing = _unitOfWork.MoviesRepository.GetNowShowingMovie()?.MovieName ?? MovieNotFound;
        var seats = _unitOfWork.SeatsRepository.GetAll();
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            CurrentMovie = currentlyShowing;
            Seats.Clear();
            foreach (var seat in seats.OrderBy(seat => seat.SeatNumber, new SeatNumberComparer()))
            {
                Seats.Add(new RSeatCardViewModel(seat.SeatNumber, seat.IsTaken, _unitOfWork));   
            }
        });
    }
}