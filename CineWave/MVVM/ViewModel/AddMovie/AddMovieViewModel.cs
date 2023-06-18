using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using CineWave.DB.Core;
using CineWave.Helpers;
using CineWave.MVVM.Model.Movies;
using CineWave.MVVM.Model.SeatsBooking;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CineWave.MVVM.ViewModel.AddMovie;

public partial class AddMovieViewModel : BaseViewModel
{
    private readonly IUnitOfWork _unitOfWork;
    [ObservableProperty] private string? _movieName;
    [ObservableProperty] private string? _price;
    [ObservableProperty] private int _releaseDateDay;

    [ObservableProperty] private ObservableCollection<int> _releaseDateDays = new();
    private string? _releaseDateMonth;
    [ObservableProperty] private ObservableCollection<string> _releaseDateMonths = new();
    [ObservableProperty] private int _releaseDateYear = DateTime.Now.Year;
    [ObservableProperty] private ObservableCollection<int> _releaseDateYears = new();

    [ObservableProperty] private ObservableCollection<int> _runtimeHourList = new();

    [ObservableProperty] private int _runtimeHourTime = 1;
    [ObservableProperty] private ObservableCollection<int> _runtimeMinuteList = new();
    [ObservableProperty] private int _runtimeMinuteTime = 1;
    [ObservableProperty] private int _screeningDateDay;
    [ObservableProperty] private ObservableCollection<int> _screeningDateDays = new();

    [ObservableProperty] private ObservableCollection<int> _screeningDateHourList = new();

    [ObservableProperty] private int _screeningDateHourTime = DateTime.Now.Hour;
    [ObservableProperty] private ObservableCollection<int> _screeningDateMinuteList = new();
    [ObservableProperty] private int _screeningDateMinuteTime = DateTime.Now.Minute;
    private string? _screeningDateMonth;
    [ObservableProperty] private ObservableCollection<string> _screeningDateMonths = new();
    [ObservableProperty] private int _screeningDateYear = DateTime.Now.Year;
    [ObservableProperty] private ObservableCollection<int> _screeningDateYears = new();

    public AddMovieViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        SetComboBoxItems();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public string? ReleaseDateMonth
    {
        get => _releaseDateMonth;
        set
        {
            if (SetProperty(ref _releaseDateMonth, value))
                // Update the available days when the month changes
                UpdateReleaseDateDays();
        }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public string? ScreeningDateMonth
    {
        get => _screeningDateMonth;
        set
        {
            if (SetProperty(ref _screeningDateMonth, value))
                // Update the available days when the month changes
                UpdateScreeningDateDays();
        }
    }

    private void SetComboBoxItems()
    {
        var currentDate = DateTime.Now;
        SetReleaseAndScreeningDates(currentDate);
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

            for (var i = 1; i <= 24; i++) ScreeningDateHourList.Add(i);

            for (var i = 0; i <= 59; i++) ScreeningDateMinuteList.Add(i);

            for (var i = 1; i <= 5; i++) RuntimeHourList.Add(i);

            for (var i = 0; i <= 59; i++) RuntimeMinuteList.Add(i);
        });
    }

    private void SetReleaseAndScreeningDates(DateTime currentDate)
    {
        ReleaseDateMonth = StringHelper.GetMonthString(currentDate.Month);
        ReleaseDateDay = currentDate.Day;
        ScreeningDateMonth = StringHelper.GetMonthString(currentDate.Month);
        ScreeningDateDay = currentDate.Day;
    }

    [RelayCommand]
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public void AddMovie()
    {
        if (MovieName is null)
        {
            MessageBox.Show("Please enter a movie name");
            return;
        }

        if (Price is null)
        {
            MessageBox.Show("Please enter a price");
            return;
        }

        var isInvalidPayment = !StringHelper.IsWholeNumberOrDecimal(Price);

        if (isInvalidPayment)
        {
            MessageBox.Show("Please enter a valid price");
            return;
        }

        var releaseDate = new DateOnly(ReleaseDateYear, StringHelper.GetMonthInt(ReleaseDateMonth), ReleaseDateDay);
        var screeningDateTime = new DateTime(
            ScreeningDateYear,
            StringHelper.GetMonthInt(ScreeningDateMonth),
            ScreeningDateDay,
            ScreeningDateHourTime,
            ScreeningDateMinuteTime,
            0
        );
        _unitOfWork.MoviesRepository.Add(new Movie(
                MovieName,
                new TimeOnly(RuntimeHourTime, RuntimeMinuteTime),
                Convert.ToDouble(Price),
                releaseDate,
                screeningDateTime
            )
        );
        var result = _unitOfWork.Complete();
        if (result == 0)
        {
            MessageBox.Show("Cannot add movie, please try again later");
            return;
        }

        var firstOrDefault = _unitOfWork.MoviesRepository.Find(m => m.MovieName == MovieName).FirstOrDefault();
        for (var row = 'A'; row <= 'E'; row++)
        for (var column = 1; column <= 10; column++)
        {
            var seatNumber = $"{row}{column}";
            var seat = new Seat(seatNumber, false);
            Debug.Assert(firstOrDefault != null, nameof(firstOrDefault) + " != null");
            seat.MovieId = firstOrDefault.MovieId;
            _unitOfWork.SeatsRepository.Add(seat);
        }

        var seatsAddingResult = _unitOfWork.Complete();
        if (seatsAddingResult == 0) return;
        MovieName = string.Empty;
        RuntimeHourTime = 1;
        RuntimeMinuteTime = 1;
        Price = string.Empty;
        SetReleaseAndScreeningDates(DateTime.Now);
        UpdateReleaseDateDays();
        UpdateScreeningDateDays();
        MessageBox.Show($"Movie {MovieName} Added Successfully");
    }

    private void UpdateReleaseDateDays()
    {
        // Get the number of days in the selected month and year
        var daysInMonth = DateTime.DaysInMonth(ReleaseDateYear, StringHelper.GetMonthInt(ReleaseDateMonth));

        // Update the available days
        ReleaseDateDays.Clear();
        for (var i = 1; i <= daysInMonth; i++) ReleaseDateDays.Add(i);

        ReleaseDateDay = 1;
    }

    private void UpdateScreeningDateDays()
    {
        // Get the number of days in the selected month and year
        var daysInMonth = DateTime.DaysInMonth(ScreeningDateYear, StringHelper.GetMonthInt(ScreeningDateMonth));

        // Update the available days
        ScreeningDateDays.Clear();
        for (var i = 1; i <= daysInMonth; i++) ScreeningDateDays.Add(i);

        ScreeningDateDay = 1;
    }
}