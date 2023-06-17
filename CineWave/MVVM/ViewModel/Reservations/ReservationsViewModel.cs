using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CineWave.MVVM.ViewModel.Reservations;

public partial class ReservationsViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<ReservationCardViewModel> _reservationCardViewModels = new();
    private readonly IUnitOfWork _unitOfWork;

    public ReservationsViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateMovieInfoCards()
    {
        var reservations = _unitOfWork.ReservationsRepository.GetAll();
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            ReservationCardViewModels.Clear();
            foreach (var reservation in reservations)
            {
                ReservationCardViewModels.Add(
                    new ReservationCardViewModel(
                        reservation.Customer.CustomerName ?? "404 Customer Name not found",
                        reservation.Customer.Ticket.Movie.MovieName,
                        reservation.Customer.Ticket.Movie.Runtime,
                        reservation.Customer.Ticket.Movie.ScreeningDateTime,
                        reservation.DateOfReservation
                    ));
            }
            _unitOfWork.Complete();
        });
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public void OpenMovieList()
    {
    }
}