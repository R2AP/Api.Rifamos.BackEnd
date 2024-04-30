using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
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

        //Métodos Básicos
        public async Task<Premio> Get(Int32 oPremioId) => await _premioRepository.Get(oPremioId);

        public async Task<Premio> Insert(Premio oPremio)
        {
     
            await _premioRepository.Post(oPremio);

            return await Get(oPremio.PremioId);

        }

        public async Task<Premio> Update(Premio oPremio)
        {

            await _premioRepository.Put(oPremio);

            return await Get(oPremio.PremioId);

        }

        public async Task<Premio> Delete(Int32 oPremioId)
        {

            Premio oPremio = await Get(oPremioId);

            await _premioRepository.Delete(oPremio);

            return oPremio;

        }

		//Métodos Complementarios
        public async Task<Premio> GetPremio(Int32 oPremioId)
        {
            return await Get(oPremioId);
        }

        public async Task<PremioFrontDTO> InsertPremio(PremioDTO oPremioDTO)
        {

            Premio oPremio = new()
            {

                RifaId = oPremioDTO.RifaId,
                PremioDescripcion = oPremioDTO.PremioDescripcion,
                PremioDetalle = oPremioDTO.PremioDetalle,
                Url = oPremioDTO.Url,
                Imagen = oPremioDTO.Imagen,
                AuditoriaUsuarioIngreso = oPremioDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now,

            };

            oPremio = await Insert(oPremio);

            PremioFrontDTO oPremioFrontDTO = new()
            {
                PremioId = oPremio.PremioId,
                RifaId = oPremio.RifaId,
                PremioDescripcion = oPremio.PremioDescripcion,
                PremioDetalle = oPremio.PremioDetalle,
                Url = oPremio.Url,
                Imagen = oPremio.Imagen
            };

            return oPremioFrontDTO;            

        }

        public async Task<PremioFrontDTO> UpdatePremio(PremioDTO oPremioDTO)
        {
            Premio oPremio = await _premioRepository.Get(oPremioDTO.PremioId);

            oPremio.RifaId = oPremioDTO.RifaId;
            oPremio.PremioDescripcion = oPremioDTO.PremioDescripcion;
            oPremio.PremioDetalle = oPremioDTO.PremioDetalle;
            oPremio.Url = oPremioDTO.Url;
            oPremio.Imagen = oPremioDTO.Imagen;
            oPremio.AuditoriaUsuarioModificacion = oPremioDTO.AuditoriaUsuario;
            oPremio.AuditoriaFechaModificacion = DateTime.Now;

            oPremio = await Update(oPremio);

            PremioFrontDTO oPremioFrontDTO = new()
            {
                PremioId = oPremio.PremioId,
                RifaId = oPremio.RifaId,
                PremioDescripcion = oPremio.PremioDescripcion,
                PremioDetalle = oPremio.PremioDetalle,
                Url = oPremio.Url,
                Imagen = oPremio.Imagen
            };

            return oPremioFrontDTO;

        }

        public async Task<PremioFrontDTO> DeletePremio(Int32 oPremioId)
        {

            Premio oPremio = await Delete(oPremioId);

            PremioFrontDTO oPremioFrontDTO = new()
            {
                PremioId = oPremio.PremioId,
                RifaId = oPremio.RifaId,
                PremioDescripcion = oPremio.PremioDescripcion,
                PremioDetalle = oPremio.PremioDetalle,
                Url = oPremio.Url,
                Imagen = oPremio.Imagen
            };

            return oPremioFrontDTO;

        }

        public async Task<List<Premio>> GetListPremio(Int32 oRifaId)
        {
            return await _premioRepository.GetListPremio(oRifaId);
        }


    }

}