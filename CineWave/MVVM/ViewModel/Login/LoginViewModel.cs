using System.Diagnostics;
using CineWave.Core;
using CineWave.MVVM.View;
using CineWave.MVVM.ViewModel.Gallery;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Login;

public class LoginViewModel : Core.ViewModel
{
    private string? _username;
    private string? _password;
    private bool _isLogInFormVisible = true;

    public string? Username
    {
        get => _username;
        set
        {
            if (value == _username) return;
            _username = value;
            OnPropertyChanged();
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            if (value == _password) return;
            _password = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoginFormVisible
    {
        get => _isLogInFormVisible;
        set
        {
            if (value == _isLogInFormVisible) return;
            _isLogInFormVisible = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand LoginCommand { get; set; }

    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(LoginProcess, ValidateInput);
    }

    private void LoginProcess(object obj)
    {
        Debug.Assert(Username != null, nameof(Username) + " != null");
        Debug.Assert(Password != null, nameof(Password) + " != null");
        var isAuthenticated = Username.Equals("pitzzahh") && Password.Equals("123456");
        if (!isAuthenticated) return;
        IsLoginFormVisible = false;
        Username = "";
        Password = "";
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().Show();
        App.ServiceProvider.GetRequiredService<MainViewModel>().Navigation.NavigateTo<HomeViewModel>();

    }
    
    private bool ValidateInput(object obj)
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }
}