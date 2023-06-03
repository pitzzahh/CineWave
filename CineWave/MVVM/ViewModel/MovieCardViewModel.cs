using System.Windows.Controls;

namespace CineWave.MVVM.ViewModel;

public class MovieCardViewModel : Core.ViewModel
{
    public Image MovieImage { get; }
    public string? MovieName { get; }
    public string? MovieDescription { get; }

    public MovieCardViewModel(Image movieImage, string? movieName, string? movieDescription)
    {
        MovieImage = movieImage;
        MovieName = movieName;
        MovieDescription = movieDescription;
    }
}