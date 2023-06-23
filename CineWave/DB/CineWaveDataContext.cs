using CineWave.MVVM.Model;
using CineWave.MVVM.Model.Movies;
using CineWave.MVVM.Model.Reservations;
using CineWave.MVVM.Model.SeatsBooking;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB;

public class CineWaveDataContext : DbContext
{
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    public DbSet<Seat> Seats { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=cinewave.db"); // SQLite connection string
    }
}