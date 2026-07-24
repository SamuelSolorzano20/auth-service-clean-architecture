using Identity.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Identity;

public sealed class ApplicationRole : IdentityRole<Guid>, IHasAuditTimestamps
{
    public ApplicationRole()
    {
        Id = Guid.CreateVersion7();
    }

    public string? Description { get; set; }
    public bool IsSystemRole { get; set; }
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}