namespace Cars.Domain.Entities;

public class Car
{
    public long Id { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int ProductionYear { get; set; }
}
