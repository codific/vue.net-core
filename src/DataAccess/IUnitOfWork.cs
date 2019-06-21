using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        Guid CurrentUserId { get; }
        IStandardRepository GetStandardRepository();
        TRepository GetCustomRepository<TRepository>();
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
