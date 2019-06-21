using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public abstract class DatabaseContextBase<TContext, TUser, TRole> : IdentityDbContext<TUser, TRole, Guid>, IDatabaseContext
        where TContext : IdentityDbContext<TUser, TRole, Guid>
        where TUser : IdentityUser<Guid>
        where TRole : IdentityRole<Guid>
    {

        public DatabaseContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }

        public Guid CurrentUserId { get; set; }

        public override int SaveChanges()
        {
            UpdateAuditableEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditableEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditableEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditableEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities()
        {
            IEnumerable<EntityEntry> modifiedEntityEntries = ChangeTracker
                .Entries()
                .Where(x => x.Entity is AuditableEntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (EntityEntry entry in modifiedEntityEntries)
            {
                var entity = (AuditableEntityBase)entry.Entity;
                DateTime now = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = now;
                    entity.CreatedBy = CurrentUserId != null ? CurrentUserId.ToString() : null;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedOn).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                }

                entity.UpdatedOn = now;
                entity.UpdatedBy = CurrentUserId != null ? CurrentUserId.ToString() : null;
            }
        }
    }
}
