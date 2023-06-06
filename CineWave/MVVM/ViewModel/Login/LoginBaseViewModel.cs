using System.Diagnostics;
using CineWave.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.Login;

public partial class LoginBaseViewModel : BaseViewModel
{
    [ObservableProperty] private string? _username;
    [ObservableProperty] private string? _password;
    [ObservableProperty] private bool _isLogInFormVisible = true;
    [ObservableProperty] private bool _canLogin;

    public LoginBaseViewModel()
    {
        CanLogin = CheckInputs();
    }

    [RelayCommand]
    public void Login()
    {
        if (CheckInputs()) return;
        Debug.Assert(Username != null, nameof(Username) + " != null");
        Debug.Assert(Password != null, nameof(Password) + " != null");
        var isAuthenticated = Username.Equals("pitzzahh") && Password.Equals("123456");
        if (!isAuthenticated) return;
        IsLogInFormVisible = false;
        Username = "";
        Password = "";
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        App.ServiceProvider.GetRequiredService<MainWindow>().Show();
        App.ServiceProvider.GetRequiredService<MainBaseViewModel>().NavigateToHome(); 
    }

    private bool CheckInputs()
    {
        return string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password);
    }
}