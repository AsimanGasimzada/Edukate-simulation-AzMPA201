using System.ComponentModel.DataAnnotations;

namespace Edukate_simulation_AzMPA201.ViewModels.UserViewModels;

public class RegisterVM
{
    [Required, MaxLength(64)]
    public string Username { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, MaxLength(128)]
    public string Fullname { get; set; } = string.Empty;
    [Required, MinLength(6), DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Required, MinLength(6), DataType(DataType.Password), Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}