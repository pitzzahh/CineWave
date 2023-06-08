using System;
using CineWave.DB;
using CineWave.DB.Core;
using CineWave.Helpers;
using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CineWave.MVVM.ViewModel.AddMovie;

public partial class AddMovieViewModel : BaseViewModel
{
    [ObservableProperty] private string? _movieName;
    [ObservableProperty] private string? _price;
    [ObservableProperty] private DateOnly _releaseDate;
    private readonly IUnitOfWork _unitOfWork;

    public AddMovieViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [RelayCommand]
    public void AddMovie()
    {
        if (MovieName is null || Price is null || !StringHelper.IsWholeNumberOrDecimal(Price)) return;
        _unitOfWork.MoviesRepository.Add(new Movie(MovieName, false, ReleaseDate));
        var complete = _unitOfWork.Complete();
        if (complete == 1)
        {
            
        }
    }
}