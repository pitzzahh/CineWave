using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CineWave.MVVM.Model.Movies;

namespace CineWave.MVVM.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!; // Navigation property
        
        public Customer(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}