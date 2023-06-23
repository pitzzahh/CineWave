using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineWave.MVVM.Model.Movies;

public class Genre
{
    [Key]
    public int GenreId { get; set; }
    
    [ForeignKey("Movie")] public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!; // Navigation property
    
    public EGenre Name { get; set; }

    public Genre(EGenre name)
    {
        Name = name;
    }
}