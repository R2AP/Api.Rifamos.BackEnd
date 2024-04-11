using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Api.Rifamos.BackEnd.Adapter;

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

        public async Task<Opcion> GetOpcion(Int32 OpcionId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _opcionRepository.Get(OpcionId);
        } 

        public async Task<Opcion> InsertOpcion(OpcionDTO OpcionDTO)
        {
            
            Opcion oOpcion = new Opcion{
                RifaId = OpcionDTO.RifaId,
                UsuarioId = OpcionDTO.UsuarioId,
                CantidadOpciones = OpcionDTO.CantidadOpciones,
                TokenOpcion = OpcionDTO.TokenOpcion,
                EstadoOpcion = OpcionDTO.EstadoOpcion, 
                AuditoriaUsuarioIngreso = OpcionDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now
            };

            await _opcionRepository.Post(oOpcion);

            return oOpcion;

        }

        public async Task<Opcion> UpdateOpcion(OpcionDTO OpcionDTO)
        {

            Opcion oOpcion = await _opcionRepository.Get(OpcionDTO.OpcionId);

            oOpcion.RifaId = OpcionDTO.RifaId;
            oOpcion.UsuarioId = OpcionDTO.UsuarioId;
            oOpcion.CantidadOpciones = OpcionDTO.CantidadOpciones;
            oOpcion.TokenOpcion = OpcionDTO.TokenOpcion;
            oOpcion.EstadoOpcion = OpcionDTO.EstadoOpcion;
            oOpcion.AuditoriaUsuarioModificacion = OpcionDTO.AuditoriaUsuarioModificacion;
            oOpcion.AuditoriaFechaModificacion = DateTime.Now;

            await _opcionRepository.Put(oOpcion);

            return oOpcion;

        }

        public async Task<Opcion> DeleteOpcion(Int32 OpcionId)
        {

            Opcion oOpcion = new Opcion{
                OpcionId =  OpcionId       
            };

            await _opcionRepository.Delete(oOpcion);

            return oOpcion;

        }
    }

}