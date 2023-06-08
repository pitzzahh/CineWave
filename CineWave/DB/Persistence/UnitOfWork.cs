using CineWave.DB.Core;
using CineWave.DB.Persistence.Repositories;

namespace CineWave.DB.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly MoviesDataContext _context;

    public UnitOfWork(MoviesDataContext context)
    {
        _context = context;
        MoviesRepository = new MoviesRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public MoviesRepository MoviesRepository { get; }
    
    public int Complete()
    {
        return _context.SaveChangesAsync().Result;
    }
}