using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public class Movie
{
    public Movie(string name, TimeOnly runtime, double price, DateOnly releaseDate,
        DateTime screeningDateTime)
    {
        Name = name;
        Runtime = runtime;
        Price = price;
        ReleaseDate = releaseDate;
        ScreeningDateTime = screeningDateTime;
    }
    public Movie() {}

    [Key] public int MovieId { get; set; }

    public string? Name { get; set; }
    public TimeOnly Runtime { get; set; }
    public double Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public DateTime ScreeningDateTime { get; set; }
    
}