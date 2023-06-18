using System;
using System.Collections.Generic;
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
        return GetAll().FirstOrDefault(movie =>
            movie.ScreeningDateTime.Day == currentTime.Day &&
            currentTime >= movie.ScreeningDateTime &&
            currentTime <= movie.ScreeningDateTime.Add(movie.Runtime.ToTimeSpan())
        );
    }

    public Movie? GetMovieByName(string movieName)
    {
        return Find(movie => movie.MovieName == movieName).FirstOrDefault();
    }

    public IEnumerable<Movie>? GetAvailableMoviesForReservation()
    {
        return GetAll().Where(movie => movie.ScreeningDateTime.Day > DateTime.Now.AddDays(1).Day);
    }
}