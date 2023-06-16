using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public int CustomerName { get; set; }
    public int TicketId { get; set; }
    
    public Customer(int ticketId)
    {
        TicketId = ticketId;
    }
}
