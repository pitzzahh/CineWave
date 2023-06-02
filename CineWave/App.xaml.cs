using System;
using System.Windows;
using CineWave.Core;
using CineWave.MVVM.View;
using CineWave.MVVM.ViewModel;
using CineWave.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;

namespace CineWave
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton<LoginWindow>(provider => new LoginWindow()
            {
                DataContext = provider.GetRequiredService<LoginViewModel>()
            });
            
            serviceCollection.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            
            serviceCollection.AddSingleton<LoginViewModel>();
            
            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<HomeViewModel>();
            serviceCollection.AddSingleton<ReservationsViewModel>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            
            serviceCollection.AddSingleton<Func<Type, ViewModel>>(provider => viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var window = _serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
            base.OnStartup(e);
        }
    }
}