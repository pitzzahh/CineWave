using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using CineWave.DB.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CineWave.DB.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly Collection<T> _collection = new();

    protected Repository(DbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _collection.Add(entity);
    }

    public void Remove(T expression)
    {
        _context.Set<T>().Remove(expression);
        _collection.Remove(expression);
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public IEnumerable<T> GetAll()
    {
        var enumerable = _context.Set<T>().ToList();
        enumerable.ToList().ForEach(_collection.Add);
        return enumerable;
    }
    
    public List<T> ToList()
    {
        return _collection.ToList();
    }
}