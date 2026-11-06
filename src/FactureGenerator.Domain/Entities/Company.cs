namespace FactureGenerator.Domain.Entities;

public class Company
{
    public int Id { get; set; }
    public string Nif { get; set; } = string.Empty;
    public string Stat { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
