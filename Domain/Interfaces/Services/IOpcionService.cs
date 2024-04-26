using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IOpcionService : IServiceBase
    {
        //Métodos Básicos
        Task<Opcion> GetOpcion(Int32 OpcionId);
        // Task<Opcion> InsertOpcion(Opcion Opcion);
        // Task<Opcion> UpdateOpcion(Opcion Opcion);
        // Task<Opcion> DeleteOpcion(Int32 OpcionId);

        //Métodos Complementarios
        Task<OpcionFrontDTO> IGetOpcionToken(string TokenOpcion);
        Task<List<OpcionFrontDTO>> IGetListOpcion(Int32 RifaId, Int32 UsuarioId);
        Task<Opcion> IInsertOpcion(OpcionDTO OpcionDTO);

    }
}

