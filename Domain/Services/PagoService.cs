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

        public async Task<Pago> Get(Int32 oPagoId) => await _pagoRepository.Get(oPagoId);

        public async Task<Pago> Insert(Pago oPago)
        {
            
            await  _pagoRepository.Post(oPago);

            return await Get(oPago.PagoId);
        }

        public async Task<Pago> Update(Pago Pago)
        {
            
            await  _pagoRepository.Put(Pago);

            return await Get(Pago.PagoId);
        }

        public async Task<Pago> Delete(Int32 oPagoId)
        {

            Pago oPago = await Get(oPagoId);

            await _pagoRepository.Delete(oPago);

            return oPago;

        }        

        public async Task<PagoFrontDTO> InsertPago(PagoDTO PagoDTO)
        {

            Pago oPago = new(){

                PagoId = PagoDTO.PagoId, 
                VentaId = PagoDTO.VentaId, 
                TipoPago = PagoDTO.TipoPago,
                CodigoTransaccion = PagoDTO.CodigoTransaccion, 
                FechaPago = PagoDTO.FechaPago,
                HoraPago = PagoDTO.HoraPago,
                Moneda  = PagoDTO.Moneda,
                Monto = PagoDTO.Monto,
                EstadoPago = PagoDTO.EstadoPago,
                AuditoriaUsuarioIngreso = PagoDTO.AuditoriaUsuario, 
                AuditoriaFechaIngreso = DateTime.Now
                
            };

            oPago = await Insert(oPago);

            PagoFrontDTO oPagoFrontDTO = new()
            {
                PagoId = oPago.PagoId,
                VentaId = oPago.VentaId,
                TipoPago = oPago.TipoPago,
                CodigoTransaccion = oPago.CodigoTransaccion,
                FechaPago = oPago.FechaPago,
                HoraPago = oPago.HoraPago,
                Moneda  = oPago.Moneda,
                Monto = oPago.Monto,
                EstadoPago = oPago.EstadoPago
            };

            return oPagoFrontDTO;

        }

        public async Task<PagoFrontDTO> UpdatePago(PagoDTO PagoDTO)
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
            oPago.AuditoriaUsuarioModificacion = PagoDTO.AuditoriaUsuario;
            oPago.AuditoriaFechaModificacion  = DateTime.Now;

            oPago = await Update(oPago);

            PagoFrontDTO oPagoFrontDTO = new()
            {
                PagoId = oPago.PagoId,
                VentaId = oPago.VentaId,
                TipoPago = oPago.TipoPago,
                CodigoTransaccion = oPago.CodigoTransaccion,
                FechaPago = oPago.FechaPago,
                HoraPago = oPago.HoraPago,
                Moneda  = oPago.Moneda,
                Monto = oPago.Monto,
                EstadoPago = oPago.EstadoPago
            };

            return oPagoFrontDTO;

        }

        public async Task<PagoFrontDTO> DeletePago(Int32 PagoId){

            Pago oPago = await Delete(PagoId);

            PagoFrontDTO oPagoFrontDTO = new()
            {
                PagoId = oPago.PagoId,
                VentaId = oPago.VentaId,
                TipoPago = oPago.TipoPago,
                CodigoTransaccion = oPago.CodigoTransaccion,
                FechaPago = oPago.FechaPago,
                HoraPago = oPago.HoraPago,
                Moneda  = oPago.Moneda,
                Monto = oPago.Monto,
                EstadoPago = oPago.EstadoPago
            };

            return oPagoFrontDTO;            
        } 
    }

}
