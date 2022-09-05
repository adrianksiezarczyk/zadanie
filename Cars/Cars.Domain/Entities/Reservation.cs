using Cars.Domain.Common;

namespace Cars.Domain.Entities;
public class Reservation : AuditableEntity
{
    public long Id { get; set; }
    public long CarId { get; set; }
    public Car Car { get; set; }
}
