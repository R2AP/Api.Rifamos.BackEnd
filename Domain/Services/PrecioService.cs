using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
//using Api.Rifamos.BackEnd.Adapter;
//using Api.Rifamos.BackEnd.Common;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;

namespace Api.Rifamos.BackEnd.Domain.Services{

    public class PrecioService : IPrecioService
    {
        private readonly IPrecioRepository _PrecioRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public PrecioService(IPrecioRepository PrecioRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _PrecioRepository = PrecioRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Precio> GetPrecio(Int32 PrecioId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _PrecioRepository.Get(PrecioId);
        }

        public async Task<List<Precio>> GetListPrecio(Int32 RifaId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _PrecioRepository.GetListPrecio(RifaId);
        }                            
    }

}