using CineWave.Core;
using CineWave.Services;

namespace CineWave.MVVM.ViewModel;

public class MainViewModel : Core.ViewModel
{
    private readonly INavigationService _navigation = null!;

    private INavigationService Navigation
    {
        get => _navigation;
        init
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToLoginViewCommand { get; set; }
    public RelayCommand NavigateToHomeViewCommand { get; set; }
    public RelayCommand NavigateToSettingsViewCommand { get; set; }
    
    public MainViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToLoginViewCommand = new RelayCommand(o => true, o =>
        {
            Navigation.NavigateTo<LoginViewModel>();
        });
        NavigateToHomeViewCommand = new RelayCommand(o => true, o =>
        {
            Navigation.NavigateTo<DashboardViewModel>();
        });
        NavigateToSettingsViewCommand = new RelayCommand(o => true, o =>
        {
            Navigation.NavigateTo<ReservationsViewModel>();
        });
    }

}