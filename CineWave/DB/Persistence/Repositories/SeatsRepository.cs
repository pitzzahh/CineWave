using CineWave.MVVM.Model;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class SeatsRepository : Repository<Seat>
{
    public SeatsRepository(DbContext context) : base(context)
    {
    }
}