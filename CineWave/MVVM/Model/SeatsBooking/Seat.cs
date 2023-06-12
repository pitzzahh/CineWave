using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.SeatsBooking;

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