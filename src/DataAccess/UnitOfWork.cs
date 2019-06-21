using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        protected TContext context;
        protected readonly IServiceProvider serviceProvider;
        protected Guid currentUserId;

        protected bool isDisposed;

        protected internal UnitOfWork(TContext context, IServiceProvider serviceProvider)
        {
            this.context = context;
            this.serviceProvider = serviceProvider;
        }

        public Guid CurrentUserId
        {
            get
            {
                return this.currentUserId;
            }
        }

        public int SaveChanges()
        {
            CheckDisposed();
            return this.context.SaveChanges();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            CheckDisposed();
            return this.context.SaveChanges(acceptAllChangesOnSuccess);
        }

        public Task<int> SaveChangesAsync()
        {
            CheckDisposed();
            return this.context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            CheckDisposed();
            return this.context.SaveChangesAsync(cancellationToken);
        }

        public IStandardRepository GetStandardRepository()
        {
            CheckDisposed();
            var repositoryType = typeof(IStandardRepository);
            var repository = (IStandardRepository)this.serviceProvider.GetService(repositoryType);
            if (repository == null)
            {
                throw new ArgumentException(String.Format("Repository {0} has not been found in the IoC container.", repositoryType.Name));
            }

            ((IRepositoryInjection)repository).SetContext(this.context);
            return repository;
        }

        public TRepository GetCustomRepository<TRepository>()
        {
            CheckDisposed();
            var repositoryType = typeof(TRepository);
            var repository = (TRepository)this.serviceProvider.GetService(repositoryType);
            if (repository == null)
            {
                throw new ArgumentException(String.Format("Repository {0} has not been found in the IoC container.", repositoryType.Name));
            }

            ((IRepositoryInjection)repository).SetContext(this.context);
            return repository;
        }

        protected void CheckDisposed()
        {
            if (this.isDisposed) throw new ObjectDisposedException("The UOW is already disposed.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    if (this.context != null)
                    {
                        this.context.Dispose();
                        this.context = null;
                    }
                }
            }
            this.isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
