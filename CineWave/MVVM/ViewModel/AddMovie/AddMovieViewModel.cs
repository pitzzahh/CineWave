using System;
using CineWave.DB;
using CineWave.Helpers;
using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CineWave.MVVM.ViewModel.AddMovie;

public partial class AddMovieViewModel : BaseViewModel
{
    [ObservableProperty] private readonly string? _movieName;
    [ObservableProperty] private string? _price;
    [ObservableProperty] private DateTime _releaseDate;

    [RelayCommand]
    public void AddMovie()
    {
        if (MovieName is null || Price is null || !StringHelper.IsWholeNumberOrDecimal(Price))
        {
            
        }
        using var moviesDataContext = new MoviesDataContext();
        moviesDataContext.Movies.Add(new Movie(MovieName, false, ReleaseDate));
        moviesDataContext.SaveChangesAsync();
    }
}