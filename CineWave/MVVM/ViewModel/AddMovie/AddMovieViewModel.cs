using System;
using System.Windows.Threading;
using CineWave.DB;
using CineWave.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace CineWave.MVVM.ViewModel.AddMovie;

public partial class AddMovieViewModel : BaseViewModel
{

    [ObservableProperty] private string _movieName;
    [ObservableProperty] private double _price;
    [ObservableProperty] private DateOnly _releaseDate;

    [RelayCommand]
    public void AddMovie()
    {
        using MoviesDataContext moviesDataContext = new();
        moviesDataContext.Movies.Add(new Movie {MovieName = MovieName, Price = Price, ReleaseDate = ReleaseDate, NowShowing = true});
        moviesDataContext.SaveChangesAsync();
    }
}