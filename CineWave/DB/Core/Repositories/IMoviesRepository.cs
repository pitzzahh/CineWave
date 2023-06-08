using System.Collections.Generic;
using CineWave.MVVM.Model.Movies;

namespace CineWave.DB.Core.Repositories;

public interface IMoviesRepository : IRepository<Movie>
{
    IEnumerable<Movie>? GetMovies();
}