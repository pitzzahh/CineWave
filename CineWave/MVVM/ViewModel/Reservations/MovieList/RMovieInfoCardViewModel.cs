using System;
using System.Diagnostics;
using CineWave.Components;
using CineWave.Messages.ManageMovies;
using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Reservations.MovieList;

public partial class RMovieInfoCardViewModel : BaseViewModel
{
    [ObservableProperty] private string _movieName = null!;
    [ObservableProperty] private double _moviePrice;
    [ObservableProperty] private TimeOnly _runtime;
    [ObservableProperty] private DateOnly _releaseDate;
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
    #pragma warning disable CA1822
    public void OpenForm()
    {
        Debug.Print("On Reservation");
    }
}