﻿using System.Collections.Generic;

namespace CineWave.MVVM.Model;

public interface IRepository<T> where T : class
{
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
}