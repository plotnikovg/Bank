namespace Bank.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole() : base()
    {
        
    }

    public ApplicationRole(string roleName)
    {
        Name = roleName;
    }
}