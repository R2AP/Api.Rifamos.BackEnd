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
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IOpcionRepository _opcionRepository;
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
            _opcionRepository = opcionRepository;
            _opcionService = opcionService;
            _precioService = precioService;
            _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Ventum> GetVenta(Int32 VentaId)
        {
            return await _ventaRepository.Get(VentaId);
        }

        public async Task<Ventum> InsertVenta(VentaDTO VentaDTO)
        {
            Precio oPrecio = new();
            //Obtenemos el precio de la Rifa para registrarlo en la venta
            oPrecio = await _precioService.GetPrecioUnitario(VentaDTO.RifaId);

            OpcionDTO oOpcionDTO = new()
            {
                OpcionId = 0,
                RifaId = VentaDTO.RifaId,
                UsuarioId = VentaDTO.UsuarioId,
                CantidadOpciones = VentaDTO.CantidadOpciones,
                EstadoOpcion = Int32.Parse(_configuration["EstadoOpcion:Registrada"]),
                AuditoriaUsuarioIngreso = VentaDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now
            };

            await _opcionService.InsertOpcion(oOpcionDTO);

            Ventum oVenta = new()
            {
                VentaId =  VentaDTO.VentaId,
                OpcionId = oOpcionDTO.OpcionId, // Relación entre Opción y Venta
                TipoComprobante = Int32.Parse(_configuration["TipoComprobante:Boleta"]),
                SerieComprobante = VentaDTO.SerieComprobante,
                NumeroComprobante = VentaDTO.NumeroComprobante,
                Moneda = VentaDTO.Moneda,
                Monto = oPrecio.PrecioUnitario *  VentaDTO.CantidadOpciones, // VentaDTO.Monto,
                EstadoVenta = Int32.Parse(_configuration["EstadoVenta:Registrada"]),
                AuditoriaUsuarioIngreso = VentaDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now
            };

            await _ventaRepository.Post(oVenta);

            return oVenta;

        }

        public async Task<Ventum> UpdateVenta(VentaDTO VentaDTO)
        {

            Ventum oVenta = await _ventaRepository.Get(VentaDTO.VentaId);

            oVenta.VentaId =  VentaDTO.VentaId;
            oVenta.OpcionId = VentaDTO.OpcionId;
            oVenta.TipoComprobante = VentaDTO.TipoComprobante;
            oVenta.SerieComprobante = VentaDTO.SerieComprobante;
            oVenta.NumeroComprobante = VentaDTO.NumeroComprobante;
            oVenta.Moneda = VentaDTO.Moneda;
            oVenta.Monto = VentaDTO.Monto;
            oVenta.EstadoVenta = VentaDTO.EstadoVenta;
            oVenta.AuditoriaUsuarioModificacion = VentaDTO.AuditoriaUsuarioIngreso;
            oVenta.AuditoriaFechaModificacion = DateTime.Now;

            await _ventaRepository.Put(oVenta);

            return oVenta;

        }

        public async Task<Ventum> DeleteVenta(Int32 VentaId)
        {

            Ventum oVenta = await _ventaRepository.Get(VentaId);

            await _ventaRepository.Delete(oVenta);

            return oVenta;

        }        
    }

}
