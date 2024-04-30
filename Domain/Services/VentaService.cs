using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        //private readonly IOpcionRepository _opcionRepository;
        private readonly IOpcionService _opcionService;
        private readonly IPrecioService _precioService;
        private readonly IConfiguration _configuration;

        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public VentaService(IVentaRepository ventaRepository,
                            IOpcionRepository opcionRepository,
                            IOpcionService opcionService,
                            IPrecioService precioService,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _ventaRepository = ventaRepository;
            //_opcionRepository = opcionRepository;
            _opcionService = opcionService;
            _precioService = precioService;
            _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Ventum> Get(Int32 oVentumId) => await _ventaRepository.Get(oVentumId);

        public async Task<Ventum> Insert(Ventum oVentum)
        {
     
            await _ventaRepository.Post(oVentum);

            return await Get(oVentum.VentaId);

        }

        public async Task<Ventum> Update(Ventum oVentum)
        {

            await _ventaRepository.Put(oVentum);

            return await Get(oVentum.VentaId);

        }

        public async Task<Ventum> Delete(Int32 oVentumId) 
        {

            Ventum oVentum = await Get(oVentumId);

            await _ventaRepository.Delete(oVentum);

            return oVentum;

        }

        //Métodos Complementarios
        public async Task<Ventum> GetVenta(Int32 oVentaId) => await _ventaRepository.Get(oVentaId);

        private async Task<Ventum> InsertVenta(VentaDTO oVentaDTO)
        {
     
            Ventum oVenta = new()
            {
                VentaId = oVentaDTO.VentaId,
                OpcionId = oVentaDTO.OpcionId,
                AuditoriaUsuarioIngreso = oVentaDTO.AuditoriaUsuario,  
                AuditoriaFechaIngreso = DateTime.Now
            };

            return await Insert(oVenta);

        }

        private async Task<Ventum> UpdateVenta(VentaDTO oVentaDTO)
        {

            Ventum oVenta = new()
            {
                VentaId = oVentaDTO.VentaId,
                OpcionId = oVentaDTO.OpcionId,
                AuditoriaUsuarioModificacion = oVentaDTO.AuditoriaUsuario,  
                AuditoriaFechaModificacion = DateTime.Now
            };

            return await Update(oVenta);

        }

        private async Task<Ventum> DeleteVenta(Int32 oVentaId)
        {

            Ventum oVenta = await Get(oVentaId);

            await _ventaRepository.Delete(oVenta);

            return oVenta;

        }

        public async Task<VentaFrontDTO> GetVentaOpcion(Int32 oVentaId)
        {
            
            Ventum oVenta = await Get(oVentaId);

            Opcion oOpcion = await _opcionService.Get(oVenta.OpcionId);

            VentaFrontDTO oVentaFrontDTO = new()
            {
                OpcionId = oOpcion.OpcionId,
                RifaId = oOpcion.RifaId,
                CantidadOpciones = oOpcion.CantidadOpciones,
                TokenOpcion = oOpcion.TokenOpcion,
                EstadoOpcion = oOpcion.EstadoOpcion,
                VentaId = oVenta.VentaId,
                TipoComprobante = oVenta.TipoComprobante,
                SerieComprobante = oVenta.SerieComprobante,
                NumeroComprobante = oVenta.NumeroComprobante,
                Moneda = oVenta.Moneda,
                Monto = oVenta.Monto,
                EstadoVenta = oVenta.EstadoVenta
            };

            return oVentaFrontDTO;

        } 

        public async Task<VentaFrontDTO> InsertVentaOpcion(VentaDTO oVentaDTO)
        {

            OpcionDTO oOpcionDTO = new(){
                OpcionId = oVentaDTO.OpcionId, 
                RifaId = oVentaDTO.RifaId,
                UsuarioId = oVentaDTO.UsuarioId,
                CantidadOpciones = oVentaDTO.CantidadOpciones,
                TokenOpcion = "0",
                TokenKey1 = "0",
                TokenKey2 = "0",
                AuditoriaUsuarioIngreso = oVentaDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
            };

            //Insertamos la opción que devuelve el registro recién insertado
            Opcion oOpcion = await _opcionService.InsertOpcion(oOpcionDTO);

            //Obtenemos el precio de la Rifa para registrarlo en la venta
            Precio oPrecio = await _precioService.GetPrecioUnitario(oVentaDTO.RifaId);

            Ventum oVenta = new()
            {
                VentaId =  oVentaDTO.VentaId,
                OpcionId = oOpcionDTO.OpcionId, // Relación entre Opción y Venta
                TipoComprobante = Int32.Parse(_configuration["TipoComprobante:Boleta"]),
                SerieComprobante = _configuration["SeriComprobante:SeriComprobanteBoleta"],
                NumeroComprobante = "0", //VentaDTO.NumeroComprobante,
                Moneda = Int32.Parse(_configuration["Moneda:Soles"]),
                Monto = oPrecio.PrecioUnitario *  oVentaDTO.CantidadOpciones, // VentaDTO.Monto,
                EstadoVenta = Int32.Parse(_configuration["EstadoVenta:Registrada"]),
                AuditoriaUsuarioIngreso = oVentaDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
            };

            //Insertamos la venta que devuelve el registro recién insertado
            oVenta = await Insert(oVenta);

            VentaFrontDTO oVentaFrontDTO = new()
            {
                OpcionId = oOpcion.OpcionId,
                RifaId = oOpcion.RifaId,
                CantidadOpciones = oOpcion.CantidadOpciones,
                TokenOpcion = oOpcion.TokenOpcion,
                EstadoOpcion = oOpcion.EstadoOpcion,
                VentaId = oVenta.VentaId,
                TipoComprobante = oVenta.TipoComprobante,
                SerieComprobante = oVenta.SerieComprobante,
                NumeroComprobante = oVenta.NumeroComprobante,
                Moneda = oVenta.Moneda,
                Monto = oVenta.Monto,
                EstadoVenta = oVenta.EstadoVenta
            };

            return oVentaFrontDTO;

        }
     
    }

}
