using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class TicketsRepository : Repository<Ticket>
{
    public TicketsRepository(DbContext context) : base(context)
    {
    }
}