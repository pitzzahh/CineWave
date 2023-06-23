using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CineWave.MVVM.Model.Movies;

namespace CineWave.MVVM.Model;

public class Customer
{
    public Customer(int ticketId)
    {
        TicketId = ticketId;
    }

    [Key] public int CustomerId { get; set; }

    public string? Name { get; set; }
    public double Payment { get; set; }

    [ForeignKey("Ticket")] public int TicketId { get; set; }

    public Ticket Ticket { get; set; } = null!; // Navigation property
}