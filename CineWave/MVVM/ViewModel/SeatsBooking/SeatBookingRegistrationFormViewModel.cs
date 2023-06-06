﻿using System.Diagnostics;
using System.Windows;
using CineWave.Components;
using CineWave.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingRegistrationFormViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _movieName;
    [ObservableProperty]
    private string? _seatNumber;
    [ObservableProperty]
    private string? _customerName;
    [ObservableProperty]
    private string? _payment;
    public Customer? Customer { get; set; }

    [RelayCommand]
    public void OnBuy()
    {
        if (CheckInputs()) // TODO: fix checking inputs
        {
            var customer = new Customer(0, CustomerName);
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
        SeatNumber = "";
        CustomerName = "";
        Payment = "";
        Customer = null;
    }

    private bool CheckInputs()
    {
        return string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(Payment) || !IsValidPayment();
    }

    private bool IsValidPayment()
    {
        return decimal.TryParse(Payment, out var paymentAmount) && paymentAmount >= 0;
    }
}