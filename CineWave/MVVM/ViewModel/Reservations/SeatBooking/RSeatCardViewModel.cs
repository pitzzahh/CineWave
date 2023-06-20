using System;
using System.Windows;
using CineWave.Helpers;
using CineWave.Messages.SeatsBooking;
using CineWave.MVVM.Model.SeatsBooking;
using CineWave.MVVM.View.Reservations.SeatBooking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Reservations.SeatBooking;

public partial class RSeatCardViewModel : BaseViewModel
{
    private static string MovieName { get; set; } = null!;
    private static double MoviePrice { get; set; }
    [ObservableProperty] private bool _isSeatAvailable;
    [ObservableProperty] private string _seatNumber;

    public RSeatCardViewModel(string movieName,double moviePrice, string seatNumber, bool isTaken)
    {
        MovieName = movieName;
        MoviePrice = moviePrice;
        SeatNumber = seatNumber;
        IsSeatAvailable = !isTaken;
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    public void OpenForm()
    {
        if (!IsSeatAvailable)
        {
            MessageBox.Show("This seat is already taken or no movie is currently showing!");
            return;
        }
        WindowHelper.ShowOrCloseWindow((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<SeatBookingReservationForm>());
        WeakReferenceMessenger.Default.Send(new GetSeatInfoMessage(new BookMovieInfo(MovieName, MoviePrice, SeatNumber)));
    }
}