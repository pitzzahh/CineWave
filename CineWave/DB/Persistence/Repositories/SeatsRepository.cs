using CineWave.MVVM.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineWave.DB.Persistence.Repositories;

public class SeatsRepository : Repository<Seat>
{
    public SeatsRepository(CineWaveDataContext context) : base(context)
    {
        new DatabaseFacade(context).EnsureCreatedAsync();
    }
}