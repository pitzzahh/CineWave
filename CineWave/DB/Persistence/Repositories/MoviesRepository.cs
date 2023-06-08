using System;
using System.Collections.Generic;
using System.Linq;
using CineWave.DB.Core.Repositories;
using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineWave.DB.Persistence.Repositories;

public class MoviesRepository : Repository<Movie>
{
    public MoviesRepository(MoviesDataContext context) : base(context)
    {
        new DatabaseFacade(context).EnsureCreatedAsync();
    }
}