using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.Helpers;
using CineWave.MVVM.View.Reservations.MovieList;
using CineWave.MVVM.View.Reservations.SeatBooking;
using CineWave.MVVM.ViewModel.Reservations.MovieList;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Reservations;

public partial class ReservationsViewModel : BaseViewModel
{
    private readonly IUnitOfWork _unitOfWork;

    [ObservableProperty] private ObservableCollection<ReservationCardViewModel> _reservationCardViewModels = new();

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
                        reservation.Customer.CustomerName ?? StringHelper.CustomerNotFound,
                        reservation.Customer.Ticket.Movie.MovieName ?? StringHelper.MovieNotFound,
                        reservation.Customer.Ticket.SeatNumber,
                        reservation.Customer.Ticket.Movie.Runtime,
                        reservation.Customer.Ticket.Movie.ScreeningDateTime,
                        reservation.DateOfReservation
                    ));
            }
        });
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    #pragma warning disable CA1822
    public void OpenMovieList()
    {
        if (!_unitOfWork.MoviesRepository.DoesHaveMoviesForReservation())
        {
            MessageBox.Show("No upcoming movie available for reservation!");
            return;
        }
        WindowHelper.ShowOrCloseWindow((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<MovieListWindow>());
        WindowHelper.HideWindow((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<SeatBookingWindow>());
        Task.Run((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<MovieListViewModel>().CreateMovieInfoCards);
    }
}