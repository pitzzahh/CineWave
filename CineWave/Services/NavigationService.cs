using System;
using CineWave.MVVM.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.Services;

public class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, BaseViewModel?> _viewModelFactory;
    
    private BaseViewModel? _currentView;

    public NavigationService(Func<Type, BaseViewModel?> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public BaseViewModel? CurrentView {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    
    public void NavigateTo<T>() where T : BaseViewModel
    {
        CurrentView = _viewModelFactory.Invoke(typeof(T));
    }
}