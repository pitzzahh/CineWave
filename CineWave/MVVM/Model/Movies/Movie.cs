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
    public DateTime ReleaseDate { get; set; }
    
    public Movie(string? movieName, double moviePrice, bool nowShowing, DateTime releaseDate)
    {
        MovieName = movieName;
        MoviePrice = moviePrice;
        NowShowing = nowShowing;
        ReleaseDate = releaseDate;
    }

}