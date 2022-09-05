using Cars.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cars.Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Car> Cars { get; set; }
    DbSet<Reservation> Reservations { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
