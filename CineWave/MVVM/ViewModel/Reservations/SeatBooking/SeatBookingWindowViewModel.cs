using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.Helpers;
using CineWave.Messages.ManageMovies;
using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace CineWave.MVVM.ViewModel.Reservations.SeatBooking;

public partial class SeatBookingWindowViewModel : BaseViewModel, IRecipient<GetMovieInfoMessage>
{
    private readonly IUnitOfWork _unitOfWork;
    [ObservableProperty] private string _currentMovie = null!;
    [ObservableProperty] private string _seatNumber = null!;
    [ObservableProperty] private ObservableCollection<RSeatCardViewModel> _seats = new(); // For seats choose

    public SeatBookingWindowViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        WeakReferenceMessenger.Default.Register(this);
    }

    public async Task SetCurrentMovie()
    {
        var currentlyShowing = _unitOfWork.MoviesRepository.GetMovieByName(CurrentMovie) ?? new Movie();
        var seats = _unitOfWork.SeatsRepository.GetAll().Where(s => s.MovieId == currentlyShowing.MovieId).ToList();
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Seats.Clear();
            if (!seats.Any())
            {
                for (var row = 'A'; row <= 'E'; row++)
                {
                    for (var column = 1; column <= 10; column++)
                    {
                        var seatNumber = $"{row}{column}";
                        Seats.Add(new RSeatCardViewModel(seatNumber, true));
                    }
                }
            }
            else
            {
                foreach (var seat in seats.OrderBy(seat => seat.SeatNumber, new SeatNumberComparer()))
                {
                    Seats.Add(new RSeatCardViewModel(seat.SeatNumber, seat.IsTaken));   
                }
            }
        });
    }

    public void Receive(GetMovieInfoMessage message)
    {
        var editMovieInfo = message.Value;
        CurrentMovie = editMovieInfo.MovieName;
    }
}