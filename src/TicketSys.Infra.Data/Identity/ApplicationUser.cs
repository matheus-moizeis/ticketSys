using Microsoft.AspNetCore.Identity;

namespace TicketSys.Infra.Data.Identity;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public bool IsActive { get; set; }

}
