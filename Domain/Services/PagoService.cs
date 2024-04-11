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
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public PagoService(IPagoRepository pagoRepository,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _pagoRepository = pagoRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Pago> GetPago(Int32 PagoId)
        {
            return await _pagoRepository.Get(PagoId);
        }

        // public async Task<List<Rifa>> GetListRifaEstado(Int32 EstadoId)
        // {
        //     // var ejemplo = _configuration["prueba"];
        //     return await _rifaRepository.GetListRifaEstado(EstadoId);
        // }

        public async Task<Pago> InsertPago(PagoDTO PagoDTO)
        {

            Pago oPago = new Pago(){

                PagoId = PagoDTO.PagoId, 
                VentaId = PagoDTO.VentaId, 
                TipoPago = PagoDTO.TipoPago,
                CodigoTransaccion = PagoDTO.CodigoTransaccion, 
                FechaPago = PagoDTO.FechaPago,
                HoraPago = PagoDTO.HoraPago,
                Moneda  = PagoDTO.Moneda,
                Monto = PagoDTO.Monto,
                EstadoPago = PagoDTO.EstadoPago,
                AuditoriaUsuarioIngreso = PagoDTO.AuditoriaUsuarioIngreso, 
                AuditoriaFechaIngreso = DateTime.Now
                
            };

            await _pagoRepository.Post(oPago);

            return oPago;

        }

        public async Task<Pago> UpdatePago(PagoDTO PagoDTO)
        {

            Pago oPago = await _pagoRepository.Get(PagoDTO.PagoId);

            oPago.PagoId = PagoDTO.PagoId;
            oPago.VentaId = PagoDTO.VentaId; 
            oPago.TipoPago = PagoDTO.TipoPago;
            oPago.CodigoTransaccion = PagoDTO.CodigoTransaccion; 
            oPago.FechaPago = PagoDTO.FechaPago;
            oPago.HoraPago = PagoDTO.HoraPago;
            oPago.Moneda  = PagoDTO.Moneda;
            oPago.Monto = PagoDTO.Monto;
            oPago.EstadoPago = PagoDTO.EstadoPago;
            oPago.AuditoriaUsuarioModificacion = PagoDTO.AuditoriaUsuarioModificacion;
            oPago.AuditoriaFechaModificacion  = DateTime.Now;

            await _pagoRepository.Put(oPago);

            return oPago;

        }

        public async Task<Pago> DeletePago(Int32 PagoId)
        {

            Pago oPago = await _pagoRepository.Get(PagoId);

            await _pagoRepository.Delete(oPago);

            return oPago;

        }        
    }

}
