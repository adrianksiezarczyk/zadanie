using Cars.Application.Interfaces;

namespace Cars.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}