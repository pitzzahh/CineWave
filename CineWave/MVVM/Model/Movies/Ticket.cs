using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public record Ticket(int TicketId, string? MovieId, double Price, int CustomerId)
{
    [Key]
    public int TicketId { get; set; } = TicketId;
    public string? MovieId { get; set; } = MovieId;
    public double Price { get; set; } = Price;
    public int CustomerId { get; set; } = CustomerId;
}