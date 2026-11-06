namespace FactureGenerator.Domain.Entities;

public class UserCompany
{
    public string UserId { get; set; } = string.Empty;
    public int CompanyId { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
