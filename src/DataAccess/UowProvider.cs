using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DataAccess
{
    public class UowProvider : IUowProvider
    {
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;

        public UowProvider()
        { }

        public UowProvider(ILogger logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public IUnitOfWork CreateUnitOfWork<TContext>(bool trackChanges = true, bool enableLogging = false)
            where TContext : DbContext
        {
            var context = (TContext)this.serviceProvider.GetService(typeof(IDatabaseContext));

            if (!trackChanges)
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var uow = new UnitOfWork<TContext>(context, this.serviceProvider);
            return uow;
        }
    }
}
