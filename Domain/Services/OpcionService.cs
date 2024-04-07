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
    public class OpcionService : IOpcionService
    {
        private readonly IOpcionRepository _opcionRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public OpcionService(IOpcionRepository opcionRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _opcionRepository = opcionRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<List<Opcion>> GetListOpcion(Int32 RifaId, Int32 UsuarioId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _opcionRepository.GetListOpcion(RifaId, UsuarioId);
        } 

        public async Task<Opcion> InsertOpcion(Opcion Opcion)
        {
            // var ejemplo = _configuration["prueba"];
            await _opcionRepository.Post(Opcion);

            return Opcion;

        }

        public async Task<Opcion> UpdateOpcion(Opcion Opcion)
        {
            // var ejemplo = _configuration["prueba"];
            await _opcionRepository.Put(Opcion);

            return Opcion;

        }

    }

}