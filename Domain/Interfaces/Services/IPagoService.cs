using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IPagoService : IServiceBase
    {

        Task<Pago> GetPago(Int32 PagoId);
        //Task<List<Rifa>> GetListRifaUsuario(Int32 UsuarioId);
        Task<Pago> InsertPago(PagoDTO PagoDTO);
        Task<Pago> UpdatePago(PagoDTO PagoDTO);
        Task<Pago> DeletePago(Int32 PagoId);
    }
}