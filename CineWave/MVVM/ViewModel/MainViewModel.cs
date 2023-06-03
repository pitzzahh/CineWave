using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CineWave.Core;
using CineWave.MVVM.View;
using CineWave.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel;

public class MainViewModel : Core.ViewModel
{
    private INavigationService _navigation = null!;

    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToLogin { get; set; }
    public RelayCommand NavigateToHome { get; set; }
    public RelayCommand NavigateToReservations { get; set; }

    public MainViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToLogin = new RelayCommand(o =>
        {
            Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
            App.ServiceProvider.GetRequiredService<MainWindow>().GalleryButton.IsChecked = true;
            App.ServiceProvider.GetRequiredService<MainWindow>().Hide();
            App.ServiceProvider.GetRequiredService<LoginViewModel>().IsLoginFormVisible = true;
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
        Navigation.NavigateTo<HomeViewModel>();
    }
}