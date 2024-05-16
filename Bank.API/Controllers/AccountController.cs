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
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IMediator mediator, ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mediator = mediator;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpGet]
        [Route("login1")]
        public IActionResult Login(string returnUrl = null)
        {
            return File("~/index.html", "text/html");
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
            // await _httpContextAccessor.HttpContext.SignInAsync(
            //     CookieAuthenticationDefaults.AuthenticationScheme,
            //     new ClaimsPrincipal(claimsIdentity),
            //     new AuthenticationProperties());
            await _signInManager.SignInAsync(identityUser, isPersistent: true);
            
            _logger.LogInformation("Success log in. Username: {@request.UserName}", request.UserName);
            return Ok(new { Message = "Logged in" });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity == null || HttpContext.User.Identity.IsAuthenticated == false)
                return Ok(new { Message = "Already logged out"});
            //await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Success log out. Username: {@userName}", HttpContext.User.Identity!.Name);
            return Ok(new { Message = "Logged out"});
        }

        [HttpGet]
        [Route("CheckLogin")]
        public bool CheckLogin()
        {
            return _httpContextAccessor.HttpContext!.User.Identity.IsAuthenticated;
        }
    }
}
