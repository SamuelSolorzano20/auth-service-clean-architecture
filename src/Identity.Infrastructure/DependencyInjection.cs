using Identity.Infrastructure.Identity;
using Identity.Infrastructure.Persistance;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Persistence.Interceptors;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Identity.Infrastructure;

public static class DependencyInjection
{
    private const string IamDatabaseConnectionString = "IamDatabase";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        string? connectionString = configuration.GetConnectionString(IamDatabaseConnectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"The connection string " +
                $"'{IamDatabaseConnectionString}' is missing.");
        }

        services.TryAddSingleton(TimeProvider.System);

        services.AddScoped<AuditableEntityInterceptor>();

        services.AddDbContext<IamDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(IamDbContext).Assembly.FullName);

                npgsqlOptions.MigrationsHistoryTable(
                    IamDatabase.MigrationHistoryTable,
                    IamDatabase.Schema);

                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(2),
                    errorCodesToAdd: null);
            });

            options.UseSnakeCaseNamingConvention();

            options.AddInterceptors(serviceProvider.GetRequiredService<AuditableEntityInterceptor>());
        });

        services
            .AddIdentityCore<ApplicationUser>(ConfigureIdentity)
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<IamDbContext>();

        return services;
    }

    private static void ConfigureIdentity(IdentityOptions options)
    {
        options.User.RequireUniqueEmail = true;

        options.SignIn.RequireConfirmedEmail = true;

        options.Password.RequiredLength = 12;
        options.Password.RequiredUniqueChars = 4;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;

        // Passphrases remain valid without requiring symbols.
        options.Password.RequireNonAlphanumeric = false;

        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

        options.Stores.MaxLengthForKeys = 128;
    }
}