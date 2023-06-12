using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using CineWave.Components;
using CineWave.DB.Core;
using CineWave.Helpers;
using CineWave.Messages.ManageMovies;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.ManageMovies;

public partial class EditMovieFormViewModel : BaseViewModel, IRecipient<GetMovieInfoMessage>
{
    [ObservableProperty] private string _movieName = null!;
    [ObservableProperty] private string _price = null!;
    [ObservableProperty] private int _releaseDateDay;
    private string? _releaseDateMonth;
    [ObservableProperty] private int _releaseDateYear = DateTime.Now.Year;
    [ObservableProperty] private int _screeningDateDay;
    private string? _screeningDateMonth;
    [ObservableProperty] private int _screeningDateYear = DateTime.Now.Year;

    [ObservableProperty] private int _runtimeHourTime = 1;
    [ObservableProperty] private int _runtimeMinuteTime = 1;

    [ObservableProperty] private int _screeningDateHourTime = DateTime.Now.Hour;
    [ObservableProperty] private int _screeningDateMinuteTime = DateTime.Now.Minute;

    [ObservableProperty] private ObservableCollection<int> _releaseDateDays = new();
    [ObservableProperty] private ObservableCollection<string> _releaseDateMonths = new();
    [ObservableProperty] private ObservableCollection<int> _releaseDateYears = new();
    [ObservableProperty] private ObservableCollection<int> _screeningDateDays = new();
    [ObservableProperty] private ObservableCollection<string> _screeningDateMonths = new();
    [ObservableProperty] private ObservableCollection<int> _screeningDateYears = new();

    [ObservableProperty] private ObservableCollection<int> _runtimeHourList = new();
    [ObservableProperty] private ObservableCollection<int> _runtimeMinuteList = new();

    [ObservableProperty] private ObservableCollection<int> _screeningDateHourList = new();
    [ObservableProperty] private ObservableCollection<int> _screeningDateMinuteList = new();

    private readonly IUnitOfWork _unitOfWork;

    public EditMovieFormViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        WeakReferenceMessenger.Default.Register(this);
        SetComboBoxItems(DateTime.Now);
    }

    [RelayCommand]
    public void OnUpdateMovie()
    {
        var movieByName = _unitOfWork.MoviesRepository.GetMovieByName(MovieName);
        if (movieByName == null) return;
        movieByName.MovieName = MovieName;
        movieByName.MoviePrice = double.Parse(Price ?? "0");
        
        var releaseDate = new DateOnly(ReleaseDateYear, StringHelper.GetMonthInt(ReleaseDateMonth), ReleaseDateDay);
        var screeningDateTime = new DateTime(
            ScreeningDateYear,
            StringHelper.GetMonthInt(ScreeningDateMonth),
            ScreeningDateDay,
            ScreeningDateHourTime,
            ScreeningDateMinuteTime,
            0
        );
        movieByName.ReleaseDate = releaseDate;
        movieByName.ScreeningDateTime = screeningDateTime;
        movieByName.Runtime = new TimeOnly(RuntimeHourTime, RuntimeMinuteTime);
        var result = _unitOfWork.Complete();
        if (result == 0)
        {
            MessageBox.Show("Movie was not updated");
            return;
        }
        MessageBox.Show("Movie updated successfully");
        OnCancel();
    }
    
    [RelayCommand]
    public void OnRemoveMovie()
    {
        var movieByName = _unitOfWork.MoviesRepository.GetMovieByName(MovieName);
        if (movieByName == null) return;
        _unitOfWork.MoviesRepository.Remove(movieByName);
        var result = _unitOfWork.Complete();
        if (result == 0)
        {
            MessageBox.Show("Movie was not removed");
            return;
        }
        MessageBox.Show("Movie removed successfully");
        OnCancel();
    }

    [RelayCommand]
    public void OnCancel()
    {
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        Task.Run(App.ServiceProvider.GetRequiredService<ManageMoviesViewModel>().CreateMovieInfoCards); // Run the method on a separate thread
        CloseRegistrationWindow(App.ServiceProvider.GetRequiredService<EditMovieForm>());
    }

    private static void CloseRegistrationWindow(Window seatBookingRegistrationForm)
    {
        if (!seatBookingRegistrationForm.IsVisible) return;
        seatBookingRegistrationForm.Hide();
    }
    
    public string? ReleaseDateMonth
    {
        get => _releaseDateMonth;
        set
        {
            if (SetProperty(ref _releaseDateMonth, value))
            {
                // Update the available days when the month changes
                UpdateReleaseDateDays();
            }
        }
    }

    public string? ScreeningDateMonth
    {
        get => _screeningDateMonth;
        set
        {
            if (SetProperty(ref _screeningDateMonth, value))
            {
                // Update the available days when the month changes
                UpdateScreeningDateDays();
            }
        }
    }
    
    private void UpdateReleaseDateDays()
    {
        // Get the number of days in the selected month and year
        var daysInMonth = DateTime.DaysInMonth(ReleaseDateYear, StringHelper.GetMonthInt(ReleaseDateMonth));

        // Update the available days
        ReleaseDateDays.Clear();
        for (var i = 1; i <= daysInMonth; i++)
        {
            ReleaseDateDays.Add(i);
        }

        ReleaseDateDay = 1;
    }

    private void UpdateScreeningDateDays()
    {
        // Get the number of days in the selected month and year
        var daysInMonth = DateTime.DaysInMonth(ScreeningDateYear, StringHelper.GetMonthInt(ScreeningDateMonth));

        // Update the available days
        ScreeningDateDays.Clear();
        for (var i = 1; i <= daysInMonth; i++)
        {
            ScreeningDateDays.Add(i);
        }

        ScreeningDateDay = 1;
    }

    private void SetComboBoxItems(DateTime currentDate)
    {
        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            // Populate the ComboBox items
            for (var i = 1; i <= 12; i++)
            {
                ReleaseDateMonths.Add(StringHelper.GetMonthString(i));
                ScreeningDateMonths.Add(StringHelper.GetMonthString(i));
            }

            for (var i = 1800; i <= currentDate.Year; i++)
            {
                ReleaseDateYears.Add(i);
                ScreeningDateYears.Add(i);
            }

            for (var i = 1; i <= 24; i++)
            {
                ScreeningDateHourList.Add(i);
            }

            for (var i = 0; i <= 59; i++)
            {
                ScreeningDateMinuteList.Add(i);
            }

            for (var i = 1; i <= 5; i++)
            {
                RuntimeHourList.Add(i);
            }

            for (var i = 0; i <= 59; i++)
            {
                RuntimeMinuteList.Add(i);
            }
        });
    }
    
    public void Receive(GetMovieInfoMessage message)
    {
        var editMovieInfo = message.Value;
        Debug.Print($"Received Movie: {editMovieInfo}");
        MovieName = editMovieInfo.MovieName;
        
        RuntimeHourTime = editMovieInfo.Runtime.Hour;
        RuntimeMinuteTime = editMovieInfo.Runtime.Minute;
        
        Price = editMovieInfo.MoviePrice.ToString(CultureInfo.CurrentCulture);

        ReleaseDateYear = editMovieInfo.ReleaseDate.Year;
        ReleaseDateMonth = StringHelper.GetMonthString(editMovieInfo.ReleaseDate.Month);
        ReleaseDateDay = editMovieInfo.ReleaseDate.Day;

        ScreeningDateYear = editMovieInfo.ScreeningDateTime.Year;
        ScreeningDateMonth = StringHelper.GetMonthString(editMovieInfo.ScreeningDateTime.Month);
        ScreeningDateDay = editMovieInfo.ScreeningDateTime.Day;

        ScreeningDateHourTime = editMovieInfo.ScreeningDateTime.Hour;
        ScreeningDateMinuteTime = editMovieInfo.ScreeningDateTime.Minute;
    }
}