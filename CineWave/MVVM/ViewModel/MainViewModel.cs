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
    public RelayCommand NavigateToGalleryViewCommand { get; set; }
    public RelayCommand NavigateToReservationsViewCommand { get; set; }
    
    public MainViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToLoginViewCommand = new RelayCommand(o => true, o =>
        {
            Navigation.NavigateTo<LoginViewModel>();
        });
        NavigateToGalleryViewCommand = new RelayCommand(o => true, o =>
        {
            Navigation.NavigateTo<GalleryViewModel>();
        });
        NavigateToReservationsViewCommand = new RelayCommand(o => true, o =>
        {
            Navigation.NavigateTo<ReservationsViewModel>();
        });
    }

}