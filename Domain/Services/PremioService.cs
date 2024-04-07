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
    
    public class PremioService : IPremioService
    {
        private readonly IPremioRepository _premioRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public PremioService(IPremioRepository premioRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _premioRepository = premioRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<List<Premio>> GetListPremio(Int32 RifaId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _premioRepository.GetListPremio(RifaId);
        }

        public async Task<Premio> InsertPremio(Premio Premio)
        {
            // var ejemplo = _configuration["prueba"];
            await _premioRepository.Post(Premio);

            return Premio;

        }         

    }

}