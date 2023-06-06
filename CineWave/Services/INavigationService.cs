using CineWave.MVVM.ViewModel;

namespace CineWave.Services;

public interface INavigationService
{
    BaseViewModel? CurrentView { get; }
    void NavigateTo<T>() where T : BaseViewModel;
}