using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IRifaRepository : IRepositoryBase<Rifa>
    {
        Task<List<Rifa>> GetListRifaUsuario(Int32 UsuarioId);
        Task<List<Rifa>> GetListRifaEstado(Int32 EstadoId);
    }

}