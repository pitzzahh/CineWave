
using System.ComponentModel.DataAnnotations;

namespace CineWave.MVVM.Model.Movies;

public class Genre
{
    [Key]
    private int GenreId { get; set; }
    private string? GenreName { get; set; }
}