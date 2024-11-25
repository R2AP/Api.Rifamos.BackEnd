using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IRiferoService : IServiceBase
    {
        //Métodos Básicos
        Task<Rifero> Get(Int32 oRiferoId);
        Task<Rifero> Insert(Rifero oRifero);
        Task<Rifero> Update(Rifero oRifero);
        Task<Rifero> Delete(Int32 oRiferoId);

        //Métodos Complementarios
        Task<Rifero> GetRifero(Int32 oRiferoId);
        Task<Rifero> GetRiferoPorEmail(string oEmail);    
        Task<RiferoFrontDTO> InsertRifero(RiferoDTO oRiferoDTO);
        Task<RiferoFrontDTO> UpdateRifero(RiferoDTO oRiferoDTO);
        Task<RiferoFrontDTO> DeleteRifero(Int32 oRiferoId);
        Task<RiferoFrontDTO> UpdatePasswordRifero(RiferoPasswordDTO oRiferoPasswordDTO);
        Task<RiferoFrontDTO> RecuperarPassword(string oEmail);
    }

}