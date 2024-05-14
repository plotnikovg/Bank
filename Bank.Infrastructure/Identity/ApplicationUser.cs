namespace Bank.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser(string phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}