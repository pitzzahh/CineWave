using System.Collections.Generic;

namespace CineWave.MVVM.Model;

public class CinemaHall
{
    internal string RoomNumber { get; set; }
    internal List<Seat> Seats { get; } = new();

    public CinemaHall(int numberOfSeats, string roomNumber)
    {
        RoomNumber = roomNumber;
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