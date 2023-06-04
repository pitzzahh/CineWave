using System.Collections.Generic;

namespace CineWave.MVVM.Model;

public class CinemaHall
{
    internal List<Seat> Seats { get; } = new();

    public CinemaHall(int numberOfSeats)
    {
        InitializeSeats(numberOfSeats);
    }

    private void InitializeSeats(int numberOfSeats)
    {
        for (var i = 1; i <= numberOfSeats; i++)
        {
            Seats.Add(new Seat(i));
        }
    }
}