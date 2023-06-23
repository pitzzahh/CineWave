using System.Linq;
using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class GenresRepository : Repository<Genre>
{
    public GenresRepository(DbContext context) : base(context)
    {

    }
}