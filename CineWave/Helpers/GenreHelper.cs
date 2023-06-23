using CineWave.MVVM.Model.Movies;

namespace CineWave.Helpers;

public static class GenreHelper
{
    public static int GetGenreId(Genre genre)
    {
        return genre switch
        {
            { Name: EGenre.Action } => 1,
            { Name: EGenre.Comedy } => 2,
            { Name: EGenre.Drama } => 3,
            { Name: EGenre.Romance } => 4,
            { Name: EGenre.SciFi } => 5,
            { Name: EGenre.Thriller } => 6,
            _ => 0
        };
    }
}