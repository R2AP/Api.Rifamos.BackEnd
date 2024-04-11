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

        public async Task<List<Premio>> GetListPremio(Int32 RifaId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _premioRepository.GetListPremio(RifaId);
        }

        public async Task<Premio> GetPremio(Int32 PremioId)
        {
            return await _premioRepository.Get(PremioId);
        }

        public async Task<Premio> InsertPremio(PremioDTO PremioDTO)
        {

            Premio oPremio = new Premio{

                RifaId = PremioDTO.RifaId,
                PremioDescripcion = PremioDTO.PremioDescripcion,
                PremioDetalle = PremioDTO.PremioDetalle,
                Url = PremioDTO.Url,
                //Imagen = PremioDTO.Imagen,
                AuditoriaUsuarioIngreso = PremioDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now,

            };

            await _premioRepository.Post(oPremio);

            return oPremio;

        }

        public async Task<Premio> UpdatePremio(PremioDTO PremioDTO)
        {
            Premio oPremio = await _premioRepository.Get(PremioDTO.PremioId);

            oPremio.RifaId = PremioDTO.RifaId;
            oPremio.PremioDescripcion = PremioDTO.PremioDescripcion;
            oPremio.PremioDetalle = PremioDTO.PremioDetalle;
            oPremio.Url = PremioDTO.Url;
            //oPremio.Imagen = PremioDTO.Imagen;
            oPremio.AuditoriaUsuarioModificacion = PremioDTO.AuditoriaUsuarioIngreso;
            oPremio.AuditoriaFechaModificacion = DateTime.Now;

            await _premioRepository.Put(oPremio);

            return oPremio;

        }

        public async Task<Premio> DeletePremio(Int32 PremioId)
        {
            Premio oPremio = new Premio{
                PremioId = PremioId
            };

            await _premioRepository.Delete(oPremio);

            return oPremio;

        }

    }

}