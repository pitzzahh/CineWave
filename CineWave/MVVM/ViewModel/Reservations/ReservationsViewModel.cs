using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.MVVM.View.Reservations.MovieList;
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
    #pragma warning disable CA1822
    public void OpenMovieList()
    {
        var currentMovie = _unitOfWork.MoviesRepository.GetNowShowingMovie();
        if (currentMovie == null)
        {
            MessageBox.Show("No upcoming movie available for reservation!");
            return;
        }
        (App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<MovieListWindow>().Hide();
        (App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<MovieListWindow>().Show();
    }
}