using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CineWave.MVVM.View;
using CineWave.MVVM.View.Login;
using CineWave.MVVM.ViewModel.AddMovie;
using CineWave.MVVM.ViewModel.Gallery;
using CineWave.MVVM.ViewModel.Login;
using CineWave.MVVM.ViewModel.Reservations;
using CineWave.MVVM.ViewModel.SeatsBooking;
using CineWave.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace CineWave.MVVM.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private INavigationService _navigation;

    public MainViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToHome();
    }
    
    [RelayCommand]
    public void NavigateToHome()
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
    }
    
    [RelayCommand]
    public void NavigateToReservations()
    {
        Navigation.NavigateTo<ReservationsViewModel>();
    }
    
    [RelayCommand]
    public void NavigateToSeatBooking()
    {
        Navigation.NavigateTo<SeatBookingViewModel>();
    }
    
    [RelayCommand]
    public void NavigateToAddMovie()
    {
        Navigation.NavigateTo<AddMovieViewModel>();
    }

    [RelayCommand]
    public void NavigateToLogin()
    {
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().GalleryButton.IsChecked = true;
        App.ServiceProvider.GetRequiredService<MainWindow>().Hide();
        App.ServiceProvider.GetRequiredService<LoginViewModel>().IsLogInFormVisible = true;
        App.ServiceProvider.GetRequiredService<LoginWindow>().UsernameInput.Focus();
    }
}