using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IOpcionRepository : IRepositoryBase<Opcion>
    {
        Task<List<Opcion>> GetListOpcion(Int32 RifaId, Int32 UsuarioId);
        Task<Opcion> GetOpcionToken(string TokenOpcion);        

    }

}