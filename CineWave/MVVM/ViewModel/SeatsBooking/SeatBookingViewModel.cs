using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingViewModel : BaseViewModel
{
    [ObservableProperty] private string? _currentMovie;
    [ObservableProperty] private string? _seatNumber;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ObservableCollection<SeatCardViewModel> _seats = new(); // For seats choose
    public IEnumerable<SeatCardViewModel> Seats => _seats;
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
                _seats.Add(new SeatCardViewModel(seat.SeatNumber, CurrentMovie == MovieNotFound ||seat.IsTaken, _unitOfWork));
            }
        });
    }
    
    private class SeatNumberComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (y != null && x != null && (x.Length < 2 || y.Length < 2))
                return string.CompareOrdinal(x, y);
            var xRow = x?[..1];
            var yRow = y?[..1];
            var xNumber = int.Parse(x?[1..] ?? string.Empty);
            var yNumber = int.Parse(y?[1..] ?? string.Empty);

            var rowComparison = string.CompareOrdinal(xRow, yRow);
            return rowComparison != 0 ? rowComparison : xNumber.CompareTo(yNumber);
        }
    }
}