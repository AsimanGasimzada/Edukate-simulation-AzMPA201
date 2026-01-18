using Microsoft.AspNetCore.Identity;

namespace Edukate_simulation_AzMPA201.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = string.Empty;
}
