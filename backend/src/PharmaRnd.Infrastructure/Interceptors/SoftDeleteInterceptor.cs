using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PharmaRnd.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaRnd.Infrastructure.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, 
            int result, 
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            var entries = eventData.Context.ChangeTracker
                .Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                entry.Entity.Delete();
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
