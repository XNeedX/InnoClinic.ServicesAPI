using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Application.Abstractions;
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    IQueryable<T> Query();
}
