using System.Security.Claims;
using TicketSys.Application.Interfaces;

namespace TicketSys.WebUI.Services;

public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    public string? UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? UserName => User?.Identity?.Name;
    public string? Email => User?.FindFirstValue(ClaimTypes.Email);
}
