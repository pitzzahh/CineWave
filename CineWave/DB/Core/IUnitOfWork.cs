using System;
using CineWave.DB.Persistence.Repositories;

namespace CineWave.DB.Core;

public interface IUnitOfWork : IDisposable
{
    MoviesRepository MoviesRepository { get; }
    GenresRepository GenresRepository { get; }
    CustomersRepository CustomersRepository { get; }
    TicketsRepository TicketsRepository { get; }
    SeatsRepository SeatsRepository { get; }
    ReservationsRepository ReservationsRepository { get; }
    int Complete();
}