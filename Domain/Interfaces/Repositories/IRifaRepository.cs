using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IRifaRepository : IRepositoryBase<Rifa>
    {
        Task<List<Rifa>> GetListRifaUsuario(Int32 UsuarioId);
        Task<List<Rifa>> GetListRifaEstadoIndicadorPremium(Int32 EstadoId, String IndicadorPremium);
        Task<List<Rifa>> GetListRifaEstado(Int32 EstadoId);
        Task<List<Rifa>> GetListRifaFechaTerminado(Int32 Anho);
        Task<List<Rifa>> GetRifaId(Int32 oRifaId);
        
    }

}