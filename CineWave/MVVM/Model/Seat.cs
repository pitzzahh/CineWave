namespace CineWave.MVVM.Model;

public class Seat
{
    public string SeatNumber { get; }
    public int CustomerId { get; set; }
    public int MovieId { get; set; }
    public bool IsTaken { get; set; } = false;

    public Seat(string seatNumber, int customerId, int movieId)
    {
        SeatNumber = seatNumber;
        CustomerId = customerId;
        MovieId = movieId;
    }
}