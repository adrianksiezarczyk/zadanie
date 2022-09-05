namespace Cars.Domain.Common;
public abstract class AuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
}
