using System;
using System.Diagnostics;
using System.Windows;
using CineWave.Components;
using CineWave.DB;
using CineWave.DB.Core;
using CineWave.DB.Persistence;
using CineWave.MVVM.View;
using CineWave.MVVM.View.Login;
using CineWave.MVVM.ViewModel;
using CineWave.MVVM.ViewModel.AddMovie;
using CineWave.MVVM.ViewModel.Gallery;
using CineWave.MVVM.ViewModel.Login;
using CineWave.MVVM.ViewModel.ManageMovies;
using CineWave.MVVM.ViewModel.Reservations;
using CineWave.MVVM.ViewModel.Reservations.MovieList;
using CineWave.MVVM.ViewModel.Reservations.SeatBooking;
using CineWave.MVVM.ViewModel.SeatsBooking;
using CineWave.MVVM.ViewModel.Trailer;
using CineWave.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public static ServiceProvider? ServiceProvider;

    public App()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddSingleton<CineWaveDataContext>();

        serviceCollection.AddTransient<LoginViewModel>();

        serviceCollection.AddSingleton<MainViewModel>();
        serviceCollection.AddSingleton<HomeViewModel>();
        serviceCollection.AddSingleton<ReservationsViewModel>();
        serviceCollection.AddSingleton<ReservationsViewModel>();
        serviceCollection.AddSingleton<SeatBookingViewModel>();
        serviceCollection.AddSingleton<ManageMoviesViewModel>();
        serviceCollection.AddSingleton<RMovieInfoCardViewModel>();
        serviceCollection.AddSingleton<MovieInfoCardViewModel>();
        serviceCollection.AddSingleton<ReservationCardViewModel>();
        serviceCollection.AddSingleton<SbSeatCardViewModel>();
        serviceCollection.AddSingleton<RSeatCardViewModel>();
        serviceCollection.AddSingleton<SeatBookingRegistrationForm>();
        serviceCollection.AddTransient<SeatBookingRegistrationFormViewModel>();
        serviceCollection.AddTransient<RSeatBookingRegistrationFormViewModel>();
        serviceCollection.AddTransient<EditMovieFormViewModel>();
        serviceCollection.AddTransient<AddMovieViewModel>();
        serviceCollection.AddSingleton<TrailerViewModel>();
        serviceCollection.AddSingleton<ManageMoviesViewModel>();
        serviceCollection.AddSingleton<INavigationService, NavigationService>();

        serviceCollection.AddTransient<LoginWindow>(provider => new LoginWindow
        {
            DataContext = provider.GetRequiredService<LoginViewModel>()
        });

        serviceCollection.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        serviceCollection.AddSingleton<SeatBookingRegistrationForm>(provider => new SeatBookingRegistrationForm
        {
            DataContext = provider.GetRequiredService<SeatBookingRegistrationFormViewModel>()
        });

        serviceCollection.AddSingleton<EditMovieForm>(provider => new EditMovieForm
        {
            DataContext = provider.GetRequiredService<EditMovieFormViewModel>()
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