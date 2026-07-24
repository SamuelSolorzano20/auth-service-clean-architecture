namespace Identity.Infrastructure.Common;

public interface IHasAuditTimestamps
{
    DateTimeOffset CreatedAtUtc { get; set; }
    DateTimeOffset UpdatedAtUtc { get; set; }
}