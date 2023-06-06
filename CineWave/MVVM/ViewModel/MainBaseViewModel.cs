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

public partial class MainBaseViewModel : BaseViewModel
{
    [ObservableProperty] private INavigationService _navigation;

    public MainBaseViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
    }

    [RelayCommand]
    public void NavigateToHome()
    {
        Navigation.NavigateTo<HomeBaseViewModel>();
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        try
        {
            Task.Run(App.ServiceProvider.GetRequiredService<HomeBaseViewModel>().GetMoviesFromApi) ; // Run the method on a separate thread
        }
        catch (Exception e)
        {
            Debug.Print(e.StackTrace);
        }
    }

    [RelayCommand]
    public void NavigateToReservations()
    {
        Navigation.NavigateTo<ReservationsBaseViewModel>();
    }

    [RelayCommand]
    public void NavigateToSeatBooking()
    {
        Navigation.NavigateTo<SeatBookingBaseViewModel>();
    }

    [RelayCommand]
    public void NavigateToAddMovie()
    {
        Navigation.NavigateTo<AddMovieBaseViewModel>();
    }

    [RelayCommand]
    public void Logout()
    {
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().GalleryButton.IsChecked = true;
        App.ServiceProvider.GetRequiredService<MainWindow>().Hide();
        App.ServiceProvider.GetRequiredService<LoginBaseViewModel>().IsLogInFormVisible = true;
        App.ServiceProvider.GetRequiredService<LoginWindow>().UsernameInput.Focus();
        App.ServiceProvider.GetRequiredService<SeatBookingRegistrationFormBaseViewModel>().OnCancel();
    }
}