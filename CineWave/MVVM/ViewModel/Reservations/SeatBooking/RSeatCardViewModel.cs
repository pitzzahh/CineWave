using System;
using System.Windows;
using CineWave.Components;
using CineWave.Helpers;
using CineWave.Messages.ManageMovies;
using CineWave.Messages.Reservations;
using CineWave.Messages.SeatsBooking;
using CineWave.MVVM.Model.SeatsBooking;
using CineWave.MVVM.View.Reservations.SeatBooking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Reservations.SeatBooking;

public partial class RSeatCardViewModel : BaseViewModel, IRecipient<GetMovieInfoMessage>
{
    private string MovieName { get; set; } = null!;
    private double MoviePrice { get; set; }
    [ObservableProperty] private bool _isSeatAvailable;
    [ObservableProperty] private string? _seatNumber;

    public RSeatCardViewModel(string seatNumber, bool isTaken)
    {
        SeatNumber = seatNumber;
        IsSeatAvailable = !isTaken;
        WeakReferenceMessenger.Default.Register(this);
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
        WeakReferenceMessenger.Default.Send(new RGetSeatInfoMessage(new BookMovieInfo(MovieName, MoviePrice, SeatNumber ?? "")));
    }

    public void Receive(GetMovieInfoMessage message)
    {
        var movieInfo = message.Value;
        MovieName = movieInfo.MovieName;
        MoviePrice = movieInfo.MoviePrice;
    }
}