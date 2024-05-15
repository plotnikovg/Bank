using System.ComponentModel.DataAnnotations;

namespace Bank.API.Models;

public class RegistrationRequest
{
    [Required]
    public string Email { get; set; }
    public string UserName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Password { get; set; }
}