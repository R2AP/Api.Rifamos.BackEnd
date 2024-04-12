using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class SesionService : ISesionService
    {
    private readonly ISesionRepository _sesionRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public SesionService(ISesionRepository SesionRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _sesionRepository = SesionRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Sesion> GetSesion(Int32 SesionId)
        {
            return await _sesionRepository.Get(SesionId);
        }

        // public async Task<List<Rifa>> GetListRifaEstado(Int32 EstadoId)
        // {
        //     // var ejemplo = _configuration["prueba"];
        //     return await _rifaRepository.GetListRifaEstado(EstadoId);
        // }

        public async Task<Sesion> InsertSesion(SesionDTO SesionDTO)
        {
            // var ejemplo = _configuration["prueba"];

            Sesion oSesion = new Sesion(){

                SesionId = SesionDTO.SesionId,
                UsuarioId = SesionDTO.UsuarioId,
                TipoEvento = SesionDTO.TipoEvento,
                Ip = SesionDTO.Ip,
                Email = SesionDTO.Email,
                AuditoriaUsuarioIngreso = SesionDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now
                
            };

            await _sesionRepository.Post(oSesion);

            return oSesion;

        }

        public async Task<Sesion> UpdateSesion(SesionDTO SesionDTO)
        {

            Sesion oSesion = await _sesionRepository.Get(SesionDTO.SesionId);

            oSesion.SesionId = SesionDTO.SesionId;
            oSesion.UsuarioId = SesionDTO.UsuarioId;
            oSesion.TipoEvento = SesionDTO.TipoEvento;
            oSesion.Ip = SesionDTO.Ip;
            oSesion.Email = SesionDTO.Email;
            oSesion.AuditoriaUsuarioIngreso = SesionDTO.AuditoriaUsuarioIngreso;
            oSesion.AuditoriaUsuarioModificacion = oSesion.AuditoriaUsuarioModificacion;
            oSesion.AuditoriaFechaModificacion = DateTime.Now;

            await _sesionRepository.Put(oSesion);

            return oSesion;

        }

        public async Task<Sesion> DeleteSesion(Int32 SesionId)
        {

            Sesion oSesion = await _sesionRepository.Get(SesionId);

            await _sesionRepository.Delete(oSesion);

            return oSesion;

        }        
    }

}
