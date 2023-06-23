using System;
using CineWave.DB.Core;
using CineWave.DB.Persistence.Repositories;

namespace CineWave.DB.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CineWaveDataContext _context;

    public UnitOfWork(CineWaveDataContext context)
    {
        _context = context;
        MoviesRepository = new MoviesRepository(_context);
        GenresRepository = new GenresRepository(_context);
        CustomersRepository = new CustomersRepository(_context);
        TicketsRepository = new TicketsRepository(_context);
        SeatsRepository = new SeatsRepository(_context);
        ReservationsRepository = new ReservationsRepository(_context);
    }

    public MoviesRepository MoviesRepository { get; }
    public GenresRepository GenresRepository { get; }
    public CustomersRepository CustomersRepository { get; }
    public TicketsRepository TicketsRepository { get; }
    public SeatsRepository SeatsRepository { get; }
    public ReservationsRepository ReservationsRepository { get; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public int Complete()
    {
        return _context.SaveChangesAsync().Result;
    }
}