namespace CineWave.MVVM.ViewModel.Gallery;

public class MovieCardBaseViewModel : BaseViewModel
{
    public string MovieImage { get; }
    public string? MovieName { get; }
    public string? MovieDescription { get; }

    public MovieCardBaseViewModel(string movieImage, string? movieName, string? movieDescription)
    {
        MovieImage = movieImage;
        MovieName = movieName;
        MovieDescription = movieDescription;
    }
}