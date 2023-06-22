using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineWave.MVVM.Model.Movies;

public class Movie
{
    public Movie(string movieName, TimeOnly runtime, double moviePrice, DateOnly releaseDate,
        DateTime screeningDateTime)
    {
        MovieName = movieName;
        Runtime = runtime;
        MoviePrice = moviePrice;
        ReleaseDate = releaseDate;
        ScreeningDateTime = screeningDateTime;
    }
    public Movie() {}

    [Key] public int MovieId { get; set; }

    public string? MovieName { get; set; }
    public TimeOnly Runtime { get; set; }
    public double MoviePrice { get; set; }
    [ForeignKey("Genre")] public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!; // Navigation property
    public DateOnly ReleaseDate { get; set; }
    public DateTime ScreeningDateTime { get; set; }
}