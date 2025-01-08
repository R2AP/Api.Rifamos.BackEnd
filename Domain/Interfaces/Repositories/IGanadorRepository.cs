using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IGanadorRepository : IRepositoryBase<Ganador>{
         
        Task<List<Ganador>> GetGanadorPorPremio(Int32 oPremioId);

    }

}