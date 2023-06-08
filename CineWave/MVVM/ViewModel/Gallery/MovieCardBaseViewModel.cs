using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.Gallery;

public partial class MovieCardBaseViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _movieImage;
    [ObservableProperty]
    private string? _movieName;
    [ObservableProperty]
    private string? _movieDescription;

    public MovieCardBaseViewModel(string movieImage, string? movieName, string? movieDescription)
    {
        MovieImage = movieImage;
        MovieName = movieName;
        MovieDescription = movieDescription;
    }
}