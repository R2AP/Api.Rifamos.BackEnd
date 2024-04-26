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

        public async Task<Ventum> GetVenta(Int32 oVentaId)
        {
            return await _ventaRepository.Get(oVentaId);
        } 

        private async Task<Ventum> InsertVenta(Ventum oVenta)
        {
     
            await _ventaRepository.Post(oVenta);

            return await GetVenta(oVenta.VentaId);

        }

        private async Task<Ventum> UpdateVenta(Ventum oVenta)
        {

            await _ventaRepository.Put(oVenta);

            return await GetVenta(oVenta.VentaId);

        }

        private async Task<Ventum> DeleteVenta(Int32 oVentaId)
        {

            Ventum oVenta = await GetVenta(oVentaId);

            await _ventaRepository.Delete(oVenta);

            return oVenta;

        }

        public async Task<VentaFrontDTO> MCGetVenta(Int32 oVentaId)
        {
            
            Ventum oVenta = await GetVenta(oVentaId);

            Opcion oOpcion = await _opcionService.GetOpcion(oVenta.OpcionId);

            VentaFrontDTO oVentaFrontDTO = new()
            {
                OpcionId = oOpcion.OpcionId,
                RifaId = oOpcion.RifaId,
                UsuarioId = oOpcion.UsuarioId,
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

        public async Task<VentaFrontDTO> MCInsertVenta(VentaDTO VentaDTO)
        {

            OpcionDTO oOpcionDTO = new()
            {
                OpcionId = 0,
                RifaId = VentaDTO.RifaId,
                UsuarioId = VentaDTO.UsuarioId,
                CantidadOpciones = VentaDTO.CantidadOpciones,
                EstadoOpcion = Int32.Parse(_configuration["EstadoOpcion:Registrada"]),
                AuditoriaUsuarioIngreso = VentaDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
            };

            //Insertamos la opción que devuelve el registro recién insertado
            Opcion oOpcion = await _opcionService.IInsertOpcion(oOpcionDTO);

            //Obtenemos el precio de la Rifa para registrarlo en la venta
            Precio oPrecio = await _precioService.GetPrecioUnitario(VentaDTO.RifaId);

            Ventum oVenta = new()
            {
                VentaId =  VentaDTO.VentaId,
                OpcionId = oOpcionDTO.OpcionId, // Relación entre Opción y Venta
                TipoComprobante = Int32.Parse(_configuration["TipoComprobante:Boleta"]),
                SerieComprobante = _configuration["SeriComprobante:SeriComprobanteBoleta"],
                NumeroComprobante = "0", //VentaDTO.NumeroComprobante,
                Moneda = Int32.Parse(_configuration["Moneda:Soles"]),
                Monto = oPrecio.PrecioUnitario *  VentaDTO.CantidadOpciones, // VentaDTO.Monto,
                EstadoVenta = Int32.Parse(_configuration["EstadoVenta:Registrada"]),
                AuditoriaUsuarioIngreso = VentaDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
            };

            //Insertamos la venta que devuelve el registro recién insertado
            oVenta = await InsertVenta(oVenta);

            VentaFrontDTO oVentaFrontDTO = new()
            {
                OpcionId = oOpcion.OpcionId,
                RifaId = oOpcion.RifaId,
                UsuarioId = oOpcion.UsuarioId,
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

        // public async Task<Ventum> UpdateVenta(VentaDTO VentaDTO)
        // {

        //     Ventum oVenta = await _ventaRepository.Get(VentaDTO.VentaId);

        //     oVenta.VentaId =  VentaDTO.VentaId;
        //     oVenta.OpcionId = VentaDTO.OpcionId;
        //     oVenta.TipoComprobante = VentaDTO.TipoComprobante;
        //     oVenta.SerieComprobante = VentaDTO.SerieComprobante;
        //     oVenta.NumeroComprobante = VentaDTO.NumeroComprobante;
        //     oVenta.Moneda = VentaDTO.Moneda;
        //     oVenta.Monto = VentaDTO.Monto;
        //     oVenta.EstadoVenta = VentaDTO.EstadoVenta;
        //     oVenta.AuditoriaUsuarioModificacion = VentaDTO.AuditoriaUsuario;
        //     oVenta.AuditoriaFechaModificacion = DateTime.Now;

        //     await _ventaRepository.Put(oVenta);

        //     return oVenta;

        // }

        // public async Task<Ventum> DeleteVenta(Int32 VentaId)
        // {

        //     Ventum oVenta = await _ventaRepository.Get(VentaId);

        //     await _ventaRepository.Delete(oVenta);

        //     return oVenta;

        // }        
    }

}
