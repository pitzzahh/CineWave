﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatBookingViewModel : BaseViewModel
{
    [ObservableProperty] private string? _currentMovie;
    [ObservableProperty] private string? _seatNumber;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ObservableCollection<SeatCardViewModel> _seats = new(); // For seats choose
    public IEnumerable<SeatCardViewModel> Seats => _seats;
    private const string MovieNotFound = "No movie is currently showing";

    public SeatBookingViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SetCurrentMovie()
    {
        await Application.Current
            .Dispatcher
            .InvokeAsync(() =>
            {
                CurrentMovie = _unitOfWork.MoviesRepository.GetNowShowingMovie()?.MovieName ?? MovieNotFound; 
            });
    }

    public async Task CreateSeats()
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            var enumerable = _unitOfWork.SeatsRepository.GetAll();
            if (_seats.Count.Equals(50)) return;
            foreach (var seat in enumerable)
            {
                _seats.Add(new SeatCardViewModel(seat.SeatNumber, CurrentMovie == MovieNotFound || seat.IsTaken, _unitOfWork));
            }
            _unitOfWork.Complete();
        });
    }
}