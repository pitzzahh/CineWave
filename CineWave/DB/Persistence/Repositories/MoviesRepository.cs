using System;
using System.Linq;
using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineWave.DB.Persistence.Repositories;

public class MoviesRepository : Repository<Movie>
{
    public MoviesRepository(DbContext context) : base(context)
    {
        new DatabaseFacade(context).EnsureCreatedAsync();
    }
    
    public Movie? GetNowShowingMovie()
    {
        var currentTime = DateTime.Now;
        return GetAll().FirstOrDefault(m => m.ScreeningDateTime >= currentTime && m.ScreeningDateTime <= new DateTime(m.Runtime.Millisecond));
    }

}