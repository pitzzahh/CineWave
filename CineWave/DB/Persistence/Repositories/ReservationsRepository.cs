using System;
using System.Collections.Generic;
using System.Linq;
using CineWave.MVVM.Model.Reservations;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class ReservationsRepository : Repository<Reservation>
{
    public ReservationsRepository(DbContext context) : base(context)
    {
    }

    public IEnumerable<Reservation> GetReservationsDuringOrBeforeScreenTime()
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        return GetAll().Where(r => r.DateOfReservation <= currentDate).ToList();
    }
}