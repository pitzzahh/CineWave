using System;

namespace CineWave.MVVM.Model;

public record Movie
{
    private string? MovieName { get; set; }
    private double Price { get; set; }
    private bool NowShowing { get; set; }
    private DateTime ReleaseDate { get; set; }

    public Movie(string? movieName, double price, bool nowShowing, DateTime releaseDate)
    {
        MovieName = movieName;
        Price = price;
        NowShowing = nowShowing;
        ReleaseDate = releaseDate;
    }
}