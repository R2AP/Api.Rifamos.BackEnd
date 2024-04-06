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
            //servicios
            //services.AddScoped<IPolizaService, PolizaService>();
            services.AddScoped<IRifaService, RifaService>();
            services.AddScoped<IPrecioService, PrecioService>();
            services.AddScoped<IOpcionService, OpcionService>();
            services.AddScoped<IPremioService, PremioService>();

            //Repositorios
            //services.AddScoped<IPolizaRepository, PolizaRepository>();
            services.AddScoped<IRifaRepository, RifaRepository>();
            services.AddScoped<IPrecioRepository, PrecioRepository>();
            services.AddScoped<IOpcionRepository, OpcionRepository>();
            services.AddScoped<IPremioRepository, PremioRepository>();

            return services;
        }

    }

}