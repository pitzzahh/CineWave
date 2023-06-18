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

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingRegistrationFormViewModel : BaseViewModel, IRecipient<GetSeatInfoMessage>
{
    private readonly IUnitOfWork _unitOfWork;
    [ObservableProperty] private string _isMovieFree = "Visible";
    [ObservableProperty] private string? _movieName;
    [ObservableProperty] private string? _moviePrice;
    [ObservableProperty] private string? _payment;
    [ObservableProperty] private string? _seatNumber;

    public SeatBookingRegistrationFormViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        WeakReferenceMessenger.Default.Register(this);
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public void OnBuy()
    {
        if (MoviePrice != "0" && CheckInputs())
        {
            MessageBox.Show("Please enter a valid payment");
            return;
        }

        var currentMovie = _unitOfWork.MoviesRepository.GetMovieByName(MovieName ?? string.Empty);

        if (currentMovie == null) return;
        if (currentMovie.MovieId != 0 && MoviePrice != "0" && Payment != null &&
            double.Parse(Payment) < currentMovie.MoviePrice)
        {
            MessageBox.Show("Payment is not enough");
            return;
        }

        var isSeatNotAvailable = IsSeatNotAvailable();
        if (isSeatNotAvailable)
        {
            MessageBox.Show("Seat is not available");
            return;
        }

        if (SeatNumber != null)
        {
            var ticket = new Ticket(currentMovie.MovieId, SeatNumber);
            _unitOfWork.TicketsRepository.Add(ticket);
            var addTicketResult = _unitOfWork.Complete();
            if (addTicketResult != 0)
            {
                var ticketResult = _unitOfWork.TicketsRepository
                    .Find(t => t.MovieId == currentMovie.MovieId && t.SeatNumber == SeatNumber).FirstOrDefault();
                Debug.Assert(ticketResult != null, nameof(ticketResult) + " != null");
                var customer = new Customer(ticketResult.TicketId);
                _unitOfWork.CustomersRepository.Add(customer);
            }
        }

        var complete = _unitOfWork.Complete();
        if (complete == 0)
        {
            MessageBox.Show("Failed to buy ticket");
            return;
        }

        var firstOrDefault = _unitOfWork.SeatsRepository
            .Find(seat => seat.MovieId == currentMovie.MovieId && seat.SeatNumber == SeatNumber).FirstOrDefault();
        if (firstOrDefault != null) firstOrDefault.IsTaken = true;

        var setTakenResult = _unitOfWork.Complete();
        if (setTakenResult == 0) return;
        var result = MessageBox.Show("Ticket bought successfully", "Confirmation", MessageBoxButton.OK);
        switch (result)
        {
            case MessageBoxResult.OK:
                WindowHelper.ShowOrCloseWindow((App.ServiceProvider ?? throw new InvalidOperationException())
                    .GetRequiredService<SeatBookingReservationForm>());
                break;
            case MessageBoxResult.Cancel:
            case MessageBoxResult.None:
            case MessageBoxResult.Yes:
            case MessageBoxResult.No:
            default:
                throw new ArgumentOutOfRangeException();
        }

        Payment = "";
        // Run the method on a separate thread
        Task.Run(App.ServiceProvider.GetRequiredService<SeatBookingViewModel>().SetCurrentMovie);
        OnCancel();
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    #pragma warning disable CA1822
    public void OnCancel()
    {
        WindowHelper.ShowOrCloseWindow((App.ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<SeatBookingReservationForm>());
    }

    private bool CheckInputs()
    {
        return Payment is null || !StringHelper.IsWholeNumberOrDecimal(Payment);
    }

    private bool IsSeatNotAvailable()
    {
        return _unitOfWork.SeatsRepository.GetAll().FirstOrDefault(s => s.SeatNumber == SeatNumber)?.IsTaken ?? false;
    }

    public void Receive(GetSeatInfoMessage message)
    {
        var reservationInfo = message.Value;
        MovieName = reservationInfo.MovieName;
        IsMovieFree = reservationInfo.MoviePrice == 0 ? "Hidden" : "Visible";
        MoviePrice = reservationInfo.MoviePrice.ToString(CultureInfo.InvariantCulture);
        SeatNumber = reservationInfo.SeatNumber;
    }
}