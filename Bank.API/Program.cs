using System.Configuration;
using System.Reflection;
using System.Text;
using Bank.Application;
using Bank.Application.Interfaces;
using Bank.Application.Repositories;
using Bank.Domain.Aggregates.ClientAggregate;
using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Infrastructure;
using Bank.Infrastructure.Identity;
using Bank.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthCore API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token in this field",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

//DI
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

//Identity
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
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey"]));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

var app = builder.Build();

var serverHeader = app.Configuration["Kestrel:Headers:Server"];
var xPoweredByHeader = app.Configuration["Kestrel:Headers:X-Powered-By"];

// Modify headers in the middleware pipeline
app.Use(async (context, next) =>
{
    // Set custom headers
    context.Response.Headers.Append("Server", serverHeader);
    context.Response.Headers.Append("X-Powered-By", xPoweredByHeader);

    // Set CSP
    // context.Response.Headers.Append("Content-Security-Policy", 
    //     "script-src 'self'; " +
    //     "style-src 'self';" +
    //     "img-src 'self';" +
    //     "font-src 'self' https://fonts.gstatic.com;");
    await next(context);
});


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

// using (var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
//     var roles = new[] { "Admin", "Manager", "Client" };
//     await userManager.AddToRoleAsync(await userManager.FindByNameAsync("+7111"), "Manager");
//     // foreach (var role in roles)
//     // {
//     //     if (!await roleManager.RoleExistsAsync(role))
//     //     {
//     //         await roleManager.CreateAsync(new IdentityRole(role));
//     //     }
//     // }
// }
app.MapControllers();



app.Run();
