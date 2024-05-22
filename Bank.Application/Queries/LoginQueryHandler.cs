using Bank.Application.Interfaces;
using Bank.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Bank.Application.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Tuple<bool, UserViewModel>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    private readonly ILogger<LoginQueryHandler> _logger;

    public LoginQueryHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        ILogger<LoginQueryHandler> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<Tuple<bool, UserViewModel>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var identityUser = await _userManager.FindByNameAsync(request.UserName);
        if (identityUser == null)
        {
            _logger.LogInformation("Failed login attempt: User with username {@request.UserName} not found",
                request.UserName);
            return Tuple.Create(false, new UserViewModel());
        }

        var verificationResult = _userManager.PasswordHasher.VerifyHashedPassword(identityUser,
            identityUser.PasswordHash!,
            request.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            _logger.LogInformation("Failed login attempt: Wrong password. Username: {@request.UserName}",
                request.UserName);
            return Tuple.Create(false, new UserViewModel());
        }

        _logger.LogInformation("Success log in. Username: {@request.UserName}", request.UserName);
        return Tuple.Create(true, new UserViewModel
        {
            UserName = identityUser.UserName,
            Token = _jwtGenerator.CreateToken(identityUser)
        });
    }
}