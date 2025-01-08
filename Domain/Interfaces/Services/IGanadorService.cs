using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface IGanadorService : IServiceBase
    {
        //Métodos Básicos
        Task<Ganador> Get(Int32 oPremioId);
        Task<Ganador> Insert(Ganador oGanador);
        Task<Ganador> Update(Ganador oGanador);
        Task<Ganador> Delete(Int32 oPremioId);

        //Métodos Complementarios
        Task<GanadorFrontDTO> InsertGanador(GanadorDTO oGanadorDTO);
        Task<GanadorFrontDTO> UpdateGanador(GanadorDTO oGanadorDTO);
        Task<GanadorFrontDTO> DeleteGanador(Int32 oPremioId);
        Task<Ganador> GetGanadorPorPremio(Int32 oPremioId);
    }

}