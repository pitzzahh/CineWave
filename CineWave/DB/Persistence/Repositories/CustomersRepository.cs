using CineWave.MVVM.Model;
using CineWave.MVVM.Model.SeatsBooking;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class CustomersRepository : Repository<Customer>
{
    public CustomersRepository(DbContext context) : base(context)
    {
    }
}