using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CineWave.Components;
using CineWave.DB.Core;
using CineWave.Helpers;
using CineWave.Messages.SeatsBooking;
using CineWave.MVVM.Model;
using CineWave.MVVM.Model.Movies;
using CineWave.MVVM.View.Reservations.SeatBooking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Reservations.SeatBooking;

public partial class SeatBookingReservationFormViewModel : BaseViewModel, IRecipient<GetSeatInfoMessage>
{
    private readonly IUnitOfWork _unitOfWork;
    [ObservableProperty] private string _isMovieFree = "Visible";
    [ObservableProperty] private string? _movieName;
    [ObservableProperty] private string? _moviePrice;
    [ObservableProperty] private string? _payment;
    [ObservableProperty] private string? _seatNumber;

    public SeatBookingReservationFormViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(GetSeatInfoMessage message)
    {
        var reservationInfo = message.Value;
        MovieName = reservationInfo.MovieName;
        IsMovieFree = reservationInfo.MoviePrice == 0 ? "Hidden" : "Visible";
        MoviePrice = reservationInfo.MoviePrice.ToString(CultureInfo.InvariantCulture);
        SeatNumber = reservationInfo.SeatNumber;
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public void OnBuy()
    {
        if (MoviePrice != "0" && CheckInputs()) MessageBox.Show("Please enter a valid payment");

        var currentMovie = _unitOfWork.MoviesRepository.GetMovieByName(MovieName ?? string.Empty);

        if (currentMovie == null) return;
        if (currentMovie.MovieId != 0 && MoviePrice != "0" && Payment != null &&
            double.Parse(Payment) < currentMovie.MoviePrice) MessageBox.Show("Payment is not enough");

        var isSeatNotAvailable = IsSeatNotAvailable();
        if (isSeatNotAvailable) MessageBox.Show("Seat is not available");

        if (SeatNumber != null)
        {
            var ticket = new Ticket(currentMovie.MovieId, SeatNumber);
            var customer = new Customer(ticket.TicketId);
            _unitOfWork.CustomersRepository.Add(customer);
            _unitOfWork.TicketsRepository.Add(ticket);
        }

        var complete = _unitOfWork.Complete();
        if (complete == 0)
        {
            MessageBox.Show("Failed to buy ticket");
            return;
        }

        var firstOrDefault = _unitOfWork.SeatsRepository.GetAll().FirstOrDefault(seat => seat.SeatNumber == SeatNumber);
        if (firstOrDefault != null) firstOrDefault.IsTaken = true;
        _unitOfWork.Complete();
        var result = MessageBox.Show("Ticket bought successfully", "Confirmation", MessageBoxButton.OK);
        switch (result)
        {
            case MessageBoxResult.OK:
                Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
                CloseRegistrationWindow(App.ServiceProvider.GetRequiredService<SeatBookingReservationForm>());
                break;
            case MessageBoxResult.Cancel:
            case MessageBoxResult.None:
            case MessageBoxResult.Yes:
            case MessageBoxResult.No:
            default:
                throw new ArgumentOutOfRangeException();
        }

        Task.Run(App.ServiceProvider.GetRequiredService<SeatBookingWindowViewModel>()
            .SetCurrentMovie); // Run the method on a separate thread
        OnCancel();
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    #pragma warning disable CA1822
    public void OnCancel()
    {
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        CloseRegistrationWindow(App.ServiceProvider.GetRequiredService<SeatBookingReservationForm>());
    }

    private static void CloseRegistrationWindow(Window seatBookingRegistrationForm)
    {
        if (!seatBookingRegistrationForm.IsVisible) return;
        seatBookingRegistrationForm.Hide();
    }

    private bool CheckInputs()
    {
        return Payment is null || !StringHelper.IsWholeNumberOrDecimal(Payment);
    }

    private bool IsSeatNotAvailable()
    {
        return _unitOfWork.SeatsRepository.GetAll().FirstOrDefault(s => s.SeatNumber == SeatNumber)?.IsTaken ?? false;
    }
}