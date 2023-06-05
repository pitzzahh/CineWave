using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CineWave.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace CineWave.MVVM.ViewModel.Login;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty] private string? _username;
    [ObservableProperty] private string? _password;
    [ObservableProperty] private bool _isLogInFormVisible = true;
    [ObservableProperty] private bool _canLogin;

    public LoginViewModel()
    {
        CanLogin = CheckInputs();
    }

    [RelayCommand]
    public async Task Login()
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
        try
        {
            App.ServiceProvider.GetRequiredService<MainWindow>().Show();
            await App.ServiceProvider.GetRequiredService<MainViewModel>().NavigateToHome();
        }
        catch (Exception e)
        {
            Debug.Print(e.StackTrace);
        }
    }

    private bool CheckInputs()
    {
        return string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password);
    }
}