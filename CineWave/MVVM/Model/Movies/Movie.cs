using System;
using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public class Movie
{
    
    [Key]
    public int MovieId { get; set; }
    public string? MovieName { get; set; }
    public double MoviePrice { get; set; }
    public bool NowShowing { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public DateOnly ScreeningDate { get; set; }
    
    public Movie(string? movieName, double moviePrice, bool nowShowing, DateOnly releaseDate, DateOnly screeningDate)
    {
        MovieName = movieName;
        MoviePrice = moviePrice;
        NowShowing = nowShowing;
        ReleaseDate = releaseDate;
        ScreeningDate = releaseDate;
    }

}