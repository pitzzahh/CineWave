using System;
using System.Diagnostics;
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

namespace CineWave.MVVM.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty] private INavigationService _navigation;

    public MainViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
    }

    [RelayCommand]
    public void NavigateToHome()
    {
        Navigation.NavigateTo<HomeViewModel>();
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        try
        {
            Task.Run(App.ServiceProvider.GetRequiredService<HomeViewModel>().GetMoviesFromApi) ; // Run the method on a separate thread
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
    public void Logout()
    {
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().GalleryButton.IsChecked = true;
        App.ServiceProvider.GetRequiredService<MainWindow>().Hide();
        App.ServiceProvider.GetRequiredService<LoginViewModel>().IsLoginFormVisible = true;
        App.ServiceProvider.GetRequiredService<LoginWindow>().UsernameInput.Focus();
        App.ServiceProvider.GetRequiredService<SeatBookingRegistrationFormBaseViewModel>().OnCancel();
    }
}