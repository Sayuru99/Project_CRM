using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CRM.Application.Common;

public class RefreshTokenRequest
{
    [Required(ErrorMessage = "Please provide the email")]
    [BindProperty(Name = "email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please provide the Token")]
    [BindProperty(Name = "token")]
    public string Token { get; set; }

    [Required(ErrorMessage = "Please provide the RefreshToken")]
    [BindProperty(Name = "refreshToken")]
    public string RefreshToken { get; set; }
}
