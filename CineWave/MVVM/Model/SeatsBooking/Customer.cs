using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public int TicketId { get; set; }
    
    public Customer(int id, int ticketId)
    {
        Id = id;
        TicketId = ticketId;
    }
}
