using System;
using System.Diagnostics;
using System.Windows;
using CineWave.Core;
using CineWave.MVVM.View;
using CineWave.MVVM.ViewModel;
using CineWave.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static ServiceProvider? ServiceProvider;

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
            serviceCollection.AddSingleton<SeatBookingViewModel>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();

            serviceCollection.AddSingleton<Func<Type, ViewModel>>(provider =>
                viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Debug.Assert(ServiceProvider != null, nameof(ServiceProvider) + " != null");
            ServiceProvider.GetRequiredService<LoginWindow>().Show();
            base.OnStartup(e);
        }
    }
}