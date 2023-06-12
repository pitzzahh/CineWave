using CineWave.MVVM.Model;
using CineWave.MVVM.Model.SeatsBooking;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class SeatsRepository : Repository<Seat>
{
    public SeatsRepository(DbContext context) : base(context)
    {
    }
}