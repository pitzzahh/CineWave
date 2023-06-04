namespace CineWave.MVVM.ViewModel;

public class SeatCardViewModel : Core.ViewModel
{
    public bool IsTaken { get; }
    public string SeatNumber { get; }

    public SeatCardViewModel(string seatNumber, bool isTaken)
    {
        IsTaken = isTaken;
        SeatNumber = seatNumber;
    }
}