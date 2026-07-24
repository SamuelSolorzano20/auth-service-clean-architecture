using Identity.Infrastructure.Identity;
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence;

public sealed class IamDbContext(DbContextOptions<IamDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(IamDatabase.Schema);

        ConfigureIdentityTables(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IamDbContext).Assembly);
    }

    private static void ConfigureIdentityTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable(IamDatabase.Tables.UserRoles, IamDatabase.Schema);

        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable(IamDatabase.Tables.UserClaims, IamDatabase.Schema);

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable(IamDatabase.Tables.UserLogins, IamDatabase.Schema);

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable(IamDatabase.Tables.UserTokens, IamDatabase.Schema);

        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable(IamDatabase.Tables.RoleClaims, IamDatabase.Schema);
    }
}