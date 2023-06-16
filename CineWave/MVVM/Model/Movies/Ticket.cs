using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public class Ticket
{
    
    [Key]
    public int TicketId { get; set; }
    public int MovieId { get; set; }
    public string SeatNumber { get; set; }
    
    public Ticket(int movieId, string seatNumber)
    {
        MovieId = movieId;
        SeatNumber = seatNumber;
    }

}