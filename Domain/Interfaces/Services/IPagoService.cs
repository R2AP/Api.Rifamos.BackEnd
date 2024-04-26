using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IPagoService : IServiceBase
    {
        //Métodos Básicos
        Task<Pago> Get(Int32 oPagoId);
        Task<Pago> Insert(Pago oPago);
        Task<Pago> Update(Pago oPago);
        Task<Pago> Delete(Int32 oPagoId);
        
        //Métodos Complementarios
        Task<PagoFrontDTO> InsertPago(PagoDTO PagoDTO);
        Task<PagoFrontDTO> UpdatePago(PagoDTO PagoDTO);
        Task<PagoFrontDTO> DeletePago(Int32 PagoId);        
    }
}