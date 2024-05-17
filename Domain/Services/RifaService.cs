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
    public class RifaService : IRifaService
    {
        private readonly IRifaRepository _rifaRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaService));
        readonly string sServicio = "RifaService: ";

        public RifaService(IRifaRepository rifaRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _rifaRepository = rifaRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        //Métodos Básicos
        public async Task<Rifa> Get(Int32 oRifaId) => await _rifaRepository.Get(oRifaId);

        public async Task<Rifa> Insert(Rifa oRifa)
        {
     
            await _rifaRepository.Post(oRifa);

            return await Get(oRifa.RifaId);

        }

        public async Task<Rifa> Update(Rifa oRifa)
        {

            await _rifaRepository.Put(oRifa);

            return await Get(oRifa.RifaId);

        }

        public async Task<Rifa> Delete(Int32 oRifaId)
        {

            Rifa oRifa = await Get(oRifaId);

            await _rifaRepository.Delete(oRifa);

            return oRifa;

        }

		//Métodos Complementarios
        public async Task<RifaFrontDTO> GetRifa(Int32 oRifaId)
        {
            
            Rifa oRifa = await Get(oRifaId);

            if (oRifa == null) return null;

            RifaFrontDTO oRifaFrontDTO = new(){

                RifaId = oRifa.RifaId,
                RifaDescripcion = oRifa.RifaDescripcion,
                FechaSorteo = oRifa.FechaSorteo,
                HoraSorteo =  oRifa.HoraSorteo,
                Imagen = oRifa.Imagen,
                Sponsor = oRifa.Sponsor,
                EstadoRifa = oRifa.EstadoRifa

            };

            return oRifaFrontDTO;
        }

        public async Task<RifaFrontDTO> InsertRifa(RifaDTO oRifaDTO)
        {

            string sPath = @"C:\\Users\\romul\\Downloads\\template.png";

            if (File.Exists(sPath)){
                byte[] oFile = new byte[1024];
                Stream oStream = File.Open(sPath,FileMode.Open,FileAccess.Read,FileShare.None);
                MemoryStream oMemoryStream = new();
                oStream.CopyTo(oMemoryStream);
                oFile = oMemoryStream.ToArray();
                oRifaDTO.Imagen = oFile;
                oMemoryStream.Close();
                oStream.Close();
            }

            Rifa oRifa = new(){

                RifaId = oRifaDTO.RifaId,
                RifaDescripcion = oRifaDTO.RifaDescripcion,
                FechaSorteo = oRifaDTO.FechaSorteo,
                HoraSorteo =  oRifaDTO.HoraSorteo,
                Imagen = oRifaDTO.Imagen,
                Sponsor = oRifaDTO.Sponsor,
                EstadoRifa = oRifaDTO.EstadoRifa,
                AuditoriaUsuarioIngreso = oRifaDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
                
            };

            await Insert(oRifa);

            RifaFrontDTO oRifaFrontDTO = new(){

                RifaId = oRifa.RifaId,
                RifaDescripcion = oRifa.RifaDescripcion,
                FechaSorteo = oRifa.FechaSorteo,
                HoraSorteo =  oRifa.HoraSorteo,
                Imagen = oRifa.Imagen,
                Sponsor = oRifa.Sponsor,
                EstadoRifa = oRifa.EstadoRifa

            };

            return oRifaFrontDTO;

        }
        public async Task<RifaFrontDTO> UpdateRifa(RifaDTO RifaDTO)
        {

            Rifa oRifa = await Get(RifaDTO.RifaId);

            string sPath = @"C:\\Users\\romul\\Downloads\\template.png";

            if (File.Exists(sPath)){
                byte[] oFile = new byte[1024];
                Stream oStream = File.Open(sPath,FileMode.Open,FileAccess.Read,FileShare.None);
                MemoryStream oMemoryStream = new();
                oStream.CopyTo(oMemoryStream);
                oFile = oMemoryStream.ToArray();
                oRifa.Imagen = oFile;
                oMemoryStream.Close();
                oStream.Close();
            }

            oRifa.RifaDescripcion = RifaDTO.RifaDescripcion;
            oRifa.FechaSorteo = RifaDTO.FechaSorteo;
            oRifa.HoraSorteo =  RifaDTO.HoraSorteo;
            //oRifa.Imagen = RifaDTO.Imagen;
            oRifa.Sponsor = RifaDTO.Sponsor;
            oRifa.EstadoRifa = RifaDTO.EstadoRifa;
            oRifa.AuditoriaUsuarioModificacion = RifaDTO.AuditoriaUsuario;
            oRifa.AuditoriaFechaModificacion = DateTime.Now;

            await Update(oRifa);

            RifaFrontDTO oRifaFrontDTO = new(){

                RifaId = oRifa.RifaId,
                RifaDescripcion = oRifa.RifaDescripcion,
                FechaSorteo = oRifa.FechaSorteo,
                HoraSorteo =  oRifa.HoraSorteo,
                Imagen = oRifa.Imagen,
                Sponsor = oRifa.Sponsor,
                EstadoRifa = oRifa.EstadoRifa

            };

            return oRifaFrontDTO;

        }

        public async Task<RifaFrontDTO> DeleteRifa(Int32 oRifaId)
        {

            Rifa oRifa = await Delete(oRifaId);

            RifaFrontDTO oRifaFrontDTO = new(){

                RifaId = oRifa.RifaId,
                RifaDescripcion = oRifa.RifaDescripcion,
                FechaSorteo = oRifa.FechaSorteo,
                HoraSorteo =  oRifa.HoraSorteo,
                Imagen = oRifa.Imagen,
                Sponsor = oRifa.Sponsor,
                EstadoRifa = oRifa.EstadoRifa

            };

            return oRifaFrontDTO;

        }        

        public async Task<List<RifaFrontDTO>> GetListRifaUsuario(Int32 oUsuarioId)
        {

            List<RifaFrontDTO> oListRifaFrontDTO = [];

            List<Rifa> oListRifa = await _rifaRepository.GetListRifaUsuario(oUsuarioId);

            foreach(var oItem in oListRifa) {
                
                Rifa oRifa = oItem;

                RifaFrontDTO oRifaFrontDTO = new(){
                    RifaId = oRifa.RifaId,
                    RifaDescripcion = oRifa.RifaDescripcion,
                    FechaSorteo = oRifa.FechaSorteo,
                    HoraSorteo =  oRifa.HoraSorteo,
                    Imagen = oRifa.Imagen,
                    Sponsor = oRifa.Sponsor,
                    EstadoRifa = oRifa.EstadoRifa
                };

                oListRifaFrontDTO.Add(oRifaFrontDTO);
            }

            return oListRifaFrontDTO;
        } 

        public async Task<List<RifaFrontDTO>> GetListRifaEstado(Int32 oEstadoId)
        {

            List<RifaFrontDTO> oListRifaFrontDTO = [];

            List<Rifa> oListRifa = await _rifaRepository.GetListRifaEstado(oEstadoId);

            if (oListRifa.Count==0){
                RifaFrontDTO oRifaFrontDTO = new()
                {
                    Error = true,
                    Mensaje = "No se encontraron coincidencias para los criterios de búsqueda."
                };
                oListRifaFrontDTO.Add(oRifaFrontDTO);
                log.Error(sServicio + oRifaFrontDTO.Mensaje);
                return oListRifaFrontDTO;

            }

            foreach(var oItem in oListRifa) {
                
                Rifa oRifa = oItem;

                RifaFrontDTO oRifaFrontDTO = new(){
                    RifaId = oRifa.RifaId,
                    RifaDescripcion = oRifa.RifaDescripcion,
                    FechaSorteo = oRifa.FechaSorteo,
                    HoraSorteo =  oRifa.HoraSorteo,
                    Imagen = oRifa.Imagen,
                    Sponsor = oRifa.Sponsor,
                    EstadoRifa = oRifa.EstadoRifa
                };

                oListRifaFrontDTO.Add(oRifaFrontDTO);
            }

            return oListRifaFrontDTO;

        }
      
    }

}
