using CineWave.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineWave.DB.Persistence.Repositories;

public class SeatsRepository : Repository<Seat>
{
    public SeatsRepository(DbContext context) : base(context)
    {
        new DatabaseFacade(context).EnsureCreatedAsync();
    }
}