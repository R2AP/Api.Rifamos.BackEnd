using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Api.Rifamos.BackEnd.Domain.Services{

    public class PrecioService : IPrecioService
    {
        private readonly IPrecioRepository _precioRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public PrecioService(IPrecioRepository PrecioRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _precioRepository = PrecioRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        //Métodos Básicos
        public async Task<Precio> Get(Int32 oPrecioId) => await _precioRepository.Get(oPrecioId);

        public async Task<Precio> Insert(Precio oPrecio)
        {
     
            await _precioRepository.Post(oPrecio);

            return await Get(oPrecio.PrecioId);

        }

        public async Task<Precio> Update(Precio oPrecio)
        {

            await _precioRepository.Put(oPrecio);

            return await Get(oPrecio.PrecioId);

        }

        public async Task<Precio> Delete(Int32 oPrecioId)
        {

            Precio oPrecio = await Get(oPrecioId);

            await _precioRepository.Delete(oPrecio);

            return oPrecio;

        }

        //Métodos Complementarios
        public async Task<Precio> GetPrecio(Int32 oPrecioId)
        {
            return await Get(oPrecioId);
        }

        public async Task<Precio> GetPrecioUnitario(Int32 oRifaId)
        {
            return await _precioRepository.GetPrecioUnitario(oRifaId);
        }

        public async Task<PrecioFrontDTO> InsertPrecio(PrecioDTO oPrecioDTO)
        {

            Precio oPrecio = new()
            {
                RifaId = oPrecioDTO.RifaId,
                PrecioUnitario = oPrecioDTO.PrecioUnitario,
                AuditoriaUsuarioIngreso = oPrecioDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
            };

            oPrecio = await Insert(oPrecio);

            PrecioFrontDTO oPrecioFrontDTO = new()
            {
            PrecioId = oPrecio.PrecioId,
            RifaId = oPrecio.RifaId,
            PrecioUnitario = oPrecio.PrecioUnitario
            };

            return oPrecioFrontDTO;

        }

        public async Task<Precio> UpdatePrecio(PrecioDTO oPrecioDTO)
        {

            Precio oPrecio = await _precioRepository.Get(oPrecioDTO.PrecioId);

            oPrecio.PrecioId = oPrecioDTO.PrecioId;
            oPrecio.RifaId = oPrecioDTO.RifaId;
            oPrecio.PrecioUnitario = oPrecioDTO.PrecioUnitario;
            oPrecio.AuditoriaUsuarioModificacion = oPrecioDTO.AuditoriaUsuario;
            oPrecio.AuditoriaFechaModificacion = DateTime.Now;

            await _precioRepository.Put(oPrecio);

            return oPrecio;

        }

        public async Task<Precio> DeletePrecio(Int32 oPrecioId)
        {

            Precio oPrecio = new()
            {
                PrecioId = oPrecioId
            };

            await _precioRepository.Delete(oPrecio);

            return oPrecio;
        }

    }

}