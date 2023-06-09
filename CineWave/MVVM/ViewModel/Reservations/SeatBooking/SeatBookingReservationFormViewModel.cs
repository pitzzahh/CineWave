﻿using System;
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
using CineWave.MVVM.Model.Reservations;
using CineWave.MVVM.View.Reservations.MovieList;
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
    [ObservableProperty] private string? _customerName;
    [ObservableProperty] private string? _moviePrice;
    [ObservableProperty] private string? _payment;
    [ObservableProperty] private string? _seatNumber;

    public SeatBookingReservationFormViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        WeakReferenceMessenger.Default.Register(this);
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public void OnBuy()
    {
        if (CustomerName is null)
        {
            MessageBox.Show("Please enter customer name");
            return;
        }

        if (MoviePrice != "0" && CheckInputs())
        {
            MessageBox.Show("Please enter a valid payment");
            return;
        }

        var currentMovie = _unitOfWork.MoviesRepository.GetMovieByName(MovieName ?? string.Empty);
 
        if (currentMovie == null) return;
        if (currentMovie.MovieId != 0 && MoviePrice != "0" && Payment != null &&
            double.Parse(Payment) < currentMovie.Price)
        {
            MessageBox.Show("Payment is not enough");
            return;
        }

        var currentSeat = _unitOfWork.SeatsRepository
            .Find(s => s.MovieId == currentMovie.MovieId && s.SeatNumber == SeatNumber)
            .FirstOrDefault();
        
        if (currentSeat == null) return;

        if (currentSeat.IsTaken)
        {
            MessageBox.Show("Seat is not available");
            return;
        }

        var ticket = new Ticket(currentMovie.MovieId, currentSeat.SeatId);
        _unitOfWork.TicketsRepository.Add(ticket);
        
        var addTicketResult = _unitOfWork.Complete();
        
        if (addTicketResult == 0) return;
        
        var addedTicket = _unitOfWork.TicketsRepository
            .Find(t => t.MovieId == currentMovie.MovieId && t.SeatId == currentSeat.SeatId)
            .FirstOrDefault();
        
        if (addedTicket == null) return;
        
        var customer = new Customer(addedTicket.TicketId)
        {
            Name = CustomerName
        };
        _unitOfWork.CustomersRepository.Add(customer);
        var customerAddResult = _unitOfWork.Complete();
        if (customerAddResult == 0) return;

        var savedCustomer = _unitOfWork.CustomersRepository
            .Find(c => c.Name == CustomerName && c.TicketId == addedTicket.TicketId)
            .FirstOrDefault();
        
        if (savedCustomer == null) return;
        var reservation = new Reservation(savedCustomer.CustomerId, DateOnly.FromDateTime(DateTime.Now));  
                                                                                                           
        _unitOfWork.ReservationsRepository.Add(reservation);

        var reservationAddResult = _unitOfWork.Complete();
        if (reservationAddResult == 0)
        {
            MessageBox.Show("Failed to avail reservation");
            return;
        }
        
        var reservedSeat = _unitOfWork.SeatsRepository.GetAll()                                            
            .FirstOrDefault(seat => seat.MovieId == currentMovie.MovieId && seat.SeatId == currentSeat.SeatId);
        
        if (reservedSeat != null) reservedSeat.IsTaken = true;
        var seatTakenChange = _unitOfWork.Complete();
        if (seatTakenChange == 0) return;
        var result = MessageBox.Show("Ticket bought successfully", "Confirmation", MessageBoxButton.OK);
        switch (result)
        {
            case MessageBoxResult.OK:
                OnCancel();
                break;
            case MessageBoxResult.Cancel:
            case MessageBoxResult.None:
            case MessageBoxResult.Yes:
            case MessageBoxResult.No:
            default:
                throw new ArgumentOutOfRangeException();
        }

        CustomerName = "";
        Payment = "";
        Task.Run((App.ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<SeatBookingWindowViewModel>()
            .SetCurrentMovie); // Run the method on a separate thread
        WindowHelper.HideWindow(App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>());
        WindowHelper.HideWindow(App.ServiceProvider.GetRequiredService<SeatBookingWindow>());
        WindowHelper.HideWindow(App.ServiceProvider.GetRequiredService<MovieListWindow>());
        // Run the method on a separate thread
        Task.Run((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<ReservationsViewModel>().CreateMovieInfoCards);
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    #pragma warning disable CA1822
    // ReSharper disable once MemberCanBeMadeStatic.Global
    public void OnCancel()
    {
        WindowHelper.HideWindow((App.ServiceProvider ?? throw new InvalidOperationException())
            .GetRequiredService<SeatBookingReservationForm>());
    }

    private bool CheckInputs()
    {
        return Payment is null || !StringHelper.IsWholeNumberOrDecimal(Payment);
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