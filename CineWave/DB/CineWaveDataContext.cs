using CineWave.MVVM.Model;
using CineWave.MVVM.Model.Movies;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB;

public class CineWaveDataContext : DbContext
{
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=cinewave.db"); // SQLite connection string
    }
}