using System.Diagnostics;
using CineWave.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Login;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty] private bool _isLoginFormVisible = true;
    private string? _username;
    private string? _password;
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
    }

    private void UpdateCanLogin()
    {
        CanLogin = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
    }
}