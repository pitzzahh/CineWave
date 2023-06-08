using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public class Ticket
{
    
    [Key]
    public int TicketId { get; set; }
    public int MovieId { get; set; }
    public double Price { get; set; }
    
    public Ticket(int movieId, double price)
    {
        MovieId = movieId;
        Price = price;
    }

}