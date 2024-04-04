using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
//using Api.Rifamos.BackEnd.Adapter;
//using Api.Rifamos.BackEnd.Common;
using Api.Rifamos.Backend.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class RifaService : IRifaService
    {
        private readonly IRifaRepository _rifaRepository;

        public IConfiguration _configuration { get; }
        private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public RifaService(IRifaRepository rifaRepository,
                            IConfiguration configuration,
                            IHostingEnvironment environment
                            )
        {
            _rifaRepository = rifaRepository;
            _configuration = configuration;
            _environment = environment;
        }

        public async Task<List<Rifa>> GetRifa(Int64 RifaId)
        {
            return await _rifaRepository.GetRifa(RifaId);
        }                
    }

    
}