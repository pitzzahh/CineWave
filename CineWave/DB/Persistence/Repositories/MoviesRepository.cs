using System;
using System.Linq;
using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class MoviesRepository : Repository<Movie>
{
    public MoviesRepository(DbContext context) : base(context)
    {
    }

    public Movie? GetNowShowingMovie()
    {
        var currentTime = DateTime.Now;
        return GetAll().FirstOrDefault(m => m.ScreeningDateTime.Day == currentTime.Day && (currentTime <= m.ScreeningDateTime && currentTime.AddHours(m.Runtime.Hour) >= currentTime));
    }

}