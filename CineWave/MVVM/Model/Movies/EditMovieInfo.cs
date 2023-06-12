using System;

namespace CineWave.MVVM.Model.Movies;

public record EditMovieInfo(
    string MovieName,
    TimeOnly Runtime,
    double MoviePrice,
    DateOnly ReleaseDate,
    DateTime ScreeningDateTime
);