namespace CineWave.MVVM.Model;

public class Seat
{
    public int SeatNumber { get; }
    public bool IsTaken { get; set; }

    public Seat(int seatNumber)
    {
        SeatNumber = seatNumber;
        IsTaken = false;
    }
}