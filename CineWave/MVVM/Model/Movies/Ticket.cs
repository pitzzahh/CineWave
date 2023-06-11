using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public class Ticket
{
    
    [Key]
    public int TicketId { get; set; }
    public int MovieId { get; set; }
    public string SeatNumber { get; set; }
    public double Price { get; set; }
    
    public Ticket(int movieId, double price, string seatNumber)
    {
        MovieId = movieId;
        Price = price;
        SeatNumber = seatNumber;
    }

}