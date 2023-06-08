using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CineWave.DB.Core.Repositories;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Remove(T expression);
    T? GetById(int id);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    IEnumerable<T> GetAll();
}