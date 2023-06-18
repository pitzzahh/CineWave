using System;
using System.Threading.Tasks;
using CineWave.Components;
using CineWave.MVVM.View;
using CineWave.MVVM.View.Login;
using CineWave.MVVM.View.Reservations.MovieList;
using CineWave.MVVM.ViewModel.AddMovie;
using CineWave.MVVM.ViewModel.Gallery;
using CineWave.MVVM.ViewModel.ManageMovies;
using CineWave.MVVM.ViewModel.Reservations;
using CineWave.MVVM.ViewModel.SeatsBooking;
using CineWave.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty] private INavigationService _navService = null!;

    public MainViewModel(INavigationService navigationService)
    {
        NavService = navigationService;
    }

    [RelayCommand]
    public void NavigateToHome()
    {
        NavService.NavigateTo<HomeViewModel>();
        (App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<MainWindow>().GalleryButton.IsChecked = true;
        // Run the method on a separate thread
        Task.Run(App.ServiceProvider.GetRequiredService<HomeViewModel>().GetMoviesFromApi);
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedMember.Global
    public void NavigateToReservations()
    {
        NavService.NavigateTo<ReservationsViewModel>();
        // Run the method on a separate thread
        Task.Run((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<ReservationsViewModel>().CreateMovieInfoCards);
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedMember.Global
    public void NavigateToSeatBooking()
    {
        NavService.NavigateTo<SeatBookingViewModel>();
        // Run the method on a separate thread
        Task.Run((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<SeatBookingViewModel>().SetCurrentMovie);
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedMember.Global
    public void NavigateToAddMovie()
    {
        NavService.NavigateTo<AddMovieViewModel>();
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedMember.Global
    public void NavigateToManageMovie()
    {
        NavService.NavigateTo<ManageMoviesViewModel>();
        // Run the method on a separate thread
        Task.Run((App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<ManageMoviesViewModel>().CreateMovieInfoCards);
    }

    [RelayCommand]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedMember.Global
    #pragma warning disable CA1822
    public void Logout()
    {
        (App.ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<MainWindow>().GalleryButton.IsChecked = true;
        App.ServiceProvider.GetRequiredService<MainWindow>().Hide();
        App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>().Hide();
        App.ServiceProvider.GetRequiredService<MovieListWindow>().Hide();
        App.ServiceProvider.GetRequiredService<EditMovieForm>().Hide();
        App.ServiceProvider.GetRequiredService<LoginWindow>().Show();
    }
}