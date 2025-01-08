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
using log4net;


namespace Api.Rifamos.BackEnd.Domain.Services{
    public class GanadorService : IGanadorService 
    {
        private readonly IGanadorRepository _ganadorRepository;
        private readonly IPremioRepository _premioRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        private static readonly ILog log = LogManager.GetLogger(typeof(GanadorService));
        readonly string sServicio = "GanadorService: ";

        public GanadorService(IGanadorRepository ganadorRepository,
                            IPremioRepository premioRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _ganadorRepository = ganadorRepository;
            _premioRepository = premioRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        //Métodos Básicos
        public async Task<Ganador> Get(Int32 oPremioId) => await _ganadorRepository.Get(oPremioId);

        public async Task<Ganador> Insert(Ganador oGanador)
        {
     
            await _ganadorRepository.Post(oGanador);

            return await Get(oGanador.PremioId);

        }

        public async Task<Ganador> Update(Ganador oGanador)
        {

            await _ganadorRepository.Put(oGanador);

            return await Get(oGanador.PremioId);

        }

        public async Task<Ganador> Delete(Int32 oRifaId)
        {

            Ganador oGanador = await Get(oRifaId);

            await _ganadorRepository.Delete(oGanador);

            return oGanador;

        }

		//Métodos Complementarios
        public async Task<GanadorFrontDTO> InsertGanador(GanadorDTO oGanadorDTO)
        {

            Ganador oGanador = new(){

                PremioId = oGanadorDTO.PremioId,
                RiferoId = oGanadorDTO.RiferoId,
                AuditoriaUsuarioIngreso = oGanadorDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now
                
            };

            await Insert(oGanador);

            GanadorFrontDTO oGanadorFrontDTO = new(){

                PremioId = oGanador.PremioId,
                RiferoId = oGanador.RiferoId

            };

            return oGanadorFrontDTO;

        }

        public async Task<GanadorFrontDTO> UpdateGanador(GanadorDTO oGanadorDTO)
        {

            Ganador oGanador = await Get(oGanadorDTO.PremioId);

            oGanador.PremioId = oGanadorDTO.PremioId;
            oGanador.RiferoId = oGanadorDTO.RiferoId;
            oGanador.AuditoriaUsuarioModificacion = oGanadorDTO.AuditoriaUsuarioModificacion;
            oGanador.AuditoriaFechaModificacion = DateTime.Now;

            await Update(oGanador);

            GanadorFrontDTO oGanadorFrontDTO = new(){

                PremioId = oGanador.PremioId,
                RiferoId = oGanador.RiferoId,

            };

            return oGanadorFrontDTO;

        }

        public async Task<GanadorFrontDTO> DeleteGanador(Int32 oPremioId)
        {

            Ganador oGanador = await Get(oPremioId); 

            await Delete(oPremioId);

            GanadorFrontDTO oGanadorFrontDTO = new(){

                PremioId = oGanador.PremioId,
                RiferoId = oGanador.RiferoId,

            };

            return oGanadorFrontDTO;

        }        

        public async Task<Ganador> GetGanadorPorPremio(Int32 oPremioId)
        {
            
            Ganador oGanador = new(){}; //await _ganadorRepository.GetGanadorPorPremio(oPremioId);

            if (oGanador == null) return null;

            // GanadorFrontDTO oGanadorFrontDTO = new(){

            //     PremioId = oGanadorDTO.PremioId,
            //     RiferoId = oGanadorDTO.RiferoId,

            // };

            return oGanador;
        }
      
    }

}
