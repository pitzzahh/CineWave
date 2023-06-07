using System;
using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model;

public class Movie
{
    [Key]
    public int Id { get; set; }
    public string? MovieName { get; set; }
    public double Price { get; set; }
    public bool NowShowing { get; set; } = false;
    public DateOnly ReleaseDate { get; set; }

}