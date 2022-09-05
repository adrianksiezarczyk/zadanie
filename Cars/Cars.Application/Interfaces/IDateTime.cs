namespace Cars.Application.Interfaces;
public interface IDateTime
{
    DateTimeOffset UtcNow { get; }
}
