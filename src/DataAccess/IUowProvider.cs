using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IUowProvider
    {
        IUnitOfWork CreateUnitOfWork<TContext>(bool trackChanges = true, bool enableLogging = false)
            where TContext : DbContext;
    }
}
