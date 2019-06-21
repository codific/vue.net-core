using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public abstract class RepositoryBase<TContext> : IRepositoryInjection where TContext : DbContext
    {
        private DbContext context;

        protected RepositoryBase(TContext context)
        {
            this.context = context;
        }

        public DbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepositoryInjection SetContext(DbContext context)
        {
            this.context = (TContext)context;
            return this;
        }
    }
}
