using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineWave.MVVM.Model.Reservations
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!; // Navigation property
        public DateOnly DateOfReservation { get; set; }
        
        public Reservation(int customerId, DateOnly dateOfReservation)
        {
            CustomerId = customerId;
            DateOfReservation = dateOfReservation;
        }
    }
}