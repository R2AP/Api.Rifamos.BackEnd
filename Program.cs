using Api.Rifamos.BackEnd.Configuration;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

var configurer = new ConfigurationBuilder()
                .AddEnvironmentVariables();

//Cors
var RifamosPolicyAllowSpecificOrigins = "_RifamosPolicyAllowSpecificOrigins";

IConfiguration Configuration = configurer.Build();

// Add services to the container.
builder.Services.AddDbContext<RifamosContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Rifamos")));

// 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();
builder.Services.ResolveDependencies();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors
builder.Services.AddCors(options => {
    options.AddPolicy( name: RifamosPolicyAllowSpecificOrigins, policy => {
        policy.WithOrigins("http://localhost:4200");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Habilitar el uso de cors
app.UseCors(RifamosPolicyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
