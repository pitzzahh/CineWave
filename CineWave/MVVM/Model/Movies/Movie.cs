using System;
using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public class Movie
{
    
    [Key]
    public int MovieId { get; set; }
    public string? MovieName { get; set; }
    public bool NowShowing { get; set; }
    public DateOnly ReleaseDate { get; set; }
    
    public Movie(string? movieName, bool nowShowing, DateOnly releaseDate)
    {
        MovieName = movieName;
        NowShowing = nowShowing;
        ReleaseDate = releaseDate;
    }

}