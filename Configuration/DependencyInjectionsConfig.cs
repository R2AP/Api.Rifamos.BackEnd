using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Persistence.Repositories;
using Api.Rifamos.BackEnd.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Rifamos.BackEnd.Configuration
{

    public static class DependencyInjectionsConfig
    {

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Servicios
            services.AddScoped<IOpcionService, OpcionService>();
            services.AddScoped<IPrecioService, PrecioService>();
            services.AddScoped<IPremioService, PremioService>();
            services.AddScoped<IRifaService, RifaService>();
            services.AddScoped<IVentaService, VentaService>();

            //Repositorios
            services.AddScoped<IOpcionRepository, OpcionRepository>();
            services.AddScoped<IPrecioRepository, PrecioRepository>();
            services.AddScoped<IPremioRepository, PremioRepository>();
            services.AddScoped<IRifaRepository, RifaRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();

            return services;
        }

    }

}