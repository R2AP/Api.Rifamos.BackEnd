using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Interseguro.ADMWR.Backend.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IOpcionService : IServiceBase
    {

        Task<List<Opcion>> GetListOpcion(Int32 RifaId, Int32 UsuarioId);
        Task<Opcion> InsertOpcion(Opcion Opcion);
        Task<Opcion> UpdateOpcion(Opcion Opcion);        
    }
}