﻿using System.Diagnostics;
using System.Windows;
using CineWave.Components;
using CineWave.DB.Core;
using CineWave.Messages.SeatsBooking;
using CineWave.MVVM.Model.SeatsBooking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Reservations;

public partial class RSeatCardViewModel : BaseViewModel
{
    [ObservableProperty] private bool _isSeatAvailable;
    [ObservableProperty] private string? _seatNumber;
    private readonly IUnitOfWork _unitOfWork;
    
    public RSeatCardViewModel(string seatNumber, bool isTaken, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        SeatNumber = seatNumber;
        IsSeatAvailable = !isTaken;
    }

    [RelayCommand]
    public void OpenForm()
    {
        if (!IsSeatAvailable)
        {
            MessageBox.Show("This seat is already taken or no movie is currently showing!");
            return;
        }
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        var seatBookingRegistrationForm = App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>(); // 
        if (seatBookingRegistrationForm.IsVisible)
        {
            seatBookingRegistrationForm.Hide();
        }
        else
        {
            var currentMovie = _unitOfWork.MoviesRepository.GetNowShowingMovie();
            if (currentMovie == null)
            {
                MessageBox.Show("No movie is currently showing!");
                return;
            }
            seatBookingRegistrationForm.Show();
            WeakReferenceMessenger.Default.Send(new GetSeatInfoMessage(new BookMovieInfo(currentMovie.MovieName, currentMovie.MoviePrice, SeatNumber ?? "")));
        }
    }
}