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
        Task<Opcion> Get(Int32 OpcionId);
        Task<Opcion> Insert(Opcion Opcion);
        Task<Opcion> Update(Opcion Opcion);
        Task<Opcion> Delete(Int32 OpcionId);

        //Métodos Complementarios
        Task<OpcionFrontDTO> GetOpcionToken(string TokenOpcion);
        Task<List<OpcionFrontDTO>> GetListOpcion(Int32 RifaId, Int32 UsuarioId);
        Task<Opcion> InsertOpcion(OpcionDTO OpcionDTO);

    }
}

