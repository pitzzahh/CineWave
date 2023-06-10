using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineWave.DB.Persistence.Repositories;

public class TicketsRepository : Repository<Ticket>
{
    public TicketsRepository(CineWaveDataContext context) : base(context)
    {
        new DatabaseFacade(context).EnsureCreatedAsync();
    }
}