using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IVentaService : IServiceBase
    {

        Task<Ventum> GetVenta(Int32 VentaId);
        //Task<List<Rifa>> GetListRifaUsuario(Int32 UsuarioId);
        Task<Ventum> InsertVenta(VentaDTO VentaDTO);
        Task<Ventum> UpdateVenta(VentaDTO VentaDTO);
        Task<Ventum> DeleteVenta(Int32 VentaId);
    }
}