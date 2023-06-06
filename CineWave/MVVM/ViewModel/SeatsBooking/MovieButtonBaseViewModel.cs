namespace CineWave.MVVM.ViewModel.SeatsBooking;

public class MovieButtonBaseViewModel : BaseViewModel
{
    public string CinemaNumber { get; }
    public string MovieName { get; }
    
    public MovieButtonBaseViewModel(string cinemaNumber,string movieName)
    {
        CinemaNumber = cinemaNumber;
        MovieName = movieName;
    }
}