using System;
using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public record Movie(string? MovieName, bool NowShowing, DateOnly ReleaseDate)
{
    [Key]
    public int MovieId { get; set; }
    public string? MovieName { get; set; } = MovieName;
    public bool NowShowing { get; set; } = NowShowing;
    public DateOnly ReleaseDate { get; set; } = ReleaseDate;
}