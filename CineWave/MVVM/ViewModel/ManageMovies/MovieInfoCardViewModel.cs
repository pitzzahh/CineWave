using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.ManageMovies;

public partial class MovieInfoCardViewModel : BaseViewModel
{
    [ObservableProperty] private string? _movieName;

    [ObservableProperty] private double _moviePrice;

    [ObservableProperty] private DateTime _releaseDate;

    [ObservableProperty] private bool _nowShowing;

    public MovieInfoCardViewModel(string movieName, double moviePrice, DateTime releaseDate, bool nowShowing)
    {
        MovieName = movieName;
        MoviePrice = moviePrice;
        ReleaseDate = releaseDate;
        NowShowing = nowShowing;
    }
}