using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Identity.Models;

public class LoginViewModel
{
    public string ReturnUrl { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}