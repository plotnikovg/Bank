using Bank.Domain.Aggregates.ClientAggregate;
using Bank.Domain.Aggregates.BankAccountAggregate;
using Bank.Infrastructure.Repositories;
using Bank.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BankContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
