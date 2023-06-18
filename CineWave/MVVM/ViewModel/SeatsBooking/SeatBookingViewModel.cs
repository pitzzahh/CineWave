using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingViewModel : BaseViewModel
{
    [ObservableProperty] private string? _currentMovie;
    [ObservableProperty] private string? _seatNumber;
    [ObservableProperty] private ObservableCollection<SbSeatCardViewModel> _seats = new(); // For seats choose
    private const string MovieNotFound = "No movie is currently showing";
    private readonly IUnitOfWork _unitOfWork;

    public SeatBookingViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SetCurrentMovie()
    {
        var currentlyShowing = _unitOfWork.MoviesRepository.GetNowShowingMovie();
        var seats = _unitOfWork.SeatsRepository.GetAll().Where(s => s.MovieId == currentlyShowing?.MovieId);
        var enumerable = seats.ToList();
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            CurrentMovie = currentlyShowing?.MovieName ?? MovieNotFound;
            Seats.Clear();
            if (!enumerable.Any())
            {
                for (var row = 'A'; row <= 'E'; row++)
                {
                    for (var column = 1; column <= 10; column++)
                    {
                        var seatNumber = $"{row}{column}";
                        Seats.Add(new SbSeatCardViewModel(seatNumber, CurrentMovie == MovieNotFound, _unitOfWork));
                    }
                }
                return;
            }
            var sortedSeats = enumerable.OrderBy(seat => seat.SeatNumber, new SeatNumberComparer());
            foreach (var seat in sortedSeats)
            {
                Seats.Add(new SbSeatCardViewModel(seat.SeatNumber, CurrentMovie == MovieNotFound || seat.IsTaken, _unitOfWork));
            }
        });
    }
}