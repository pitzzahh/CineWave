using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CineWave.MVVM.Model.SeatsBooking;

namespace CineWave.MVVM.Model.Movies;

public class Ticket
{
    public Ticket(int movieId, int seatId)
    {
        MovieId = movieId;
        SeatId = seatId;
    }

    [Key] public int TicketId { get; set; }

    [ForeignKey("Movie")] public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!; // Navigation property
    
    [ForeignKey("Seats")] 
    public int SeatId { get; set; }
    public Seat Seat { get; set; } = null!;
}