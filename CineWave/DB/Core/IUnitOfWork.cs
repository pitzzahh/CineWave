using System;
using CineWave.DB.Persistence.Repositories;

namespace CineWave.DB.Core;

public interface IUnitOfWork : IDisposable
{
    MoviesRepository MoviesRepository { get; }
    int Complete();
}