using CineWave.MVVM.Model;
using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB;

public class MoviesDataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=cinewave_movies.db");
    }

    public DbSet<Movie> Movies { get; set; } = null!;
}