using System;
using System.Diagnostics;
using System.Windows;
using CineWave.Components;
using CineWave.DB;
using CineWave.DB.Core;
using CineWave.DB.Core.Repositories;
using CineWave.DB.Persistence;
using CineWave.DB.Persistence.Repositories;
using CineWave.MVVM.View;
using CineWave.MVVM.View.Login;
using CineWave.MVVM.ViewModel;
using CineWave.MVVM.ViewModel.AddMovie;
using CineWave.MVVM.ViewModel.Gallery;
using CineWave.MVVM.ViewModel.Login;
using CineWave.MVVM.ViewModel.ManageMovies;
using CineWave.MVVM.ViewModel.Reservations;
using CineWave.MVVM.ViewModel.SeatsBooking;
using CineWave.MVVM.ViewModel.Trailer;
using CineWave.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            serviceCollection.AddSingleton<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddSingleton<MoviesDataContext>();

            serviceCollection.AddTransient<LoginViewModel>();

            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<HomeViewModel>();
            serviceCollection.AddSingleton<ReservationsViewModel>();
            serviceCollection.AddSingleton<SeatBookingViewModel>();
            serviceCollection.AddSingleton<SeatCardViewModel>();
            serviceCollection.AddSingleton<SeatBookingRegistrationForm>();
            serviceCollection.AddTransient<SeatBookingRegistrationFormBaseViewModel>();
            serviceCollection.AddSingleton<AddMovieViewModel>();
            serviceCollection.AddSingleton<TrailerViewModel>();
            serviceCollection.AddSingleton<ManageMoviesViewModel>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            
            serviceCollection.AddTransient<LoginWindow>(provider => new LoginWindow()
            {
                DataContext = provider.GetRequiredService<LoginViewModel>()
            });

            serviceCollection.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            
            serviceCollection.AddSingleton<SeatBookingRegistrationForm>(provider => new SeatBookingRegistrationForm
            {
                DataContext = provider.GetRequiredService<SeatBookingRegistrationFormBaseViewModel>()
            });
            
            serviceCollection.AddSingleton<Func<Type, BaseViewModel>>(provider => viewModelType => (BaseViewModel)provider.GetRequiredService(viewModelType));

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