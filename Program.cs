using Api.Rifamos.BackEnd.Configuration;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

var configurer = new ConfigurationBuilder()
                .AddEnvironmentVariables();

IConfiguration Configuration = configurer.Build();

// Add services to the container.
builder.Services.AddDbContext<RifamosContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Rifamos")));

builder.Services.AddControllers();
builder.Services.ResolveDependencies();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
