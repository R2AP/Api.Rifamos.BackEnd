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
        Task<Ventum> Get(Int32 oVentaId);
        Task<Ventum> Insert(Ventum oVentum);
        Task<Ventum> Update(Ventum oVentum);
        Task<Ventum> Delete(Int32 oVentaId);

        //Métodos Complementarios
        Task<VentaFrontDTO> GetVentaOpcion(Int32 oVentaId);
        Task<VentaFrontDTO> InsertVentaOpcion(VentaDTO oVentaDTO);
    }
}