using Identity.Infrastructure.Identity;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    private const int EmailMaxLength = 256;
    private const int UserNameMaxLength = 256;
    private const int FullNameMaxLength = 200;

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(
            IamDatabase.Tables.Users,
            IamDatabase.Schema,
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "ck_users_full_name_not_blank",
                    "char_length(btrim(full_name)) > 0");
            });

        builder.Property(user => user.FullName)
            .HasMaxLength(FullNameMaxLength)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasMaxLength(EmailMaxLength)
            .IsRequired();

        builder.Property(user => user.NormalizedEmail)
            .HasMaxLength(EmailMaxLength)
            .IsRequired();

        builder.Property(user => user.UserName)
            .HasMaxLength(UserNameMaxLength)
            .IsRequired();

        builder.Property(user => user.NormalizedUserName)
            .HasMaxLength(UserNameMaxLength)
            .IsRequired();

        builder.Property(user => user.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(user => user.CreatedAtUtc)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(user => user.UpdatedAtUtc)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(user => user.DeactivatedAtUtc)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(user => user.NormalizedEmail)
            .IsUnique()
            .HasDatabaseName("ux_users_normalized_email");

        builder.HasIndex(user => user.NormalizedUserName)
            .IsUnique()
            .HasDatabaseName("ux_users_normalized_user_name");
    }
}