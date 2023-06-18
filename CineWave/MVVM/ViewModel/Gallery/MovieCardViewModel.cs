using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.Gallery;

public partial class MovieCardViewModel : BaseViewModel
{
    [ObservableProperty] private string? _movieDescription;

    [ObservableProperty] private string _movieImage;

    [ObservableProperty] private string? _movieName;

    public MovieCardViewModel(string movieImage, string? movieName, string? movieDescription)
    {
        MovieImage = movieImage;
        MovieName = movieName;
        MovieDescription = movieDescription;
    }
}