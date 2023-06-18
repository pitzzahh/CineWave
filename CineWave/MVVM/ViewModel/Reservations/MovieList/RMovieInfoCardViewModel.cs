using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CineWave.Helpers;
using CineWave.Messages.ManageMovies;
using CineWave.MVVM.Model.Movies;
using CineWave.MVVM.View.Reservations.MovieList;
using CineWave.MVVM.View.Reservations.SeatBooking;
using CineWave.MVVM.ViewModel.Reservations.SeatBooking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Reservations.MovieList;

public partial class RMovieInfoCardViewModel : BaseViewModel
{
    [ObservableProperty] private string _movieName = null!;
    [ObservableProperty] private double _moviePrice;
    [ObservableProperty] private DateOnly _releaseDate;
    [ObservableProperty] private TimeOnly _runtime;
    [ObservableProperty] private DateTime _screeningDateTime;

    public RMovieInfoCardViewModel(string movieName, double moviePrice, TimeOnly runtime, DateOnly releaseDate,
        DateTime screeningDateTime)
    {
        MovieName = movieName;
        MoviePrice = moviePrice;
        Runtime = runtime;
        ReleaseDate = releaseDate;
        ScreeningDateTime = screeningDateTime;
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    #pragma warning disable CA1822
    public void OpenForm()
    {
        WindowHelper.ShowOrCloseWindow((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<SeatBookingWindow>());
        var movieInfo = new EditMovieInfo(
            MovieName,
            Runtime,
            MoviePrice,
            ReleaseDate,
            ScreeningDateTime
        );
        WeakReferenceMessenger.Default.Send(new GetMovieInfoMessage(movieInfo));
        Task.Run((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<SeatBookingWindowViewModel>().SetCurrentMovie);
    }
}