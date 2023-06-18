using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.Helpers;
using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingViewModel : BaseViewModel
{
    private const string MovieNotFound = "No movie is currently showing";
    private readonly IUnitOfWork _unitOfWork;
    [ObservableProperty] private string? _currentMovie;
    [ObservableProperty] private string? _seatNumber;
    [ObservableProperty] private ObservableCollection<SbSeatCardViewModel> _seats = new(); // For seats choose

    public SeatBookingViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SetCurrentMovie()
    {
        var currentlyShowing = _unitOfWork.MoviesRepository.GetNowShowingMovie() ?? new Movie();
        var seats = _unitOfWork.SeatsRepository.GetAll().Where(s => s.MovieId == currentlyShowing.MovieId).ToList();
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            CurrentMovie = currentlyShowing.MovieName ?? MovieNotFound;
            Seats.Clear();
            if (!seats.Any())
            {
                for (var row = 'A'; row <= 'E'; row++)
                {
                    for (var column = 1; column <= 10; column++)
                    {
                        var seatNumber = $"{row}{column}";
                        Seats.Add(new SbSeatCardViewModel(seatNumber, true, _unitOfWork));
                    }
                }
            }
            else
            {
                foreach (var seat in seats.OrderBy(seat => seat.SeatNumber, new SeatNumberComparer()))
                {
                    Seats.Add(new SbSeatCardViewModel(seat.SeatNumber, seat.IsTaken, _unitOfWork));   
                }
            }
        });
    }
}