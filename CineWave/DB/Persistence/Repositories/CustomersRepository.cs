using CineWave.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineWave.DB.Persistence.Repositories;

public class CustomersRepository : Repository<Customer>
{
    public CustomersRepository(DbContext context) : base(context)
    {
        new DatabaseFacade(context).EnsureCreatedAsync();
    }
}