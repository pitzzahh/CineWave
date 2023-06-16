using System;
using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Reservations;

public class Reservation
{
    [Key]
    private int ReservationId { get; set; }
    private int CustomerId { get; set; }
    private DateOnly DateOfReservation { get; set; }

    public Reservation(int customerId, DateOnly dateOfReservation)
    {
        CustomerId = customerId;
        DateOfReservation = dateOfReservation;
    }
}