﻿using System;
using System.Collections.ObjectModel;
using CineWave.DB.Core;
using CineWave.Helpers;
using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MessageBox = System.Windows.MessageBox;

namespace CineWave.MVVM.ViewModel.AddMovie;

public partial class AddMovieViewModel : BaseViewModel
{
    [ObservableProperty] private string? _movieName;
    [ObservableProperty] private string? _price;
    [ObservableProperty] private int _releaseDateDay;
    private string? _releaseDateMonth;
    [ObservableProperty] private int _releaseDateYear = DateTime.Now.Year;
    [ObservableProperty] private int _screeningDateDay;
    private string? _screeningDateMonth;
    [ObservableProperty] private int _screeningDateYear = DateTime.Now.Year;
    [ObservableProperty] private ObservableCollection<int> _releaseDateDays = new();

    [ObservableProperty] private ObservableCollection<string> _releaseDateMonths = new();
    [ObservableProperty] private ObservableCollection<int> _releaseDateYears = new();
    [ObservableProperty] private ObservableCollection<int> _screeningDateDays = new();
    [ObservableProperty] private ObservableCollection<string> _screeningDateMonths = new();
    [ObservableProperty] private ObservableCollection<int> _screeningDateYears = new();

    private readonly IUnitOfWork _unitOfWork;

    public AddMovieViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        // Initialize the default values for the date properties
        var currentDate = DateTime.Now;
        ReleaseDateYear = currentDate.Year;
        ReleaseDateMonth = StringHelper.GetMonthString(currentDate.Month);
        ReleaseDateDay = currentDate.Day;
        ScreeningDateYear = currentDate.Year;
        ScreeningDateMonth = StringHelper.GetMonthString(currentDate.Month);
        ScreeningDateDay = currentDate.Day;
        // Populate the ComboBox items
        for (var i = 1; i <= 31; i++)
        {
            ReleaseDateDays.Add(i);
            ScreeningDateDays.Add(i);
        }

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

    [RelayCommand]
    public void AddMovie()
    {
        if (MovieName is null || Price is null || !StringHelper.IsWholeNumberOrDecimal(Price))
            return;
        var releaseDate = new DateOnly(ReleaseDateYear, StringHelper.GetMonthInt(ReleaseDateMonth), ReleaseDateDay);
        var screeningDate = new DateOnly(ScreeningDateYear, StringHelper.GetMonthInt(ScreeningDateMonth), ScreeningDateDay);
        _unitOfWork.MoviesRepository.Add(new Movie(MovieName, Convert.ToDouble(Price), false, releaseDate,
            screeningDate));
        var complete = _unitOfWork.Complete();

        if (complete == 1)
        {
            MessageBox.Show($"Movie {MovieName} Added Successfully");
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
}