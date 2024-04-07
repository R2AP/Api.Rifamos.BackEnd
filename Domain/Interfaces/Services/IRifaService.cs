using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Interseguro.ADMWR.Backend.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IRifaService : IServiceBase
    {

        Task<Rifa> GetRifa(Int32 RifaId);
        Task<List<Rifa>> GetListRifaUsuario(Int32 UsuarioId);
        Task<List<Rifa>> GetListRifaEstado(Int32 UsuarioId);
        Task<Rifa> InsertRifa(Rifa Rifa);
        Task<Rifa> UpdateRifa(Rifa Rifa);
    }
}