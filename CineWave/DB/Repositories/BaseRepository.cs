using CineWave.MVVM.Model;

namespace CineWave.DB.Repositories;

public abstract class BaseRepository
{
    private readonly string _connectionString;

    protected BaseRepository()
    {
        _connectionString = "Server=(local); Database=CineWave; Integrated Security=true;";
    }

}