using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

public partial class SeatBookingRegistrationFormBaseViewModel : BaseViewModel, IRecipient<GetSeatInfoMessage>
{
    [ObservableProperty] private string? _movieName;
    [ObservableProperty] private string? _moviePrice;
    [ObservableProperty] private string? _seatNumber;
    [ObservableProperty] private string? _payment;
    private readonly IUnitOfWork _unitOfWork;

    public SeatBookingRegistrationFormBaseViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        WeakReferenceMessenger.Default.Register(this);
    }

    [RelayCommand]
    public async Task OnBuy()
    {
        if (!CheckInputs()) return; // TODO: fix checking inputs
        Movie? currentMovie = null;
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            currentMovie = _unitOfWork.MoviesRepository.GetAll().FirstOrDefault(m => m.NowShowing);
        });
        if (currentMovie != null)
        {
            if (!IsSeatAvailable().Result)
            {
                MessageBox.Show("Seat is not available");
                return;
 
            }
            var ticket = new Ticket(currentMovie.MovieId, currentMovie.MoviePrice);
            var customer = new Customer(0, ticket.TicketId);
            Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
            CloseRegistrationWindow(App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>());
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
        return string.IsNullOrEmpty(Payment) || !IsValidPayment();
    }

    private bool IsValidPayment()
    {
        return decimal.TryParse(Payment, out var paymentAmount) && paymentAmount >= 0;
    }
    
    public async Task<bool> IsSeatAvailable()
    {
        var result = false;
        await Application.Current.Dispatcher.InvokeAsync(() =>
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