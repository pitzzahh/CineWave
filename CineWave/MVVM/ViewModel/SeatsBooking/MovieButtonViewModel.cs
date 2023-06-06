namespace CineWave.MVVM.ViewModel.SeatsBooking;

public class MovieButtonViewModel : BaseViewModel
{
    public string CinemaNumber { get; }
    public string MovieName { get; }
    
    public MovieButtonViewModel(string cinemaNumber,string movieName)
    {
        CinemaNumber = cinemaNumber;
        MovieName = movieName;
    }
}