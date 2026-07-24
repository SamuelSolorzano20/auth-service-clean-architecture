using Identity.Infrastructure.Identity;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    private const int NameMaxLength = 256;
    private const int DescriptionMaxLength = 500;

    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable(IamDatabase.Tables.Roles, IamDatabase.Schema);

        builder.Property(role => role.Name)
            .HasMaxLength(NameMaxLength)
            .IsRequired();

        builder.Property(role => role.NormalizedName)
            .HasMaxLength(NameMaxLength)
            .IsRequired();

        builder.Property(role => role.Description)
            .HasMaxLength(DescriptionMaxLength);

        builder.Property(role => role.IsSystemRole)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(role => role.CreatedAtUtc)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(role => role.UpdatedAtUtc)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.HasIndex(role => role.NormalizedName)
            .IsUnique()
            .HasDatabaseName("ux_roles_normalized_name");
    }
}