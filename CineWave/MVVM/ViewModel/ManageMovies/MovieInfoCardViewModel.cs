using System;
using System.Diagnostics;
using CineWave.Components;
using CineWave.Messages.ManageMovies;
using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.ManageMovies;

public partial class MovieInfoCardViewModel : BaseViewModel
{
    [ObservableProperty] private string _movieName = null!;
    [ObservableProperty] private double _moviePrice;
    [ObservableProperty] private DateOnly _releaseDate;
    [ObservableProperty] private TimeOnly _runtime;
    [ObservableProperty] private DateTime _screeningDateTime;

    public MovieInfoCardViewModel(string movieName, double moviePrice, TimeOnly runtime, DateOnly releaseDate,
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
    public void OpenForm()
    {
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        var editMovieForm = App.ServiceProvider.GetRequiredService<EditMovieForm>();
        if (editMovieForm.IsVisible)
        {
            editMovieForm.Hide();
        }
        else
        {
            var movieInfo = new EditMovieInfo(
                MovieName,
                Runtime,
                MoviePrice,
                ReleaseDate,
                ScreeningDateTime
            );
            WeakReferenceMessenger.Default.Send(new GetMovieInfoMessage(movieInfo));
            editMovieForm.Show();
        }
    }
}