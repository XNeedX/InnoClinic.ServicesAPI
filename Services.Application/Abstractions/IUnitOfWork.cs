using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Application.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
