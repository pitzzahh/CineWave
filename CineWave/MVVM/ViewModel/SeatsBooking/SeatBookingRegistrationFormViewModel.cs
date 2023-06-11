using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using CineWave.Components;
using CineWave.DB.Core;
using CineWave.Messages.SeatsBooking;
using CineWave.MVVM.Model;
using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingRegistrationFormViewModel : BaseViewModel, IRecipient<GetSeatInfoMessage>
{
    [ObservableProperty] private string? _movieName;
    [ObservableProperty] private string? _moviePrice;
    [ObservableProperty] private string? _seatNumber;
    [ObservableProperty] private string? _payment;
    private readonly IUnitOfWork _unitOfWork;

    public SeatBookingRegistrationFormViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        WeakReferenceMessenger.Default.Register(this);
    }

    [RelayCommand]
    public async Task OnBuy()
    {
        if (CheckInputs())
        {
            MessageBox.Show("Please enter a valid payment");
            return;
        }

        var currentMovie = _unitOfWork.MoviesRepository.GetAll().FirstOrDefault(m => m.NowShowing);

        if (currentMovie != null)
        {
            if (double.Parse(Payment ?? "0") < currentMovie.MoviePrice)
            {
                MessageBox.Show("Payment is not enough");
                return;
            }
            
            if (!IsSeatAvailable())
            {
                MessageBox.Show("Seat is not available");
                return;
            }

            if (SeatNumber != null)
            {
                var ticket = new Ticket(currentMovie.MovieId, currentMovie.MoviePrice, SeatNumber);
                var customer = new Customer(0, ticket.TicketId);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    _unitOfWork.CustomersRepository.Add(customer);
                    _unitOfWork.TicketsRepository.Add(ticket);
                });
            }

            var complete = _unitOfWork.Complete();
            if (complete == 1)
            {
                var result = MessageBox.Show("Ticket bought successfully", "Confirmation", MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
                        CloseRegistrationWindow(App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>());
                        break;
                    case MessageBoxResult.Cancel:
                        // Cancel button is clicked
                        // Perform the desired action here
                        break;
                    case MessageBoxResult.None:
                    case MessageBoxResult.Yes:
                    case MessageBoxResult.No:
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var tickets = _unitOfWork.TicketsRepository.GetAll();
                var customers = _unitOfWork.CustomersRepository.GetAll();
            }
        }
    }

    [RelayCommand]
    public void OnCancel()
    {
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        CloseRegistrationWindow(App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>());
    }

    private void CloseRegistrationWindow(Window seatBookingRegistrationForm)
    {
        if (!seatBookingRegistrationForm.IsVisible) return;
        seatBookingRegistrationForm.Hide();
        MovieName = "";
        MoviePrice = "";
        SeatNumber = "";
        Payment = "";
    }

    private bool CheckInputs()
    {
        return !IsValidPayment();
    }

    private bool IsValidPayment()
    {
        return Regex.IsMatch(Payment?? "0", @"^\d+(\.\d+)?$");
    }

    private bool IsSeatAvailable()
    {
        var result = false; 
        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            var seat = _unitOfWork.SeatsRepository.GetAll()
                .Where(s => s.SeatNumber == SeatNumber)
                .FirstOrDefault(m => m.IsTaken);
            result = seat?.IsTaken ?? false;
        });
        return result;
    }

    public void Receive(GetSeatInfoMessage message)
    {
        var reservationInfo = message.Value;
        MovieName = reservationInfo.MovieName;
        MoviePrice = reservationInfo.MoviePrice.ToString(CultureInfo.InvariantCulture);
        SeatNumber = reservationInfo.SeatNumber;
    }
}