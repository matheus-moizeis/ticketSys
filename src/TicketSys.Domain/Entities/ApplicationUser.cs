namespace TicketSys.Domain.Entities;

public sealed record ApplicationUser()
{
    public string Id { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Name { get; init; }
    public bool IsActive { get; init; }
    public string? TypeOfAccountId { get; set; }
}
