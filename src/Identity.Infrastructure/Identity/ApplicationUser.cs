using Identity.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>, IHasAuditTimestamps
{
    public ApplicationUser()
    {
        Id = Guid.CreateVersion7();
    }

    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTimeOffset? DeactivatedAtUtc { get; set; }
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
}