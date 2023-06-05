using System.Diagnostics;
using CineWave.MVVM.View;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace CineWave.MVVM.ViewModel.Login;

public partial class LoginViewModel : ObservableObject
{
    
    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _password;
    [ObservableProperty]
    private bool _isLogInFormVisible = true;

    private void LoginProcess(object obj)
    {
        if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
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


    }
   
}