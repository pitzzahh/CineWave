using System;
using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public class Ticket
{
    [Key]
    public int Id { get; init; }
    public string? MovieName { get; init; }
    public double Price { get; init; }
    public DateOnly ReleaseDate { get; init; }
}