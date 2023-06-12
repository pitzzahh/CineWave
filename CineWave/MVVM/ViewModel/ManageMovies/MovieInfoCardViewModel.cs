using System;
using System.Diagnostics;
using CineWave.Components;
using CineWave.DB.Core;
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
    [ObservableProperty] private TimeOnly _runtime;
    [ObservableProperty] private DateOnly _releaseDate;
    [ObservableProperty] private DateTime _screeningDateTime;

    private readonly IUnitOfWork _unitOfWork;

    public MovieInfoCardViewModel(string movieName, double moviePrice, TimeOnly runtime, DateOnly releaseDate,
        DateTime screeningDateTime, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        MovieName = movieName;
        MoviePrice = moviePrice;
        Runtime = runtime;
        ReleaseDate = releaseDate;
        ScreeningDateTime = screeningDateTime;
    }

    [RelayCommand]
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
            editMovieForm.Show();
            var movieInfo = new EditMovieInfo(
                MovieName,
                Runtime,
                MoviePrice,
                ReleaseDate,
                ScreeningDateTime
            );
            WeakReferenceMessenger.Default.Send(new GetMovieInfoMessage(movieInfo));
        }
    }
}