using System;
using CineWave.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.Services;

public class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, ViewModel?> _viewModelFactory;
    
    private ViewModel? _currenView;

    public NavigationService(Func<Type, ViewModel?> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public ViewModel? CurrentView {
        get => _currenView;
        private set
        {
            _currenView = value;
            OnPropertyChanged();
        }
    }
    
    public void NavigateTo<T>() where T : ViewModel
    {
        CurrentView = _viewModelFactory.Invoke(typeof(T)); 
    }
}