namespace Bank.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}