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
    public class RifaService : IRifaService
    {
        private readonly IRifaRepository _rifaRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public RifaService(IRifaRepository rifaRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _rifaRepository = rifaRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Rifa> GetRifa(Int32 RifaId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _rifaRepository.Get(RifaId);
        }

        public async Task<List<Rifa>> GetListRifaUsuario(Int32 UsuarioId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _rifaRepository.GetListRifaUsuario(UsuarioId);
        } 

        public async Task<List<Rifa>> GetListRifaEstado(Int32 EstadoId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _rifaRepository.GetListRifaEstado(EstadoId);
        }           
    }

    
}