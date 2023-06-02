using CineWave.Core;
using CineWave.Services;

namespace CineWave.MVVM.ViewModel;

public class MainViewModel : Core.ViewModel
{
    private INavigationService _navigation;

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
        NavigateToLogin = new RelayCommand(o => { Navigation.NavigateTo<LoginViewModel>(); }, o => true);
        NavigateToHome = new RelayCommand(o => { Navigation.NavigateTo<HomeViewModel>(); }, o => true);
        NavigateToReservations = new RelayCommand(o => { Navigation.NavigateTo<ReservationsViewModel>(); }, o => true);
    }
}