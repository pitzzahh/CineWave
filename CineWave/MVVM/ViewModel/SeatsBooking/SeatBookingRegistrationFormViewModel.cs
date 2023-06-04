using System.Diagnostics;
using System.Windows;
using CineWave.Components;
using CineWave.Core;
using CineWave.MVVM.Model;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public class SeatBookingRegistrationFormViewModel : Core.ViewModel
{
    
    private string? _movieName;
    private string? _seatNumber;
    private string? _customerName;
    private string? _payment;
    
    public string? MovieName
    {
        get => _movieName;
        set
        {
            if (value == _movieName) return;
            _movieName = value;
            OnPropertyChanged();
        }
    }

    public string? SeatNumber
    {
        get => _seatNumber;
        set
        {
            if (value == _seatNumber) return;
            _seatNumber = value;
            OnPropertyChanged();
        }
    }

    public string? CustomerName
    {
        get => _customerName;
        set
        {
            if (value == _customerName) return;
            _customerName = value;
            OnPropertyChanged();
        }
    }

    public string? Payment
    {
        get => _payment;
        set
        {
            if (value == _payment) return;
            _payment = value;
            OnPropertyChanged();
        }
    }

    public Customer? Customer { get; set; }
    public RelayCommand OnBuy { get; set; }
    public RelayCommand OnCancel { get; set; }

    public SeatBookingRegistrationFormViewModel()
    {
        OnBuy = new RelayCommand(o =>
        {
            Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
            CloseRegistrationWindow(App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>());
        }, o => CheckInputs());
        OnCancel = new RelayCommand(o =>
        {
            Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
            CloseRegistrationWindow(App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>());
        }, o => true);
    }

    private void CloseRegistrationWindow(Window seatBookingRegistrationForm)
    {
        if (seatBookingRegistrationForm.IsVisible)
        {
            seatBookingRegistrationForm.Hide();
            MovieName = "";
            SeatNumber = "";
            CustomerName = "";
            Payment = "";
            Customer = null;
        }
    }

    private bool CheckInputs()
    {
        return string.IsNullOrEmpty(_customerName) || string.IsNullOrEmpty(_payment) || !IsValidPayment();
    }

    private bool IsValidPayment()
    {
        return decimal.TryParse(_payment, out var paymentAmount) && paymentAmount >= 0;
    }
}