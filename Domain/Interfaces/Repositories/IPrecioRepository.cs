using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IPrecioRepository : IRepositoryBase<Precio>
    {
        Task<Precio> GetPrecioUnitario(Int32 RifaId);
        Task<List<Precio>> GetListPrecioUnitario(Int32 oRifaId);
    }

}