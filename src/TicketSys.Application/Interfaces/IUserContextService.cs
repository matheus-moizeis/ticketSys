namespace TicketSys.Application.Interfaces;

public interface IUserContextService
{
    string? UserId { get; }
    string? UserName { get; }
    string? Email { get; }
}
