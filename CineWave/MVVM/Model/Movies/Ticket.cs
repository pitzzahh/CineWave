using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineWave.MVVM.Model.Movies;

public class Ticket
{
    public Ticket(int movieId, string seatNumber)
    {
        MovieId = movieId;
        SeatNumber = seatNumber;
    }

    [Key] public int TicketId { get; set; }

    [ForeignKey("Movie")] public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!; // Navigation property
    public string SeatNumber { get; set; }
}