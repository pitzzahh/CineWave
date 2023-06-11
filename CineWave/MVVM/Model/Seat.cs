using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model;

public class Seat
{
    [Key]
    public string SeatNumber { get; set; }
    public bool IsTaken { get; set; }

    public Seat(string seatNumber, bool isTaken)
    {
        SeatNumber = seatNumber;
        IsTaken = isTaken;
    }
}