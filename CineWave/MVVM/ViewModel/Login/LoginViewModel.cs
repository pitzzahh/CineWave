using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CineWave.MVVM.View;
using CineWave.MVVM.ViewModel.Gallery;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Login;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty] private bool _isLoginFormVisible = true;
    [Required] private string? _username;
    [Required] private string? _password;
    [ObservableProperty] private bool _canLogin;

    public string? Username
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value);
            UpdateCanLogin();
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            UpdateCanLogin();
        }
    }

    [RelayCommand]
    public void Login()
    {
        var isAuthenticated = Password != null && Username is "pitzzahh" && Password is "123456";
        if (!isAuthenticated) return;
        IsLoginFormVisible = false;
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().Show();
        App.ServiceProvider.GetRequiredService<MainViewModel>().NavService.NavigateTo<HomeViewModel>();
    }

    private void UpdateCanLogin()
    {
        CanLogin = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
    }
}