using Identity.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Identity.Infrastructure.Persistence.Interceptors;

internal sealed class AuditableEntityInterceptor(TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        UpdateTimestamps(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateTimestamps(eventData.Context);

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    private void UpdateTimestamps(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        DateTimeOffset utcNow = timeProvider.GetUtcNow();

        IEnumerable<EntityEntry<IHasAuditTimestamps>> entries =
            context.ChangeTracker
                .Entries<IHasAuditTimestamps>()
                .Where(entry => entry.State is EntityState.Added or EntityState.Modified);

        foreach (EntityEntry<IHasAuditTimestamps> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc = utcNow;
            }
            else
            {
                entry.Property(nameof(IHasAuditTimestamps.CreatedAtUtc)).IsModified = false;
            }

            entry.Entity.UpdatedAtUtc = utcNow;
        }
    }
}