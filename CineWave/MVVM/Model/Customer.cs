using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model;

public record Customer(int Id, int TicketId)
{
    [Key]
    public int Id { get; set; } = Id;
    public int TicketId { get; set; } = TicketId;
}
