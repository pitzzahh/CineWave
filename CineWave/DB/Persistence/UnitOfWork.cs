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
        CustomersRepository = new CustomersRepository(_context);
        TicketsRepository = new TicketsRepository(_context);
        SeatsRepository = new SeatsRepository(_context);
    }

    public MoviesRepository MoviesRepository { get; }
    public CustomersRepository CustomersRepository { get; }
    public TicketsRepository TicketsRepository { get; }
    public SeatsRepository SeatsRepository { get; }

    public void Dispose() => GC.SuppressFinalize(this);

    public int Complete() => _context.SaveChangesAsync().Result;
}