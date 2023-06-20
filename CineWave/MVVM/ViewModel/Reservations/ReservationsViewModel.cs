using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        var reservations = _unitOfWork.ReservationsRepository.GetReservationsDuringOrBeforeScreenTime();
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            ReservationCardViewModels.Clear();
            foreach (var reservation in reservations)
            {
                var customer = _unitOfWork.CustomersRepository.Find(c => c.CustomerId == reservation.CustomerId)
                    .FirstOrDefault();
                if (customer == null) continue;
                var ticket = _unitOfWork.TicketsRepository.Find(t => t.TicketId == customer.TicketId)
                    .FirstOrDefault();
                if (ticket == null) continue;
                var movie = _unitOfWork.MoviesRepository.Find(m => m.MovieId == ticket.MovieId)
                    .FirstOrDefault();
                if (movie != null)
                {
                    ReservationCardViewModels.Add(
                        new ReservationCardViewModel(
                            customer.CustomerName ?? StringHelper.CustomerNotFound,
                            customer.Ticket.Movie.MovieName ?? StringHelper.MovieNotFound,
                            ticket.SeatNumber,
                            movie.Runtime,
                            movie.ScreeningDateTime,
                            reservation.DateOfReservation
                        ));
                }
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

        WindowHelper.ShowOrCloseWindow((App.ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<MovieListWindow>());
        WindowHelper.HideWindow((App.ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<SeatBookingWindow>());
        Task.Run((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<MovieListViewModel>()
            .CreateMovieInfoCards);
    }
}