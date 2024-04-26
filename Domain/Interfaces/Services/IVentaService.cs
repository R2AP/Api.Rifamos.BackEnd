using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IVentaService : IServiceBase
    {
        //Métodos Básicos
        Task<Ventum> GetVenta(Int32 VentaId);
        // Task<Venta> InsertVenta(Venta Venta);
        // Task<Venta> UpdateVenta(Venta Venta);
        // Task<Venta> DeleteVenta(Int32 VentaId);

        //Métodos Complementarios
        Task<VentaFrontDTO> MCGetVenta(Int32 oVentaId);
        Task<VentaFrontDTO> MCInsertVenta(VentaDTO VentaDTO);
    }
}