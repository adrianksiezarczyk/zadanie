using Cars.Application.Interfaces;
using Cars.Domain.Common;
using Cars.Domain.Entities;
using Cars.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cars.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDateTime dateTime;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime) : base(options)
    {
        this.dateTime = dateTime;
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Reservation> Reservations { get; set; }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.CreatedAt == default)
                        entry.Entity.CreatedAt = dateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedAt = dateTime.UtcNow;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        foreach (var et in modelBuilder.Model.GetEntityTypes())
        {
            if (et.ClrType.IsSubclassOf(typeof(AuditableEntity)))
                et.FindProperty(nameof(AuditableEntity.CreatedAt)).SetDefaultValueSql(DbTypes.CurrentTimestamp);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}