using CineWave.Core;
using CineWave.Services;

namespace CineWave.MVVM.ViewModel;

public class MainViewModel : Core.ViewModel
{
    private readonly INavigationService _navigation = null!;

    private INavigationService NavigationService
    {
        get => _navigation;
        init
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToHomeViewCommand { get; set; }
    public RelayCommand NavigateToSettingsViewCommand { get; set; }
    
    public MainViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigateToHomeViewCommand = new RelayCommand(o => true, o =>
        {
            NavigationService.NavigateTo<HomeViewModel>();
        });
        NavigateToSettingsViewCommand = new RelayCommand(o => true, o =>
        {
            NavigationService.NavigateTo<ReservationsViewModel>();
        });
    }

}