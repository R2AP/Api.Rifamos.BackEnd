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
        private readonly IPremioRepository _premioRepository;
        private readonly IGanadorRepository _ganadorRepository;
        private readonly IPrecioRepository _precioRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaService));
        readonly string sServicio = "RifaService: ";

        public RifaService(IRifaRepository rifaRepository,
                            IPremioRepository premioRepository,
                            IGanadorRepository ganadorRepository,
                            IPrecioRepository precioRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _rifaRepository = rifaRepository;
            _premioRepository = premioRepository;
            _ganadorRepository = ganadorRepository; 
            _precioRepository = precioRepository;
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
                RifaDetalle = oRifa.RifaDetalle,
                FechaSorteo = oRifa.FechaSorteo,
                HoraSorteo =  oRifa.HoraSorteo,
                Sponsor = oRifa.Sponsor,
                EstadoRifa = oRifa.EstadoRifa

            };

            return oRifaFrontDTO;
        }
        public async Task<RifaFrontDTO> InsertRifa(RifaDTO oRifaDTO)
        {

            // string sPath = @"C:\\Users\\romul\\Downloads\\Foto RRAP.png";

            // if (File.Exists(sPath)){
            //     byte[] oFile = new byte[1024];
            //     Stream oStream = File.Open(sPath,FileMode.Open,FileAccess.Read,FileShare.None);
            //     MemoryStream oMemoryStream = new();
            //     oStream.CopyTo(oMemoryStream);
            //     oFile = oMemoryStream.ToArray();
            //     oRifaDTO.Imagen = oFile;
            //     oMemoryStream.Close();
            //     oStream.Close();
            // }

            Rifa oRifa = new(){

                RifaId = oRifaDTO.RifaId,
                RifaDescripcion = oRifaDTO.RifaDescripcion,
                IndicadorPremium = oRifaDTO.IndicadorPremium,
                RifaDetalle = oRifaDTO.RifaDetalle,
                FechaSorteo = oRifaDTO.FechaSorteo,
                HoraSorteo =  oRifaDTO.HoraSorteo,
                Sponsor = oRifaDTO.Sponsor,
                EstadoRifa = oRifaDTO.EstadoRifa,
                AuditoriaUsuarioIngreso = oRifaDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
                
            };

            await Insert(oRifa);

            RifaFrontDTO oRifaFrontDTO = new(){

                RifaId = oRifa.RifaId,
                RifaDescripcion = oRifa.RifaDescripcion,
                IndicadorPremium = oRifa.IndicadorPremium,
                RifaDetalle = oRifa.RifaDetalle,
                FechaSorteo = oRifa.FechaSorteo,
                HoraSorteo =  oRifa.HoraSorteo,
                Sponsor = oRifa.Sponsor,
                EstadoRifa = oRifa.EstadoRifa

            };

            return oRifaFrontDTO;

        }
        public async Task<RifaFrontDTO> UpdateRifa(RifaDTO RifaDTO)
        {

            Rifa oRifa = await Get(RifaDTO.RifaId);

            // string sPath = @"C:\\Users\\romul\\Downloads\\Foto RRAP.png";            

            // if (File.Exists(sPath)){
            //     byte[] oFile = new byte[1024];
            //     Stream oStream = File.Open(sPath,FileMode.Open,FileAccess.Read,FileShare.None);
            //     MemoryStream oMemoryStream = new();
            //     oStream.CopyTo(oMemoryStream);
            //     oFile = oMemoryStream.ToArray();
            //     oMemoryStream.Close();
            //     oStream.Close();
            // }

            oRifa.RifaDescripcion = RifaDTO.RifaDescripcion;
            oRifa.RifaDetalle = RifaDTO.RifaDetalle;
            oRifa.IndicadorPremium = RifaDTO.IndicadorPremium;
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
                IndicadorPremium = oRifa.IndicadorPremium,
                RifaDetalle = oRifa.RifaDetalle,
                FechaSorteo = oRifa.FechaSorteo,
                HoraSorteo =  oRifa.HoraSorteo,
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
                IndicadorPremium = oRifa.IndicadorPremium,
                RifaDetalle = oRifa.RifaDetalle,
                FechaSorteo = oRifa.FechaSorteo,
                HoraSorteo =  oRifa.HoraSorteo,
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
                    IndicadorPremium = oRifa.IndicadorPremium,
                    RifaDetalle = oRifa.RifaDetalle,
                    FechaSorteo = oRifa.FechaSorteo,
                    HoraSorteo =  oRifa.HoraSorteo,
                    Sponsor = oRifa.Sponsor,
                    EstadoRifa = oRifa.EstadoRifa
                };

                oListRifaFrontDTO.Add(oRifaFrontDTO);
            }

            return oListRifaFrontDTO;
        } 

        public async Task<List<RifaFrontDTO>> GetListRifaEstado(Int32 oEstadoId, String oIndicadorPremium)
        {

            List<RifaFrontDTO> oListRifaFrontDTO = [];

            List<Rifa> oListRifa = await _rifaRepository.GetListRifaEstado(oEstadoId);

            if (oListRifa.Count==0){

                RifaFrontDTO oRifaFrontDTO = new()
                {
                    Error = true,
                    Mensaje = "GetListRifaEstado: No se encontraron coincidencias para los criterios de búsqueda."
                };

                log.Error(sServicio + oRifaFrontDTO.Mensaje);
                return oListRifaFrontDTO;

            }

            if (oListRifa.Count!=0){

                foreach(var oItemListRifa in oListRifa) {

                    List<PremioDTO> oListPremioDTO = [];
                    List<PrecioDTO> oListPrecioDTO = [];

                    if (oItemListRifa.IndicadorPremium == oIndicadorPremium){

                        List<Precio> oListPrecio = await _precioRepository.GetListPrecioUnitario(oItemListRifa.RifaId);

                        if (oListPrecio.Count!=0){
                        
                            foreach(var oItemListPrecio in oListPrecio){

                                PrecioDTO oPrecioDTO = new(){

                                    PrecioId = oItemListPrecio.PrecioId,
                                    RifaId = oItemListPrecio.RifaId,
                                    PrecioUnitario = oItemListPrecio.PrecioUnitario,

                                };

                                oListPrecioDTO.Add(oPrecioDTO);

                            }

                        }

                        List<Premio> oListPremio = await _premioRepository.GetListPremio(oItemListRifa.RifaId);

                        if (oListPremio.Count!=0){

                            foreach(var oItemListPremio in oListPremio){

                                List<GanadorDTO> oListGanadorDTO = [];

                                PremioDTO oPremioDTO = new(){

                                    PremioId = oItemListPremio.PremioId,
                                    RifaId = oItemListPremio.RifaId,
                                    PremioDescripcion = oItemListPremio.PremioDescripcion,
                                    PremioDetalle = oItemListPremio.PremioDetalle,
                                    Url = oItemListPremio.Url,
                                    Imagen = oItemListPremio.Imagen,
                                    //ImagenCorta = oItemListPremio.ImagenCorta,

                                };                      

                                List<Ganador> oListGanador = await _ganadorRepository.GetGanadorPorPremio(oItemListPremio.PremioId);

                                if (oListGanador.Count!=0){

                                    foreach(var oItemListGanador in oListGanador){

                                        GanadorDTO oGanadorDTO = new(){
                                            GanadorId = oItemListGanador.GanadorId,
                                            PremioId = oItemListGanador.PremioId,
                                            RiferoId = oItemListGanador.RiferoId,
                                        };

                                        oListGanadorDTO.Add(oGanadorDTO);
                                        oPremioDTO.Ganador = oListGanadorDTO;

                                    }
                                }

                                oListPremioDTO.Add(oPremioDTO);

                            } 
                        }

                        RifaFrontDTO oRifaFrontDTO  = new (){

                            RifaId = oItemListRifa.RifaId, 
                            RifaDescripcion = oItemListRifa.RifaDescripcion, 
                            IndicadorPremium = oItemListRifa.IndicadorPremium, 
                            RifaDetalle = oItemListRifa.RifaDetalle, 
                            FechaSorteo = oItemListRifa.FechaSorteo, 
                            HoraSorteo = oItemListRifa.HoraSorteo, 
                            Sponsor = oItemListRifa.Sponsor, 
                            EstadoRifa = oItemListRifa.EstadoRifa,
                            ListPremio = oListPremioDTO,
                            ListPrecio = oListPrecioDTO,
                            Error = false,
                            Mensaje = "Éxito" 
                        };

                        oListRifaFrontDTO.Add(oRifaFrontDTO);
                        
                    };            
                }
            }

            return oListRifaFrontDTO;

        }   

        public async Task<List<RifaFrontDTO>> GetRifaId(Int32 oRifaId)
        {

            List<RifaFrontDTO> oListRifaFrontDTO = [];

            List<Rifa> oListRifa = await _rifaRepository.GetRifaId(oRifaId);

            if (oListRifa.Count==0){

                RifaFrontDTO oRifaFrontDTO = new()
                {
                    Error = true,
                    Mensaje = "GetListRifaEstado: No se encontraron coincidencias para los criterios de búsqueda."
                };

                log.Error(sServicio + oRifaFrontDTO.Mensaje);
                return oListRifaFrontDTO;

            }

            if (oListRifa.Count!=0){

                foreach(var oItemListRifa in oListRifa) {

                    List<PremioDTO> oListPremioDTO = [];
                    List<PrecioDTO> oListPrecioDTO = [];

                    List<Precio> oListPrecio = await _precioRepository.GetListPrecioUnitario(oItemListRifa.RifaId);

                    if (oListPrecio.Count!=0){
                    
                        foreach(var oItemListPrecio in oListPrecio){

                            PrecioDTO oPrecioDTO = new(){

                                PrecioId = oItemListPrecio.PrecioId,
                                RifaId = oItemListPrecio.RifaId,
                                PrecioUnitario = oItemListPrecio.PrecioUnitario,

                            };

                            oListPrecioDTO.Add(oPrecioDTO);

                        }

                    }

                    List<Premio> oListPremio = await _premioRepository.GetListPremio(oItemListRifa.RifaId);

                    if (oListPremio.Count!=0){

                        foreach(var oItemListPremio in oListPremio){

                            List<GanadorDTO> oListGanadorDTO = [];

                            PremioDTO oPremioDTO = new(){

                                PremioId = oItemListPremio.PremioId,
                                RifaId = oItemListPremio.RifaId,
                                PremioDescripcion = oItemListPremio.PremioDescripcion,
                                PremioDetalle = oItemListPremio.PremioDetalle,
                                Url = oItemListPremio.Url,
                                Imagen = oItemListPremio.Imagen,
                                //ImagenCorta = oItemListPremio.ImagenCorta,

                            };                      

                            List<Ganador> oListGanador = await _ganadorRepository.GetGanadorPorPremio(oItemListPremio.PremioId);

                            if (oListGanador.Count!=0){

                                foreach(var oItemListGanador in oListGanador){

                                    GanadorDTO oGanadorDTO = new(){
                                        GanadorId = oItemListGanador.GanadorId,
                                        PremioId = oItemListGanador.PremioId,
                                        RiferoId = oItemListGanador.RiferoId,
                                    };

                                    oListGanadorDTO.Add(oGanadorDTO);
                                    oPremioDTO.Ganador = oListGanadorDTO;

                                }
                            }

                            oListPremioDTO.Add(oPremioDTO);

                        } 
                    }

                    RifaFrontDTO oRifaFrontDTO  = new (){

                        RifaId = oItemListRifa.RifaId, 
                        RifaDescripcion = oItemListRifa.RifaDescripcion, 
                        IndicadorPremium = oItemListRifa.IndicadorPremium, 
                        RifaDetalle = oItemListRifa.RifaDetalle, 
                        FechaSorteo = oItemListRifa.FechaSorteo, 
                        HoraSorteo = oItemListRifa.HoraSorteo, 
                        Sponsor = oItemListRifa.Sponsor, 
                        EstadoRifa = oItemListRifa.EstadoRifa,
                        ListPremio = oListPremioDTO,
                        ListPrecio = oListPrecioDTO,
                        Error = false,
                        Mensaje = "Éxito" 
                    };

                    oListRifaFrontDTO.Add(oRifaFrontDTO);
                        
                }
            }

            return oListRifaFrontDTO;

        }   

    }

}
