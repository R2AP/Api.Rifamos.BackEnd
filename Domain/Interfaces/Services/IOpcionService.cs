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
        Task<Opcion> Get(Int32 oOpcionId);
        Task<Opcion> Insert(Opcion oOpcion);
        Task<Opcion> Update(Opcion oOpcion);
        Task<Opcion> Delete(Int32 oOpcionId);

        //Métodos Complementarios
        Task<OpcionFrontDTO> GetOpcionToken(string oTokenOpcion);
        Task<List<OpcionFrontDTO>> GetListOpcion(Int32 oRifaId, Int32 oUsuarioId);
        Task<Opcion> InsertOpcion(OpcionDTO oOpcionDTO);

    }
}

