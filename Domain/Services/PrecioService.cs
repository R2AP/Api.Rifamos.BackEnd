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

        public async Task<Precio> GetPrecio(Int32 PrecioId)
        {
            // var ejemplo = _configuration["prueba"];
            return await _precioRepository.Get(PrecioId);
        }

        public async Task<Precio> GetPrecioUnitario(Int32 RifaId)
        {
            return await _precioRepository.GetPrecioUnitario(RifaId);
        }

        public async Task<Precio> InsertPrecio(PrecioDTO PrecioDTO)
        {

            Precio oPrecio = new()
            {
                RifaId = PrecioDTO.RifaId,
                PrecioUnitario = PrecioDTO.PrecioUnitario,
                AuditoriaUsuarioIngreso = PrecioDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now
            };

            // var ejemplo = _configuration["prueba"];
            await _precioRepository.Post(oPrecio);

            return oPrecio;

        }

        public async Task<Precio> UpdatePrecio(PrecioDTO PrecioDTO)
        {

            Precio oPrecio = await _precioRepository.Get(PrecioDTO.PrecioId);

            oPrecio.PrecioId = PrecioDTO.PrecioId;
            oPrecio.RifaId = PrecioDTO.RifaId;
            oPrecio.PrecioUnitario = PrecioDTO.PrecioUnitario;
            oPrecio.AuditoriaUsuarioModificacion = PrecioDTO.AuditoriaUsuarioModificacion;
            oPrecio.AuditoriaFechaModificacion = DateTime.Now;

            await _precioRepository.Put(oPrecio);

            return oPrecio;

        }

        public async Task<Precio> DeletePrecio(Int32 PrecioId)
        {

            Precio oPrecio = new()
            {
                PrecioId = PrecioId
            };

            await _precioRepository.Delete(oPrecio);

            return oPrecio;
        }

    }

}