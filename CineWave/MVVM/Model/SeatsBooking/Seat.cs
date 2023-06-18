using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CineWave.MVVM.Model.Movies;

namespace CineWave.MVVM.Model.SeatsBooking;

public class Seat
{
    public Seat(string seatNumber, bool isTaken)
    {
        SeatNumber = seatNumber;
        IsTaken = isTaken;
    }

    [Key] public int SeatId { get; set; }
    public string SeatNumber { get; set; }
    public bool IsTaken { get; set; }
    [ForeignKey("Movie")] public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!; // Navigation property
}