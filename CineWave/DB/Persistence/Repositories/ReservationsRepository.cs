using CineWave.MVVM.Model.Reservations;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class ReservationsRepository : Repository<Reservation>
{
    public ReservationsRepository(DbContext context) : base(context) { }
}