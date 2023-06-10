using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingViewModel : BaseViewModel
{
    [ObservableProperty] private string? _currentMovie;
    [ObservableProperty] private string? _seatNumber;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ObservableCollection<SeatCardViewModel> _seats = new(); // For seats choose
    public IEnumerable<SeatCardViewModel> Seats => _seats;

    public SeatBookingViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        Task.Run(CreateSeats);
        Task.Run(SetCurrentMovie);
    }

    public async Task SetCurrentMovie()
    {
        await Application.Current
            .Dispatcher
            .InvokeAsync(() =>
            {
                CurrentMovie = _unitOfWork.MoviesRepository.GetAll().FirstOrDefault(m => m.NowShowing)?.MovieName ??
                               "No movie is currently showing";
            });
    }

    private async Task CreateSeats()
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            for (var row = 'A'; row <= 'E'; row++)
            {
                for (var column = 1; column <= 10; column++)
                {
                    var seatNumber = $"{row}{column}";
                    SeatNumber = seatNumber;
                    _seats.Add(new SeatCardViewModel(seatNumber, false, _unitOfWork));
                }
            }
        });
    }
}