using System.Diagnostics;
using System.Threading.Tasks;
using CineWave.Components;
using CineWave.MVVM.View;
using CineWave.MVVM.View.Login;
using CineWave.MVVM.ViewModel.AddMovie;
using CineWave.MVVM.ViewModel.Gallery;
using CineWave.MVVM.ViewModel.ManageMovies;
using CineWave.MVVM.ViewModel.Reservations;
using CineWave.MVVM.ViewModel.SeatsBooking;
using CineWave.MVVM.ViewModel.Trailer;
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

    public void ShowTrailer()
    {
        NavService.NavigateTo<TrailerViewModel>();
    }

    [RelayCommand]
    public void NavigateToHome()
    {
        NavService.NavigateTo<HomeViewModel>();
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        Task.Run(App.ServiceProvider.GetRequiredService<HomeViewModel>()
            .GetMoviesFromApi); // Run the method on a separate thread
    }

    [RelayCommand]
    public void NavigateToReservations()
    {
        NavService.NavigateTo<ReservationsViewModel>();
    }

    [RelayCommand]
    public void NavigateToSeatBooking()
    {
        NavService.NavigateTo<SeatBookingViewModel>();
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        Task.Run(App.ServiceProvider.GetRequiredService<SeatBookingViewModel>()
            .SetCurrentMovie); // Run the method on a separate thread
    }

    [RelayCommand]
    public void NavigateToAddMovie()
    {
        NavService.NavigateTo<AddMovieViewModel>();
    }

    [RelayCommand]
    public void NavigateToManageMovie()
    {
        NavService.NavigateTo<ManageMoviesViewModel>();
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        Task.Run(App.ServiceProvider.GetRequiredService<ManageMoviesViewModel>()
            .CreateMovieInfoCards); // Run the method on a separate thread
    }

    [RelayCommand]
    public void Logout()
    {
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().GalleryButton.IsChecked = true;
        App.ServiceProvider.GetRequiredService<MainWindow>().Hide();
        App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>().Hide();
        App.ServiceProvider.GetRequiredService<EditMovieForm>().Hide();
        App.ServiceProvider.GetRequiredService<LoginWindow>().Show();
    }
}