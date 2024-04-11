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

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public VentaService(IVentaRepository ventaRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _ventaRepository = ventaRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Ventum> GetVenta(Int32 VentaId)
        {
            return await _ventaRepository.Get(VentaId);
        }

        // public async Task<List<Rifa>> GetListRifaEstado(Int32 EstadoId)
        // {
        //     // var ejemplo = _configuration["prueba"];
        //     return await _rifaRepository.GetListRifaEstado(EstadoId);
        // }

        public async Task<Ventum> InsertVenta(VentaDTO VentaDTO)
        {
            // var ejemplo = _configuration["prueba"];

            Ventum oVenta = new Ventum(){

                VentaId =  VentaDTO.VentaId,
                OpcionId = VentaDTO.OpcionId,
                TipoComprobante = VentaDTO.TipoComprobante,
                SerieComprobante = VentaDTO.SerieComprobante,
                NumeroComprobante = VentaDTO.NumeroComprobante,
                Moneda = VentaDTO.Moneda,
                Monto = VentaDTO.Monto,
                EstadoVenta = VentaDTO.EstadoVenta,
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
