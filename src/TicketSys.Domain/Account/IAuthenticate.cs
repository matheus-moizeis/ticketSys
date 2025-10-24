using TicketSys.Domain.Entities;

namespace TicketSys.Domain.Account;

public interface IAuthenticate
{
    Task<bool> Authenticate(string email, string password);
    Task<bool> RegisterUser(string email, string password, string name, string idRole);
    Task Logout();
    Task<IEnumerable<ApplicationUser>> GetAllAsync();
    Task<bool> UpdateUser(string id, string email, string password, string name, bool isActive, string idRole);
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<ApplicationUser?> GetUserByIdAsync(string id);
    Task<Role?> GetRoleByIdAsync(string id);
}
