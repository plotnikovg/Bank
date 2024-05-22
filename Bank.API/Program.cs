using System.Reflection;
using Bank.Application;
using Bank.Domain.Aggregates.ClientAggregate;
using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Infrastructure;
using Bank.Infrastructure.Identity;
using Bank.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//DI
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

//Identity
// builder.Services.AddDefaultIdentity<ApplicationUser>()
//     .AddRoles<IdentityRole>()
//     .AddEntityFrameworkStores<BankContext>()
    // .AddApiEndpoints();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BankContext>()
    .AddSignInManager<SignInManager<IdentityUser>>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = false;
});
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//     {
//         options.Cookie.HttpOnly = true;
//         options.SlidingExpiration = true;
//         options.ExpireTimeSpan = new TimeSpan(0, 5, 0);
//         options.LoginPath= new Microsoft.AspNetCore.Http.PathString("/login.html");
//         options.AccessDeniedPath = "/index";
//     });
// builder.Services.ConfigureApplicationCookie(options =>
// {
//     // Cookie settings
//     options.Cookie.HttpOnly = true;
//     options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
//
//     options.LoginPath = "/Account/login1";
//     options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//     options.SlidingExpiration = true;
// });

builder.Services.AddHttpContextAccessor();
// builder.Services.ConfigureApplicationCookie(options =>
// {
//     // Cookie settings
//     options.Cookie.HttpOnly = true;
//     options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
//
//     options.LoginPath = "/Areas/Identity/Pages/Account/Login";
//     options.LogoutPath = "/Areas/Identity/Pages/Account/Logout";
//     options.AccessDeniedPath = "/Areas/Identity/Pages/Account/AccessDenied";
//     options.SlidingExpiration = true;
// });

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();

//Razor
// builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// });
app.MapControllers();
//app.MapRazorPages();
//app.MapGroup("/account").MapIdentityApi<ApplicationUser>();
//app.MapIdentityApi<IdentityUser>();
//app.MapControllers();


app.Run();
