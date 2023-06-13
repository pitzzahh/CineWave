using System.Collections.Generic;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ObservableCollection<SbSeatCardViewModel> _seats = new(); // For seats choose
    public IEnumerable<SbSeatCardViewModel> Seats => _seats;
    private const string MovieNotFound = "No movie is currently showing";

    public SeatBookingViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SetCurrentMovie()
    {
        var noMovieIsCurrentlyShowing = _unitOfWork.MoviesRepository.GetNowShowingMovie()?.MovieName ?? MovieNotFound;
        var seats = _unitOfWork.SeatsRepository.GetAll();
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            CurrentMovie = noMovieIsCurrentlyShowing;
            _seats.Clear();
            var sortedSeats = seats.OrderBy(seat => seat.SeatNumber, new SeatNumberComparer());
            foreach (var seat in sortedSeats)
            {
                _seats.Add(new SbSeatCardViewModel(seat.SeatNumber, CurrentMovie == MovieNotFound ||seat.IsTaken, _unitOfWork));
            }
        });
    }
}