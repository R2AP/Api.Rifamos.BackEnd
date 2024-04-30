using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IRifaService : IServiceBase
    {
        //Métodos Básicos
        Task<Rifa> Get(Int32 oRifaId);
        Task<Rifa> Insert(Rifa oRifa);
        Task<Rifa> Update(Rifa oRifa);
        Task<Rifa> Delete(Int32 oRifaId);

		//Métodos Complementarios
        Task<RifaFrontDTO> GetRifa(Int32 oRifaId);
        Task<RifaFrontDTO> InsertRifa(RifaDTO oRifaDTO);
        Task<RifaFrontDTO> UpdateRifa(RifaDTO oRifaDTO);
        Task<RifaFrontDTO> DeleteRifa(Int32 oRifaId);
        Task<List<RifaFrontDTO>> GetListRifaUsuario(Int32 oUsuarioId);
        Task<List<RifaFrontDTO>> GetListRifaEstado(Int32 oUsuarioId);
    }
}