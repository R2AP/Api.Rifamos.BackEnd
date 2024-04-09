using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IOpcionService : IServiceBase
    {
        Task<Opcion> GetOpcion(Int32 OpcionId);
        Task<List<Opcion>> GetListOpcion(Int32 RifaId, Int32 UsuarioId);
        Task<Opcion> InsertOpcion(OpcionDTO OpcionDTO);
        Task<Opcion> UpdateOpcion(OpcionDTO OpcionDTO);        
    }
}