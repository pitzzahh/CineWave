namespace CineWave.MVVM.ViewModel;

public class MovieButtonViewModel : Core.ViewModel
{
    public string CinemaNumber { get; }
    public string MovieName { get; }

    public MovieButtonViewModel(string cinemaNumber,string movieName)
    {
        CinemaNumber = cinemaNumber;
        MovieName = movieName;
    }
}