using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineWave.DB.Persistence.Repositories;

public class TicketsRepository : Repository<Ticket>
{
    public TicketsRepository(DbContext context) : base(context)
    {
        new DatabaseFacade(context).EnsureCreatedAsync();
    }
}