﻿using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Identity.Models;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;
}
