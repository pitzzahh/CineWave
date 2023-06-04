namespace CineWave.MVVM.Model;

public class Seat
{
    public string SeatNumber { get; }
    public int CustomerId { get; set; }
    public bool IsTaken { get; set; }

    public Seat(string seatNumber)
    {
        SeatNumber = seatNumber;
        IsTaken = false;
    }
}