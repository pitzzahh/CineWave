using System.Diagnostics;
using CineWave.MVVM.View;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Login;

public partial class LoginViewModel : BaseViewModel
{
    private string? _username;
    private string? _password;
    public bool IsLogInFormVisible { get; set; } = true;
    private bool _canLogin;

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

    public bool CanLogin
    {
        get => _canLogin;
        set => SetProperty(ref _canLogin, value);
    }

    [RelayCommand]
    public void Login()
    {
        Debug.Assert(Username != null, nameof(Username) + " != null");
        Debug.Assert(Password != null, nameof(Password) + " != null");
        var isAuthenticated = Username.Equals("pitzzahh") && Password.Equals("123456");
        if (!isAuthenticated) return;
        IsLogInFormVisible = false;
        Username = "";
        Password = "";
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().Show();
        App.ServiceProvider.GetRequiredService<MainViewModel>().NavigateToHome();
    }

    private void UpdateCanLogin()
    {
        CanLogin = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
    }
}