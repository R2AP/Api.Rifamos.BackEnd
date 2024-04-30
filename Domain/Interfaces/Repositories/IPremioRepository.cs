using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Repositories

{
    public interface IPremioRepository : IRepositoryBase<Premio>
    {
        Task<List<Premio>> GetListPremio(Int32 RifaId);
    }

}