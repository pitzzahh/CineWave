using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.ManageMovies;

public partial class MovieInfoCardViewModel : BaseViewModel
{
    [ObservableProperty] private string _movieName = null!;
    [ObservableProperty] private double _moviePrice;
    [ObservableProperty] private TimeOnly _runtime;
    [ObservableProperty] private DateOnly _releaseDate;
    [ObservableProperty] private DateTime _screeningDateTime;

    public MovieInfoCardViewModel(string movieName, double moviePrice, TimeOnly runtime, DateOnly releaseDate, DateTime screeningDateTime)
    {
        MovieName = movieName;
        MoviePrice = moviePrice;
        Runtime = runtime;
        ReleaseDate = releaseDate;
        ScreeningDateTime = screeningDateTime;
    }
}