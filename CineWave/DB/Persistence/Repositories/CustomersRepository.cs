using CineWave.MVVM.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineWave.DB.Persistence.Repositories;

public class CustomersRepository : Repository<Customer>
{
    public CustomersRepository(CineWaveDataContext context) : base(context)
    {
        new DatabaseFacade(context).EnsureCreatedAsync();
    }
}