using System.Security.Claims;
using Bank.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IMediator mediator, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequest? request)
        {
            if (!ModelState.IsValid || request == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }
            
            var identityUser = new IdentityUser { Email = request.Email, UserName = request.PhoneNumber, PhoneNumber = request.PhoneNumber };
            var result = await _userManager.CreateAsync(identityUser, request.Password);
            
            if (!result.Succeeded)
            {
                var errors = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    errors.AddModelError(error.Code, error.Description);
                }
                
                _logger.LogInformation("Errors while registering new user: {@identityUser}:\n\n{@errors}", 
                    identityUser, errors);

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = errors });
            }
            
            _logger.LogInformation("Registered new user: {@identityUser}", identityUser);

            return Ok(new { Message = "User Registration Successful" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest? request)
        {
            if (!ModelState.IsValid || request == null)
                return new BadRequestObjectResult(new {Message = "Login failed"});
            
            var identityUser = await _userManager.FindByNameAsync(request.UserName);
            if (identityUser == null)
            {
                _logger.LogInformation("Failed login attempt: User with username {@request.UserName} not found", request.UserName);
                return new BadRequestObjectResult(new { Message = "User with this phoneNumber not found" });
            }

            var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash!,
                request.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogInformation("Failed login attempt: Wrong password. Username: {@request.UserName}", request.UserName);
                return new BadRequestObjectResult(new { Message = "Wrong password" });
            }

            var claims = new List<Claim?>
            {
                new Claim(ClaimTypes.Email, identityUser.Email!),
                new Claim(ClaimTypes.Name, identityUser.UserName!)
            };

            var claimsIdentity = new ClaimsIdentity(claims!, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            
            _logger.LogInformation("Success log in. Username: {@request.UserName}", request.UserName);
            return Ok(new { Message = "Logged in" });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("Success log out. Username: {@userName}", HttpContext.User.Identity!.Name);
            return Ok(new { Message = "Logged out"});
        }
    }
}
