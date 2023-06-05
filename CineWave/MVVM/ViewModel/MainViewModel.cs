using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CineWave.Core;
using CineWave.MVVM.View;
using CineWave.MVVM.View.Login;
using CineWave.MVVM.ViewModel.AddMovie;
using CineWave.MVVM.ViewModel.Gallery;
using CineWave.MVVM.ViewModel.Login;
using CineWave.MVVM.ViewModel.Reservations;
using CineWave.MVVM.ViewModel.SeatsBooking;
using CineWave.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel;

public class MainViewModel : Core.ViewModel
{
    private readonly INavigationService _navigation = null!;

    public INavigationService Navigation
    {
        get => _navigation;
        init
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToLogin { get; set; }
    public RelayCommand NavigateToHome { get; set; }
    public RelayCommand NavigateToReservations { get; set; }
    public RelayCommand NavigateToSeatBooking { get; set; }
    public RelayCommand NavigateToAddMovie { get; set; }

    public MainViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToLogin = new RelayCommand(o =>
        {
            Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
            App.ServiceProvider.GetRequiredService<MainWindow>().GalleryButton.IsChecked = true;
            App.ServiceProvider.GetRequiredService<MainWindow>().Hide();
            App.ServiceProvider.GetRequiredService<LoginViewModel>().IsLoginFormVisible = true;
            App.ServiceProvider.GetRequiredService<LoginWindow>().UsernameInput.Focus();
        }, o => true);
        NavigateToHome = new RelayCommand(o =>
        {
            Navigation.NavigateTo<HomeViewModel>();
            Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
            var homeViewModel = App.ServiceProvider.GetRequiredService<HomeViewModel>();
            if (homeViewModel.MovieCardViewModels.Any()) return;
            try
            {
                Task.Run(homeViewModel.GetMoviesFromApi); // Run the method on a separate thread
            }
            catch (Exception e)
            {
                Debug.Print(e.StackTrace);
            }
        }, o => true);
        NavigateToReservations = new RelayCommand(o => { Navigation.NavigateTo<ReservationsViewModel>(); }, o => true);
        NavigateToSeatBooking = new RelayCommand(o => { Navigation.NavigateTo<SeatBookingViewModel>(); }, o => true);
        NavigateToAddMovie = new RelayCommand(o => { Navigation.NavigateTo<AddMovieViewModel>(); }, o => true);
        Navigation.NavigateTo<HomeViewModel>();
    }
}