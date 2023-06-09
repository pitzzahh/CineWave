﻿using System;
using CineWave.DB.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.ManageMovies;

public partial class ManageMoviesViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _movieName;
    
    [ObservableProperty]
    private double _moviePrice;
        
    [ObservableProperty]
    private DateTime _releaseDate;
    
    [ObservableProperty]
    private bool _nowShowing;

    private readonly IUnitOfWork _unitOfWork;

    public ManageMoviesViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
}