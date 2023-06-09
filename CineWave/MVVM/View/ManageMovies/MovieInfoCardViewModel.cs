using System;
using CineWave.MVVM.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.View.ManageMovies;

public partial class MovieInfoCardViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _movieName;
    
    [ObservableProperty]
    private double _moviePrice;
        
    [ObservableProperty]
    private DateTime _releaseDate;
    
    [ObservableProperty]
    private bool _nowShowing;
}